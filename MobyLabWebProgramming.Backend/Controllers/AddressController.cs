using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Constants;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;


[ApiController]
[Route("api/[controller]")]

public class AddressController(IUserService userService, WebAppDatabaseContext context, IMailService mailService,IAddressService addressService)
    : AuthorizedController(userService)
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<AddressDto>>>  GetUserAddress()
    {
        try
        {
            var currentUser = await GetCurrentUser();
            return currentUser.Result != null ?
                FromServiceResponse(await addressService.GetUserAddress(currentUser.Result.Id)) :
                ErrorMessageResult<AddressDto>(currentUser.Error);
        } 
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Error retrieving address.", Detail = e.Message });
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>>  AddAddress([FromBody] AddressDto address)
    {
        try
        {
            var currentUser = await GetCurrentUser();
            return currentUser.Result != null ?
                FromServiceResponse(await addressService.AddAddress(currentUser.Result.Id, address,currentUser.Result.Email)) :
                ErrorMessageResult(currentUser.Error);
        } 
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Error adding the address.", Detail = e.Message });
        }
    }
    
    
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> UpdateAddress([FromBody] AddressDto address)
    {
        try
        {
            var currentUser = await GetCurrentUser();
            return currentUser.Result != null ?
                FromServiceResponse(await addressService.UpdateAddress(currentUser.Result.Id, address)) :
                ErrorMessageResult(currentUser.Error);
        }
        catch (Exception ex)
        {
            // Return a 500 status code with an error message
            return StatusCode(500, new { Message = "Error updating the address.", Detail = ex.Message });
        }
    }
    
    [Authorize]
    [HttpDelete]
    public async Task<ActionResult<RequestResponse>> DeleteAddress()
    {
        try
        {
            var currentUser = await GetCurrentUser();
            return currentUser.Result != null ?
                FromServiceResponse(await addressService.DeleteAddress(currentUser.Result.Id)) :
                ErrorMessageResult(currentUser.Error);
            
        }
        catch (Exception ex)
        {
            // Return a 500 status code with an error message
            return StatusCode(500, new { Message = "Error deleting the address.", Detail = ex.Message });
        }
    }
}

 