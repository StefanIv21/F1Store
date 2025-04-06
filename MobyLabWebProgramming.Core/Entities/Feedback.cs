namespace MobyLabWebProgramming.Core.Entities;

public class Feedback : BaseEntity
{
    public string Category { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public bool Bought { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    
}