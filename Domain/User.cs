namespace Domain;

public class User
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}