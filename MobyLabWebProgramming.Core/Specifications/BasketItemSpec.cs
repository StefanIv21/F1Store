using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class BasketItemSpec : Specification<BasketItem, BasketItemDto>
{
    public BasketItemSpec(bool orderByCreatedAt = false) =>
        Query.Select(e => new()
            {
                ProductId = e.ProductId,
                Quantity = e.Quantity,
                Name = e.Product.Name,
                Price = e.Product.Price,
                Type = e.Product.Type,
                Brand = e.Product.Brand,
                BasketId = e.BasketId
            })
            .OrderByDescending(x => x.CreatedAt, orderByCreatedAt);
    
    public BasketItemSpec(Guid id) : this() => Query.Where(e => e.Id == id);
    
    public BasketItemSpec(Guid id,bool check) : this() => Query.Where(e => e.BasketId == id);

    
    
}