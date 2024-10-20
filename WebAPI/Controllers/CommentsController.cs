using Domain;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts.Interfaces;
using WebAPI.DTOs;

namespace WebAPI.Controllers;

[Route("comments")]
public class CommentsController(
    ICommentRepository commentRepository,
    IUserRepository userRepository,
    ILikeRepository likeRepository) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Comment comment)
    {
        await userRepository.GetByIdAsync(comment.UserId);
        await commentRepository.AddAsync(comment);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetCommentsDto getCommentsDto)
    {
        var comments = commentRepository.GetAll();

        if (getCommentsDto.UserId != null)
            comments = comments.Where(c => c.UserId == getCommentsDto.UserId);
        
        comments = comments.Where(c => c.CommentableType == getCommentsDto.CommentableType && c.CommentableId == getCommentsDto.CommentableId);

        return Ok(comments);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var comment = await commentRepository.GetByIdAsync(id);
        await commentRepository.DeleteAsync(comment);
        return Ok();
    }
}