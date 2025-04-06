using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System.Linq;

namespace MobyLabWebProgramming.Core.Specifications
{
    public class BasketSpec : Specification<Basket, BasketDto>
    {
        public BasketSpec(string userId)
        {
            // Filter the basket by UserId
            Query.Where(basket => basket.UserId == userId);

            // Eager load Items and related Product
            Query.Include(basket => basket.Items)
                .ThenInclude(item => item.Product);

            // Project the Basket entity to a BasketDto
            Query.Select(basket => new BasketDto
            {
                Id = basket.Id,
                UserId = basket.UserId,
                // Use ToList() to materialize the IEnumerable into a List
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    Type = item.Product.Type,
                    Brand = item.Product.Brand,
                    BasketId = basket.Id,
                }).ToList() // Convert IEnumerable to List
            });
        }
    }
}