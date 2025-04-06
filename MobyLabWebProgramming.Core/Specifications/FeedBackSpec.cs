using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class FeedBackSpec : Specification<Feedback, FeedBackDto>
{
    public FeedBackSpec(bool orderByCreatedAt = false) =>
        Query.Select(e => new()
            {
                Id = e.Id,
                Category = e.Category,
                Rating = e.Rating,
                Comment = e.Comment,
                Bought = e.Bought
            })
            .OrderByDescending(x => x.CreatedAt, orderByCreatedAt);
    public FeedBackSpec(Guid id) : this() => Query.Where(e => e.Id == id);
    
    public FeedBackSpec(Guid userId,bool check) : this() => Query.Where(e => e.UserId == userId);
    
}