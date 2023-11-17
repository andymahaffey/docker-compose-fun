using Microsoft.AspNetCore.Mvc;
using Api.Data.Providers;
using Api.RequestModels;
using Api.Services;

namespace Api.Controllers;
[ApiController]
[Route("[controller]")]
public class CassandraController : ControllerBase
{
    private readonly ICassandraProvider _cassandraProvider;
    private readonly IShoppingCartService _shoppingCartService;
    public CassandraController(ICassandraProvider cassandraProvider, IShoppingCartService shoppingCartService)
    {
        _cassandraProvider = cassandraProvider;
        _shoppingCartService = shoppingCartService;
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ShoppingCartResponse> Get(string userId)
    {
        var result = await _shoppingCartService.GetAsync(userId);
 
        if(result == null)
        {
            return null;
        }

        return new ShoppingCartResponse
        {
            UserId = result.UserId,
            ItemCount = result.ItemCount,
            LastUpdateDate = result.LastUpdateDate
        };
    }

    [HttpPut("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Put(string userId, [FromBody]ShoppingCartPut shoppingCart)
    { 
        await _shoppingCartService.UpdateAsync(new ShoppingCart
        {
            UserId = userId,
            ItemCount = shoppingCart.ItemCount,
            LastUpdateDate = shoppingCart.LastUpdateDate
        });
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Post([FromBody]ShoppingCartPost shoppingCart)
    {
        await _shoppingCartService.CreateAsync(new ShoppingCart
        { 
            UserId = shoppingCart.UserId,
            ItemCount = shoppingCart.ItemCount,
            LastUpdateDate = shoppingCart.LastUpdateDate
        });
        return StatusCode(StatusCodes.Status201Created); 
    }

    [HttpDelete("{userId}")]
    public async Task<ActionResult> Delete(string userId)
    {
        await _shoppingCartService.DeleteAsync(userId);
        return Ok();
    }
}