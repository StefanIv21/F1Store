using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Backend.Entities;
using MobyLabWebProgramming.Core.DataTransferObjects;

namespace MobyLabWebProgramming.Core.Specifications;

public class ProductSpec : Specification<Product, ProductDto>
{
    public ProductSpec(bool orderByCreatedAt = false) =>
        Query.Select(e => new()
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
            Price = e.Price,
            Type = e.Type,
            Brand = e.Brand,
            Stock = e.Stock
        }) .OrderByDescending(x => x.CreatedAt, orderByCreatedAt);
    
    public ProductSpec(Guid id) : this() => Query.Where(e => e.Id == id);
    
    public ProductSpec(string? search) : this(true)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Name, searchExpr));
    }
    
}