namespace MobyLabWebProgramming.Core.Entities;

public class Address : BaseEntity
{
    public string AddressLine1 { get; set; } = null!;
    public string AddressLine2 { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string Country { get; set; } = null!;
    
    public Guid UserId { get; set; }
    
    public User User { get; set; } = null!;
    
    
}