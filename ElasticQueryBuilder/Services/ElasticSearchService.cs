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
        configuration = configuration;
        var uri = configuration.GetValue<string>("ElasticSearch:Uri");
        var certificateFingerprint = configuration.GetValue<string>("ElasticSearch:CertificateFingerprint");
        var username = configuration.GetValue<string>("ElasticSearch:Username");
        var password = configuration.GetValue<string>("ElasticSearch:Password");

        // ElasticSearch istemcisi ayarları
        var settings = new ElasticsearchClientSettings(new Uri(uri))
            .CertificateFingerprint(certificateFingerprint)
            .Authentication(new BasicAuthentication(username, password));
            //.ServerCertificateValidationCallback((sender, certificate, chain, sslPolicyErrors) => true); // SSL bypass

        _client = new ElasticsearchClient(settings);
    }

    public async Task<ElasticsearchClient> GetClient() => _client;

    public async Task<string> IndexDocumentAsync<T>(T document, string indexName) where T : class
    {
        var response = await _client.IndexAsync(document, idx => idx.Index(indexName));
        return response.IsValidResponse ? "Document indexed successfully!" : response.DebugInformation;
    }
}
