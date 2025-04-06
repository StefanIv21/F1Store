using System.Net;
using Microsoft.AspNetCore.Http;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class BasketItemService(IRepository<WebAppDatabaseContext> repository,WebAppDatabaseContext context,IHttpContextAccessor httpContextAccessor,IMailService mailService) : IBasketItemService
{
    public async Task<ServiceResponse> AddBasketItem(BasketItemDto basketItem, CancellationToken cancellationToken = default)
    {
        var basket = await context.Baskets.FindAsync(basketItem.BasketId);
        if (basket == null)
        {
            return ServiceResponse.FromError(CommonErrors.BasketNotFound);
        }
        var product = await context.Products.FindAsync(basketItem.ProductId);
        if (product == null)
        {
            return ServiceResponse.FromError(CommonErrors.ProducNotFound);
        }
        await repository.AddAsync(new BasketItem
        {
            ProductId = basketItem.ProductId,
            Quantity = basketItem.Quantity,
            BasketId = basketItem.BasketId
        }, cancellationToken);
        
        return ServiceResponse.ForSuccess();

    }

    public async Task<ServiceResponse> UpdateBasketItem(Guid id, BasketItemDto basketItem, CancellationToken cancellationToken = default)
    {
        var existingBasketItem = await repository.GetAsync<BasketItem>(new BasketItemSpec(id), cancellationToken);
        if (existingBasketItem == null)
        {
            return ServiceResponse.FromError(CommonErrors.BasketItemNotFound);
        }
        var product = await context.Products.FindAsync(basketItem.ProductId);
        if (product == null)
        {
            return ServiceResponse.FromError(CommonErrors.ProducNotFound);
        }
        var basket = await context.Baskets.FindAsync(basketItem.BasketId);
        if (basket == null)
        {
            return ServiceResponse.FromError(CommonErrors.BasketNotFound);
        }

        existingBasketItem.ProductId = basketItem.ProductId;
        existingBasketItem.Quantity = basketItem.Quantity;
        existingBasketItem.BasketId = basketItem.BasketId;

        await repository.UpdateAsync(existingBasketItem, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteBasketItem(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await repository.DeleteAsync<BasketItem>(id, cancellationToken) > 0;
        return result ? ServiceResponse.ForSuccess() : ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.InternalServerError, "BasketItem could not be deleted"));
    }
    
    public async Task<ServiceResponse<List<BasketItemDto>>> GetBasketItems(Guid basketId, CancellationToken cancellationToken = default)
    {
        var result = await repository.ListAsync(new BasketItemSpec(basketId,true), cancellationToken);
        return ServiceResponse.ForSuccess(result);
    }
    
    public async Task<ServiceResponse<BasketItemDto>> GetBasketItem(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await repository.GetAsync(new BasketItemSpec(id), cancellationToken);
        return result != null
            ? ServiceResponse.ForSuccess(result)
            : ServiceResponse.FromError<BasketItemDto>(CommonErrors.BasketItemNotFound);
    }
    
    
    
        


    
}