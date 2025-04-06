using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IAddressService
{
    public Task<ServiceResponse>  DeleteAddress(Guid id,CancellationToken cancellationToken = default);
    public Task<ServiceResponse>  UpdateAddress(Guid id,AddressDto address, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddAddress(Guid id,AddressDto address,String email ,CancellationToken cancellationToken = default);
    public Task<ServiceResponse<AddressDto>>   GetUserAddress(Guid id,CancellationToken cancellationToken = default);
}