namespace Api.RequestModels;

public class ShoppingCartResponse
{
    public string? UserId { get; set; }
    public int ItemCount {get; set; }
    public DateTime LastUpdateDate { get; set; }
}