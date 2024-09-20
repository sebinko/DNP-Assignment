namespace Domain;

public class Subforum
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public required int OwnerUserId { get; set; }
}