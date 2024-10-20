using Domain;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts.Interfaces;
using WebAPI.DTOs;

namespace WebAPI.Controllers;

[Route("likes")]
public class LikesController(
    ILikeRepository likeRepository,
    IUserRepository userRepository,
    IPostRepository postRepository,
    ICommentRepository commentRepository) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Like like)
    {
        await userRepository.GetByIdAsync(like.UserId);
        if (like.LikeableType == typeof(Post).ToString())
            await postRepository.GetByIdAsync(like.LikeableId);
        else if (like.LikeableType == typeof(Comment).ToString())
            await commentRepository.GetByIdAsync(like.LikeableId);

        await likeRepository.AddAsync(like);
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetLikesDto getLikesDto)
    {
        var likes = likeRepository.GetAll();
        
        if (getLikesDto.UserId != null)
            likes = likes.Where(l => l.UserId == getLikesDto.UserId);
        if (getLikesDto.Value != null)
            likes = likes.Where(l => l.Value == getLikesDto.Value);
            
        likes = likes.Where(l => l.LikeableType == getLikesDto.LikeableType && l.LikeableId == getLikesDto.LikeableId);
        
        return Ok(likes);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var like = await likeRepository.GetByIdAsync(id);
        await likeRepository.DeleteAsync(like);
        return Ok();
    }
}