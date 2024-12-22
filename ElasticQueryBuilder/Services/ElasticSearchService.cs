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

    public ElasticSearchService(ElasticCredentials credentials)
    {
        var settings = new ElasticsearchClientSettings(new Uri(credentials.Url))
           .CertificateFingerprint(credentials.Fingerprint)
           .Authentication(new BasicAuthentication(credentials.Username, credentials.Password))
           .ServerCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) =>
           {
               return sslPolicyErrors == SslPolicyErrors.None || true;
           });

        _client = new ElasticsearchClient(settings);
    }

    public async Task<List<string>> GetFieldsAsync(string indexName)
    {

        try
        {
            var response = await _client.Indices.GetMappingAsync(indexName);

            if (!response.IsValidResponse)
                throw new Exception($"Mapping bilgisi alınamadı: {response.DebugInformation}");

            var indexMapping = response.Indices[indexName];

            if (indexMapping == null || indexMapping.Mappings == null)
                throw new Exception("Mapping bilgisi bulunamadı.");

            var fields = indexMapping.Mappings.Properties;

            var fieldList = new List<string>();

            foreach (var field in fields)
            {
                fieldList.Add($" {field.Key.ToString()} : {field.Value.ToString().Split('.').Last()}");
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

}

