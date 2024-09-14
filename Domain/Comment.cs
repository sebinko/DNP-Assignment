namespace Domain;

public class Comment
{
    public int Id { get; set; }
    public required string Body { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
   
    public required int UserId { get; set; }
    public required int CommentableId { get; set; }
    public string CommentableType { get; set; }
}