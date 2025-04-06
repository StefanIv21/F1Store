using MobyLabWebProgramming.Backend.Entities;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class BasketItemDto
{
    public Guid ProductId { get; set; }
    
    public string Name { get; set; } = null!;
    
    public long Price { get; set; }
    
    public string  Brand { get; set; } = null!;
    
    public string Type { get; set; } = null!;
    
    public int Quantity { get; set; }
    
    public Guid BasketId { get; set; }
    
}