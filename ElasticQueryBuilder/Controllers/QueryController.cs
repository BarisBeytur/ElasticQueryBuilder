using Elastic.Clients.Elasticsearch.Nodes;
using ElasticQueryBuilder.Interfaces;
using ElasticQueryBuilder.Models;
using ElasticQueryBuilder.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json.Serialization;

namespace ElasticQueryBuilder.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QueryController : ControllerBase
{

    private readonly IElasticSearchService _elasticSearchService;

    public QueryController(IElasticSearchService elasticSearchService)
    {
        _elasticSearchService = elasticSearchService;
    }

    [HttpGet("fields")]
    public async Task<IActionResult> GetFields(string index)
    {
        if (string.IsNullOrEmpty(index))
            return BadRequest("Index name is required.");

        try
        {
            var fields = await _elasticSearchService.GetFieldsAsync(index);
            return Ok(fields);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error Occured: {ex.Message}");
        }
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Query query)
    {
        if (string.IsNullOrEmpty(query.Index) || string.IsNullOrEmpty(query.QueryString))
        {
            return BadRequest("Index or QueryString is missing.");
        }

        var client = _elasticSearchService.GetClient();

        var response = await client.SearchAsync<object>(s => s
            .Index(query.Index)
            .Query(q => q
                .QueryString(qs => qs
                    .Query(query.QueryString)
                )
            )
        );

        if (!response.IsValidResponse)
        {
            return StatusCode(500, response.DebugInformation);
        }

        return Ok(response.Documents);
    }
}
