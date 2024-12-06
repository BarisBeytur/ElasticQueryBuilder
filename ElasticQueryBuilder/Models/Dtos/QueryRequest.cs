namespace ElasticQueryBuilder.Models.Dtos;

public class QueryRequest
{
    public string Index { get; set; }
    public Dictionary<string, string> Filters { get; set; } = new Dictionary<string, string>();
}