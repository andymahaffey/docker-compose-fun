using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CacheController : ControllerBase
{

    private readonly ILogger<CacheController> _logger;
    private readonly IDistributedCache _cache;

    public CacheController(ILogger<CacheController> logger, IDistributedCache cache)
    {
        _logger = logger;
        _cache = cache;
    }

    [HttpGet("{key}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CacheResponse>> Get(string key)
    {
        var cachedValue = new CacheResponse()
        {
            Value = await _cache.GetStringAsync(key)
        };

        if(cachedValue == null) {
            return NotFound();
        }

        return Ok(cachedValue);
    }
    
    [HttpPut("{key}")]
    public async Task Put(string key, CachePut body)
    {
        await _cache.SetAsync(key, Encoding.UTF8.GetBytes(body.Value));
    }
}
