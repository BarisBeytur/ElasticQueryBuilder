using Elastic.Clients.Elasticsearch;

namespace ElasticQueryBuilder.Interfaces;

public interface IElasticSearchService
{
    Task<ElasticsearchClient> GetClient();
    Task<string> IndexDocumentAsync<T>(T document, string indexName) where T : class;
}
