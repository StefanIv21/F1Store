using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IBasketService
{
   public  Task<ServiceResponse<Basket>> AddToBasket(string userId,string? claimUserId,Guid productId,int quantity,CancellationToken cancellationToken = default);
    public Task<Basket?> RetrieveBasket(string userId,CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse> UpdateBasket(string userId,BasketDto basketDto,CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse> DeleteFromBasket(string UserId,Guid productId,int quantity,CancellationToken cancellationToken = default);
}