using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IBasketItemService
{
    public Task<ServiceResponse<List<BasketItemDto>>> GetBasketItems(Guid basketId, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddBasketItem(BasketItemDto basketItemDto, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateBasketItem(Guid id, BasketItemDto basketItemDto, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteBasketItem(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<BasketItemDto>> GetBasketItem(Guid id, CancellationToken cancellationToken = default);
}