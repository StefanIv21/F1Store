using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IProductService
{
    public Task<ServiceResponse<PagedResponse<ProductDto>>> GetProducts(PaginationSearchQueryParams productParams, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> AddProduct(ProductDto product, CancellationToken cancellationToken = default);
   
    public Task<ServiceResponse> UpdateProduct(ProductDto product, Guid id ,CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse> DeleteProduct(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<ProductDto>> GetProduct(Guid id, CancellationToken cancellationToken = default);
}
