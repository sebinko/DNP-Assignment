namespace Domain;

public class Subforum
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int OwnerUserId { get; set; }
}