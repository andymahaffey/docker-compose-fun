using System.ComponentModel.DataAnnotations;

namespace Api.RequestModels;

public class ShoppingCartPost
{
    [Required]
    public string? UserId { get; set; }

    [Required]
    public int ItemCount {get; set; }
    
    [Required]
    public DateTime LastUpdateDate { get; set; }
}