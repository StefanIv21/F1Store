using Microsoft.AspNetCore.Http;
using MobyLabWebProgramming.Backend.Entities;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class ProductService(IRepository<WebAppDatabaseContext> repository,WebAppDatabaseContext context,IHttpContextAccessor httpContextAccessor) : IProductService
{
    public async Task<ServiceResponse<PagedResponse<ProductDto>>> GetProducts(PaginationSearchQueryParams productParams, CancellationToken cancellationToken = default)
    {
       
        var result = await repository.PageAsync(productParams, new ProductSpec(productParams.Search), cancellationToken);
        httpContextAccessor.HttpContext?.Response.AddPagination(new MetaData 
        {
            TotalCount = result.TotalCount,
            PageSize = result.PageSize,
            CurrentPage = result.Page,
            TotalPages = (int)Math.Ceiling(result.TotalCount / (double)result.PageSize)
        });
        
        return ServiceResponse.ForSuccess(result);
        
    }
    
    public async Task<ServiceResponse<ProductDto>> GetProduct(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await repository.GetAsync(new ProductSpec(id), cancellationToken);
        
        return product != null ? 
            ServiceResponse.ForSuccess(product) : 
            ServiceResponse.FromError<ProductDto>(CommonErrors.ProducNotFound);
    }
    
    public async Task<ServiceResponse> AddProduct(ProductDto product, CancellationToken cancellationToken = default)
    {
        await repository.AddAsync(new Product
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Type = product.Type,
            Brand = product.Brand,
            Stock = product.Stock,
            Id = Guid.NewGuid()
        }, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }
    
    public async Task<ServiceResponse> UpdateProduct(ProductDto product, Guid id ,CancellationToken cancellationToken = default)
    {
        var existingProduct = await repository.GetAsync<Product>(new ProductSpec(id), cancellationToken);
        
        if (existingProduct == null)
        {
            return ServiceResponse.FromError(CommonErrors.ProducNotFound);
        }
        
        existingProduct.Name = !string.IsNullOrEmpty(product.Name) ? product.Name : existingProduct.Name;
        existingProduct.Description = !string.IsNullOrEmpty(product.Description) ? product.Description : existingProduct.Description;
        existingProduct.Price = product.Price != 0 ? product.Price : existingProduct.Price; // Assuming 0 means no change
        existingProduct.Type = !string.IsNullOrEmpty(product.Type) ? product.Type : existingProduct.Type;
        existingProduct.Brand = !string.IsNullOrEmpty(product.Brand) ? product.Brand : existingProduct.Brand;
        existingProduct.Stock = product.Stock > 0 ? product.Stock : existingProduct.Stock; // Assuming 0 means no change

        
        await repository.UpdateAsync(existingProduct, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }
    
    public async Task<ServiceResponse> DeleteProduct(Guid id, CancellationToken cancellationToken = default)
    {
        await repository.DeleteAsync<Product>(id, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }
    
}