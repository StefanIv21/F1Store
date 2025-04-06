using System.ComponentModel.DataAnnotations;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ProductDto
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public string Description { get; set; } = null!;
    
    [Required]
    [Range(0, long.MaxValue)]
    public long Price { get; set; } 
    
    [Required]
    public string Type { get; set; } = null!;
    
    [Required]
    public string Brand { get; set; } = null!;
    
    [Required]
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}