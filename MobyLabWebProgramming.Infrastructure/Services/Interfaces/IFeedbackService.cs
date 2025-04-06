using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IFeedbackService
{
    public Task<ServiceResponse> AddFeedback(Guid id,FeedBackDto feedbackDto, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteFeedback(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateFeedback(Guid userId,Guid id, FeedBackDto feedbackDto, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<List<FeedBackDto>>> GetFeedbacks(Guid id, CancellationToken cancellationToken = default);
}