using System.ComponentModel.DataAnnotations;

namespace Api.RequestModels;

public class ShoppingCartPut
{
    [Required]
    public int ItemCount {get; set; }
    
    [Required]
    public DateTime LastUpdateDate { get; set; }
}