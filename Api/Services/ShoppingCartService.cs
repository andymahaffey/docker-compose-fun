using Api.Data.Providers;
using Cassandra.Data.Linq;
using Cassandra.Mapping;

namespace Api.Services;
public class ShoppingCartService : IShoppingCartService
{    
    private readonly ICassandraProvider _cassandraProvider;

    public ShoppingCartService(ICassandraProvider cassandraProvider)
    {
        _cassandraProvider = cassandraProvider;

        MappingConfiguration.Global.Define(
            new Map<ShoppingCart>()
            .KeyspaceName("store")
            .TableName("shopping_cart")
            .PartitionKey(u => u.UserId)
            .Column(s => s.UserId, cm => cm.WithName("userid"))
            .Column(s => s.ItemCount, cm => cm.WithName("item_count"))
            .Column(s => s.LastUpdateDate, cm => cm.WithName("last_update_timestamp")));
    }

    public async Task UpdateAsync(ShoppingCart shoppingCart)
    {
        var table = await GetTable();

            await table.Where(u => u.UserId == shoppingCart.UserId)
                .Select(u => new ShoppingCart 
                { 
                    ItemCount = shoppingCart.ItemCount, 
                    LastUpdateDate  = shoppingCart.LastUpdateDate 
                })
                .UpdateIfExists()
                .ExecuteAsync();
    }
    
    public async Task CreateAsync(ShoppingCart shoppingCart)
    {        
        var table = await GetTable();
        await table.Insert(shoppingCart).ExecuteAsync();
    }
    
    public async Task<ShoppingCart> GetAsync(string userId)
    {  
        var table = await GetTable();
        var result = await table
            .FirstOrDefault(a => a.UserId == userId)
            .ExecuteAsync();
        return result;   
    }
    
    public async Task DeleteAsync(string userId)
    {  
        var table = await GetTable();
        await table
            .Where(u => u.UserId == userId)
            .Delete()
            .ExecuteAsync();
    }
    

    private async Task<Table<ShoppingCart>> GetTable()
    {        
        var table = new Table<ShoppingCart>(_cassandraProvider.Session);
        await table.CreateIfNotExistsAsync();
        return table;
    }
}