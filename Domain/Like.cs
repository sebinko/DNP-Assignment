namespace Domain;

public class Like
{
    public int Id { get; set; }
    public int Value { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public int UserId { get; set; }
    public int LikeableId { get; set; }
    public string LikeableType { get; set; }
}