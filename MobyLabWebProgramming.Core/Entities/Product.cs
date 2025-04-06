using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Backend.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public long Price { get; set; } 
    
    public string Type { get; set; } = null!;
    
    public string Brand { get; set; } = null!;
    
    public int Stock { get; set; }
    
}