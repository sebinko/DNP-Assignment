namespace Domain;

public class Like
{
    public int Id { get; set; }
    public required int Value { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public required int UserId { get; set; }
    public required int LikeableId { get; set; }
    public string LikeableType { get; set; }
}