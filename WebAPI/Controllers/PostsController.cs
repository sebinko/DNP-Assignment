using Domain;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts.Interfaces;
using SharedLib.Exceptions;
using WebAPI.DTOs;

namespace WebAPI.Controllers;

[Route("subforum/{subforumId}/posts")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository _postRepository;
    private readonly ISubforumRepository _subforumRepository;

    public PostsController(IPostRepository postRepository, ISubforumRepository subforumRepository)
    {
        _postRepository = postRepository;
        _subforumRepository = subforumRepository;
    }
    
    private async Task<Subforum> GetSubforum(int subforumId)
    {
        var subforum = await _subforumRepository.GetByIdAsync(subforumId);
        if (subforum == null)
        {
            throw new NotFoundException(typeof(Subforum));
        }

        return subforum;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPosts(int subforumId, GetPostsDto? dto)
    {
        await GetSubforum(subforumId);
        
        var posts = _postRepository.GetAll().Where(p => p.SubforumId == subforumId);
        
        if (dto != null)
        {
            if (dto.Title != null)
            {
                posts = posts.Where(p => p.Title.Contains(dto.Title));
            }
            
            if (dto.Content != null)
            {
                posts = posts.Where(p => p.Body.Contains(dto.Content));
            }
            
            if (dto.UserId != null)
            {
                posts = posts.Where(p => p.UserId == dto.UserId);
            }
        }
        
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPost(int subforumId, int id)
    {
        await GetSubforum(subforumId);

        var post = await _postRepository.GetByIdAsync(id);
        
        if (post.SubforumId != subforumId)
        {
            throw new NotFoundException(typeof(Post));
        }
        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> AddPost(int subforumId, [FromBody] CreateOrUpdatePostDto dto)
    {
        await GetSubforum(subforumId);

        var addedPost = await _postRepository.AddAsync(new Post
        {
            Title = dto.Title,
            Body = dto.Body,
            UserId = dto.UserId,
            SubforumId = subforumId
        });

        return Ok(addedPost);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int subforumId, int id, [FromBody] CreateOrUpdatePostDto dto)
    {
        await GetSubforum(subforumId);

        var post = await _postRepository.GetByIdAsync(id);
        if (post.SubforumId != subforumId)
        {
            throw new NotFoundException(typeof(Post));
        }

        post.Title = dto.Title;
        post.Body = dto.Body;
        post.UserId = dto.UserId;

        var updatedPost = await _postRepository.UpdateAsync(post);

        return Ok(updatedPost);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int subforumId, int id)
    {
        await GetSubforum(subforumId);

        var post = await _postRepository.GetByIdAsync(id);
        if (post.SubforumId != subforumId)
        {
            throw new NotFoundException(typeof(Post));
        }

        var deletedPost = await _postRepository.DeleteAsync(post);

        return Ok(deletedPost);
    }
}

