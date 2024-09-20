namespace Domain;

public class Post
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public required int UserId { get; set; }
    public required int SubforumId { get; set; }
}