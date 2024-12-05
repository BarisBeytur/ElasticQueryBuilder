using ElasticQueryBuilder.Models;
using ElasticQueryBuilder.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticQueryBuilder.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QueryController : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromBody] Query query)
    {
        var service = new ElasticSearchService();
        return Ok();
    }
}
