using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Transport;
using ElasticQueryBuilder.Interfaces;
using ElasticQueryBuilder.Models.Dtos;
using System.Net.Security;

namespace ElasticQueryBuilder.Services;

public class ElasticSearchService : IElasticSearchService
{

    private ElasticsearchClient _client;
    private string UserName {  get; set; }
    private string Password { get; set; }
    private string CertificateFingerprint { get; set; }
    private string Url { get; set; }

    private bool _isAuthenticated = false;

    public ElasticSearchService(IConfiguration configuration)
    {

    }

    public ElasticsearchClient GetClient() => _client;

    public async Task<List<string>> GetFieldsAsync(string indexName)
    {

        EnsureAuthenticated();

        try
        {
            var response = await _client.Indices.GetMappingAsync(indexName);

            if (!response.IsValidResponse)
                throw new Exception($"Mapping bilgisi alınamadı: {response.DebugInformation}");

            var indexMapping = response.Indices[indexName];

            if (indexMapping == null || indexMapping.Mappings == null)
                throw new Exception("Mapping bilgisi bulunamadı.");

            // Tüm alanları çıkar
            var fields = indexMapping.Mappings.Properties;

            var fieldList = new List<string>();

            foreach (var field in fields)
            {
                fieldList.Add($" {field.Key.ToString()} - {field.Value.ToString()}");
            }

            return fieldList;
        }
        catch (Exception ex)
        {
            throw new Exception($"Alanları alırken hata oluştu: {ex.Message}");
        }
    }

    public async Task<object> BuildQueryAsync(QueryRequest queryRequest)
    {

        EnsureAuthenticated();

        try
        {
            var mustQueries = new List<Query>();

            foreach (var filter in queryRequest.Filters)
            {
                var matchQuery = new MatchQuery(new Field(filter.Key))
                {
                    Query = filter.Value          
                };

                mustQueries.Add(matchQuery);
            }

            var query = new BoolQuery
            {
                Must = mustQueries 
            };

            var searchRequest = new SearchRequest(queryRequest.Index)
            {
                Query = query
            };

            var response = await _client.SearchAsync<object>(searchRequest);

            if (!response.IsValidResponse)
                throw new Exception($"Search failed: {response.DebugInformation}");

            return response.Documents;
        }
        catch (Exception ex)
        {
            throw new Exception($"Query building failed: {ex.Message}");
        }
    }

    public async Task<string> LoginElasticAsync(LoginRequest loginRequest)
    {
        UserName = loginRequest.Username;
        Password = loginRequest.Password;
        CertificateFingerprint = loginRequest.FingerPrint;
        Url = loginRequest.Url;

        var settings = new ElasticsearchClientSettings(new Uri(Url))
            .CertificateFingerprint(CertificateFingerprint)
            .Authentication(new BasicAuthentication(UserName, Password))
            .ServerCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) =>
            {
                // For controlled environments, you can skip certificate validation
                return sslPolicyErrors == SslPolicyErrors.None || true; // SSL bypass in dev
            });

        _client = new ElasticsearchClient(settings);

        // Test the connection to make sure it's successful
        try
        {
            var pingResponse = await _client.PingAsync();
            if (!pingResponse.IsValidResponse)
            {
                _isAuthenticated = false;
                throw new Exception("Unable to connect to Elasticsearch.");
            }
        }
        catch (Exception ex)
        {
            _isAuthenticated = false;
            throw new Exception($"Connection failed: {ex.Message}");
        }

        _isAuthenticated = true;
        return "Login successful";
    }

    private void EnsureAuthenticated()
    {
        if (!_isAuthenticated)
        {
            throw new UnauthorizedAccessException("User is not authenticated. Please log in first.");
        }
    }
}

