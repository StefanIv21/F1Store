using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public class AddressSpec : Specification<Address, AddressDto>
{
    
    public AddressSpec(bool orderByCreatedAt = false) =>
        Query.Select(e => new()
            {
                AddressLine1 = e.AddressLine1,
                AddressLine2 = e.AddressLine2,
                State = e.State,
                City = e.City,
                ZipCode = e.ZipCode,
                Country = e.Country
            })
            .OrderByDescending(x => x.CreatedAt, orderByCreatedAt);
    
    
    public AddressSpec(Guid id) : this() => Query.Where(e => e.UserId == id);

}