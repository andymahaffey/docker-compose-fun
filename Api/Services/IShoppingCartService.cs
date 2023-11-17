using Api.Data.Providers;

namespace Api.Services;
public interface IShoppingCartService
{
    Task UpdateAsync(ShoppingCart shoppingCart);
    Task CreateAsync(ShoppingCart shoppingCart);
    Task<ShoppingCart> GetAsync(string userId);
    
    Task DeleteAsync(string userId);
}