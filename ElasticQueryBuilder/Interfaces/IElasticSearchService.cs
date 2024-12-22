using Elastic.Clients.Elasticsearch;
using ElasticQueryBuilder.Models.Dtos;

namespace ElasticQueryBuilder.Interfaces;

public interface IElasticSearchService
{
    Task<List<string>> GetFieldsAsync(string indexName);
    Task<object> BuildQueryAsync(QueryRequest queryRequest);
}
