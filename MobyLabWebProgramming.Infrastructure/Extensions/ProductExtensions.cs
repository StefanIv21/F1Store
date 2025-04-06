using MobyLabWebProgramming.Backend.Entities;

namespace MobyLabWebProgramming.Infrastructure.Extensions;

public static class ProductExtensions
{
    public static IQueryable<Product> Search(this IQueryable<Product> query, string search)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return query;
        }
        var lowerCaseSearch = search.Trim().ToLower();
        return query.Where(product => product.Name.ToLower().Contains(lowerCaseSearch));
    }
}