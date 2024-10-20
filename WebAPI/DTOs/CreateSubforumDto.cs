namespace WebAPI.DTOs;

public class CreateSubforumDto
{
    public required string Title { get; set; }
    public required string Description{ get; set; }
    public required int OwnerId { get; set; }
    
}