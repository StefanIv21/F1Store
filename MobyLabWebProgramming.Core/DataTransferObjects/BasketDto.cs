using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class BasketDto
{
    public Guid Id { get; set; }
    
    public string UserId { get; set; } = null!;
    
    public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
}