using System.ComponentModel.DataAnnotations;
using WebAPI.Validation;

namespace WebAPI.DTOs;

public class GetLikesDto
{
    public int? UserId { get; set; }
    
    [Required]
    [In("Domain.Post", "Domain.Comment")]
    public string LikeableType { get; set; }
    [Required]
    public int LikeableId { get; set; }
    
    public int? Value { get; set; }
    
}