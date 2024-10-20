using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs;

public class CreateUserDto
{
    [Required]
    public required string Username { get; set; }
    [Required]
    public required string Password { get; set; }
}