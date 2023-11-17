namespace Api.Data.Providers;
public class ShoppingCart
{
    public string? UserId { get; set; }
    public int ItemCount {get; set; }
    public DateTime LastUpdateDate { get; set; }

    public override string ToString()
    {
        return $"UserId: {UserId}, ItemCount: {ItemCount}, LastUpdateDate: {LastUpdateDate}";
    }
}