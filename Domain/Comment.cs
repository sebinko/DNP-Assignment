namespace Domain;

public class Comment
{
    public int Id { get; set; }
    public string Body { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
   
    public int UserId { get; set; }
    public int CommentableId { get; set; }
    public string CommentableType { get; set; }
}