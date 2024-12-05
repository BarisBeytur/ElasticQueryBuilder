using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Nodes;
using Elastic.Transport;
using ElasticQueryBuilder.Interfaces;
using ElasticQueryBuilder.Models;
using System;

namespace ElasticQueryBuilder.Services;

public class ElasticSearchService : IElasticSearchService
{

    private readonly ElasticsearchClient _client;

    public ElasticSearchService(IConfiguration configuration)
    {
        var uri = configuration.GetValue<string>("ElasticSearch:Uri");
        var certificateFingerprint = configuration.GetValue<string>("ElasticSearch:CertificateFingerprint");
        var username = configuration.GetValue<string>("ElasticSearch:Username");
        var password = configuration.GetValue<string>("ElasticSearch:Password");

        // ElasticSearch istemcisi ayarları
        var settings = new ElasticsearchClientSettings(new Uri(uri))
            .CertificateFingerprint(certificateFingerprint)
            .Authentication(new BasicAuthentication(username, password))
            .ServerCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true); // SSL bypass

        _client = new ElasticsearchClient(settings);
    }

    public ElasticsearchClient GetClient() => _client;

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

            // Tüm alanları çıkar
            var fields = indexMapping.Mappings.Properties;

            var fieldList = new List<string>();

            foreach (var field in fields)
            {
                fieldList.Add(field.Key.ToString());
            }

            return fieldList;
        }
        catch (Exception ex)
        {
            throw new Exception($"Alanları alırken hata oluştu: {ex.Message}");
        }
    }



}
