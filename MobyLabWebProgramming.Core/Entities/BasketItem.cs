using System.ComponentModel.DataAnnotations.Schema;
using MobyLabWebProgramming.Backend.Entities;

namespace MobyLabWebProgramming.Core.Entities;

public class BasketItem : BaseEntity
{
    public int Quantity { get; set; }
    
    public Guid ProductId { get; set; }
    
    public Product Product { get; set; } = null!;
    
    public Guid BasketId { get; set; }
    
    public Basket Basket { get; set; } = null!;
}