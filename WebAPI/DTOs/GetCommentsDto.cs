using System.ComponentModel.DataAnnotations;
using WebAPI.Validation;

namespace WebAPI.DTOs;

public class GetCommentsDto
{
    public int? UserId { get; set; }
    
    [Required]
    public required int CommentableId { get; set; }
    [Required]
    [In("Domain.Post", "Domain.Comment")]
    public string CommentableType { get; set; }
}