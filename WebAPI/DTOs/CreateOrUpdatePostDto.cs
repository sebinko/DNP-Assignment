namespace WebAPI.DTOs;

public class CreateOrUpdatePostDto
{
    public required string Title { get; set; }
    public required string Body { get; set; }
    public required int UserId { get; set; }
    public required int SubforumId { get; set; }
}