using Elastic.Clients.Elasticsearch.Nodes;
using Elastic.Clients.Elasticsearch.Sql;
using ElasticQueryBuilder.Interfaces;
using ElasticQueryBuilder.Models;
using ElasticQueryBuilder.Models.Dtos;
using ElasticQueryBuilder.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json.Serialization;

namespace ElasticQueryBuilder.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QueryController : ControllerBase
{

    [HttpGet("fields/{index}")]
    public async Task<IActionResult> GetFields([FromRoute] string index,
                               [FromHeader(Name = "username")] string username,
                               [FromHeader(Name = "password")] string password,
                               [FromHeader(Name = "fingerprint")] string fingerprint,
                               [FromHeader(Name = "url")] string url)
    {

        var credentials = new ElasticCredentials
        {
            Username = username,
            Password = password,
            Fingerprint = fingerprint,
            Url = url
        };

        if (string.IsNullOrEmpty(index))
            return BadRequest("Index name is required.");

        try
        {
            ElasticSearchService _elasticSearchService = new ElasticSearchService(credentials);

            var fields = await _elasticSearchService.GetFieldsAsync(index);
            return Ok(fields);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error Occured: {ex.Message}");
        }
    }

    [HttpPost("build-query")]
    public async Task<IActionResult> BuildQuery([FromBody] ElasticQueryBuilder.Models.Dtos.QueryRequest queryRequest,
                                                  [FromHeader(Name = "username")] string username,
                                                  [FromHeader(Name = "password")] string password,
                                                  [FromHeader(Name = "fingerprint")] string fingerprint,
                                                  [FromHeader(Name = "url")] string url)
    {
        if (queryRequest == null || string.IsNullOrEmpty(queryRequest.Index))
            return BadRequest("QueryRequest is required and must contain an index name.");

        var credentials = new ElasticCredentials
        {
            Username = username,
            Password = password,
            Fingerprint = fingerprint,
            Url = url
        };

        try
        {
            ElasticSearchService _elasticSearchService = new ElasticSearchService(credentials);

            var result = await _elasticSearchService.BuildQueryAsync(queryRequest);

            return Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("User is not authenticated. Please log in first.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error Occurred: {ex.Message}");
        }
    }

}
