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

public class FeedbackService(IRepository<WebAppDatabaseContext> repository,WebAppDatabaseContext context,IHttpContextAccessor httpContextAccessor) : IFeedbackService
{
    public async Task<ServiceResponse> AddFeedback(Guid id ,FeedBackDto feedback, CancellationToken cancellationToken = default)
    {
        await repository.AddAsync(new Feedback
        {
            UserId = id,
            Rating = feedback.Rating,
            Comment = feedback.Comment,
            Category = feedback.Category,
            Bought = feedback.Bought,
            Id = Guid.NewGuid() 
        }, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }
    
    public async Task<ServiceResponse> DeleteFeedback(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await repository.DeleteAsync<Feedback>(id, cancellationToken)>0;
        return result ? ServiceResponse.ForSuccess() : ServiceResponse.FromError(new ErrorMessage(HttpStatusCode.InternalServerError, "Feedback could not be deleted"));
    }
    
    public async Task<ServiceResponse> UpdateFeedback(Guid userId,Guid id, FeedBackDto feedback, CancellationToken cancellationToken = default)
    {
        var existingFeedback = await repository.GetAsync<Feedback>(new FeedBackSpec(id), cancellationToken);
        
        if (existingFeedback == null)
        {
            return ServiceResponse.FromError(CommonErrors.FeedbackNotFound);
        }
        
        existingFeedback.Rating = feedback.Rating;
        existingFeedback.Comment = feedback.Comment;
        existingFeedback.Category = feedback.Category;
        existingFeedback.Bought = feedback.Bought;
        existingFeedback.UserId = userId;
        
        await repository.UpdateAsync(existingFeedback, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }
    
    public async Task<ServiceResponse<List<FeedBackDto>>> GetFeedbacks(Guid userId, CancellationToken cancellationToken = default)
    {
        var result = await repository.ListAsync(new FeedBackSpec(userId,true), cancellationToken);
        if (result == null)
        {
            return ServiceResponse.FromError<List<FeedBackDto>>(CommonErrors.FeedbackNotFound);
        }
        return ServiceResponse.ForSuccess(result);
    }
    
    
    
}