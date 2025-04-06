namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class FeedBackDto
{
    public Guid Id { get; set; }
    public string Category { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public bool Bought { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}