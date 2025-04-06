using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedBackController(IUserService userService, WebAppDatabaseContext context,IFeedbackService feedbackService):
AuthorizedController(userService)
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<List<FeedBackDto>>>>  GetUserFeedback()
    {
        try
        {
            var currentUser = await GetCurrentUser();
            return currentUser.Result != null ?
                FromServiceResponse(await feedbackService.GetFeedbacks(currentUser.Result.Id)) :
                ErrorMessageResult<List<FeedBackDto>>(currentUser.Error);
        } 
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Error retrieving feedbacks.", Detail = e.Message });
        }
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>>  AddFeedback([FromBody] FeedBackDto feedback)
    {
        try
        {
            var currentUser = await GetCurrentUser();
            return currentUser.Result != null ?
                FromServiceResponse(await feedbackService.AddFeedback(currentUser.Result.Id, feedback)) :
                ErrorMessageResult(currentUser.Error);
            
           
            
        } 
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Error adding feedback.", Detail = e.Message });
        }
    }
    
    [Authorize(Roles = "Admin,Personnel")]
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> UpdateFeedback(Guid id, [FromBody] FeedBackDto feedback)
    {
        try
        {
            var currentUser = await GetCurrentUser();
            return currentUser.Result != null ?
                FromServiceResponse(await feedbackService.UpdateFeedback(currentUser.Result.Id, id, feedback)) :
                ErrorMessageResult(currentUser.Error);
        } 
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Error updating feedback.", Detail = e.Message });
        }
    
    }
    
    [Authorize(Roles = "Admin,Personnel")]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> DeleteFeedback(Guid id)
    {
        try
        {
            var currentUser = await GetCurrentUser();
            return currentUser.Result != null ?
                FromServiceResponse(await feedbackService.DeleteFeedback(id)) :
                ErrorMessageResult(currentUser.Error);
           
        } 
        catch (Exception e)
        {
            return StatusCode(500, new { Message = "Error deleting feedback.", Detail = e.Message });
        }
    }
    

   
    
}