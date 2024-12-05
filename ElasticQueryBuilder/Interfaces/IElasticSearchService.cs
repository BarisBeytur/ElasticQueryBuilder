using Elastic.Clients.Elasticsearch;

namespace ElasticQueryBuilder.Interfaces;

public interface IElasticSearchService
{
    ElasticsearchClient GetClient();
    Task<List<string>> GetFieldsAsync(string indexName);
}
