using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.Constants;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class AddressService(IRepository<WebAppDatabaseContext> repository,WebAppDatabaseContext context,IHttpContextAccessor httpContextAccessor,IMailService mailService) : IAddressService
{
    public async Task<ServiceResponse<AddressDto>> GetUserAddress(Guid id,
        CancellationToken cancellationToken = default)
    {

        var result = await repository.GetAsync(new AddressSpec(id), cancellationToken);

        return result != null
            ? ServiceResponse.ForSuccess(result)
            : ServiceResponse.FromError<AddressDto>(CommonErrors.AddressNotFound);

    }
    
    public async Task<ServiceResponse> AddAddress(Guid id,AddressDto address,String email ,CancellationToken cancellationToken = default)
    {
        await repository.AddAsync(new Address
        {
            City = address.City,
            Country = address.Country,
            ZipCode = address.ZipCode,
            AddressLine1 = address.AddressLine1,
            AddressLine2 = address.AddressLine2,
            State = address.State,
            UserId = id
            
        }, cancellationToken);
        
        await mailService.SendMail(email, "Welcome!", MailTemplates.UserAddTemplate(address.AddressLine1), true, "My App", cancellationToken); 

        return ServiceResponse.ForSuccess();
    }
    
    public async Task<ServiceResponse> UpdateAddress(Guid id,AddressDto address, CancellationToken cancellationToken = default)
    {
        
        var existingAddress = await repository.GetAsync<Address>(new AddressSpec(id), cancellationToken);

        if (existingAddress == null)
        {
            return ServiceResponse.FromError(CommonErrors.AddressNotFound);
        }

        existingAddress.City = address.City;
        existingAddress.Country = address.Country;
        existingAddress.ZipCode = address.ZipCode;
        existingAddress.AddressLine1 = address.AddressLine1;
        existingAddress.AddressLine2 = address.AddressLine2;
        existingAddress.State = address.State;

        await repository.UpdateAsync(existingAddress, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
    
    public async Task<ServiceResponse> DeleteAddress(Guid id, CancellationToken cancellationToken = default)
    {
        var existingAddress = await repository.GetAsync<Address>(new AddressSpec(id), cancellationToken);
        if (existingAddress == null)
        {
            return ServiceResponse.FromError(CommonErrors.AddressNotFound);
        }
        var deleteResult = await repository.DeleteAsync<Address>(existingAddress.Id, cancellationToken) > 0;
        return deleteResult ? ServiceResponse.ForSuccess() : ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.InternalServerError, "Address could not be deleted"));

    }
}