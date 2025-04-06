using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.Extensions;

public static  class BasketExtensions
{
    public static BasketDto MapBasketDto(this Basket basket)
    {
        
        return new BasketDto
        {
            Id = basket.Id,
            UserId = basket.UserId,
            Items = basket.Items.Select(x => new BasketItemDto
            {
                ProductId = x.ProductId,
                Name = x.Product.Name,
                Price = x.Product.Price,
                Type = x.Product.Type,
                Brand = x.Product.Brand,
                Quantity = x.Quantity
            }).ToList()
        };
    }
    
}