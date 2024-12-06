using Elastic.Clients.Elasticsearch.Nodes;
using Elastic.Clients.Elasticsearch.Sql;
using ElasticQueryBuilder.Interfaces;
using ElasticQueryBuilder.Models;
using ElasticQueryBuilder.Models.Dtos;
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

    [HttpPost("LoginElastic")]
    public async Task<IActionResult> LoginElastic([FromBody] LoginRequest loginRequest)
    {
        if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
            return BadRequest("Username and Password are required.");

        try
        {
            var result = await _elasticSearchService.LoginElasticAsync(loginRequest);
            if (string.IsNullOrEmpty(result))
                return Unauthorized("Invalid credentials or failed to connect to ElasticSearch.");

            return Ok("Login successful");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error occurred during login: {ex.Message}");
        }
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

    [HttpPost("build-query")]
    public async Task<IActionResult> BuildQuery([FromBody] ElasticQueryBuilder.Models.Dtos.QueryRequest queryRequest)
    {
        if (queryRequest == null || string.IsNullOrEmpty(queryRequest.Index))
            return BadRequest("QueryRequest is required and must contain an index name.");

        try
        {
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
