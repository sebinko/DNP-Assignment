using Domain;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts.Interfaces;
using WebAPI.DTOs;

namespace WebAPI.Controllers;

[Route("subforum")]
public class SubforumsController(ISubforumRepository subforumRepository) : ControllerBase
{
    [HttpGet]
    public IActionResult GetSubforums()
    {
        var subforums = subforumRepository.GetAll();
        return Ok(subforums);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubforum(int id)
    {
        var subforum = await subforumRepository.GetByIdAsync(id);

        return Ok(subforum);
    }

    [HttpPost]
    public async Task<IActionResult> AddSubforum([FromBody] CreateSubforumDto dto)
    {
        var subforum = new Subforum
        {
            Title = dto.Title,
            Description = dto.Description,
            OwnerUserId = dto.OwnerId
        };

        await subforumRepository.AddAsync(subforum);
        return CreatedAtAction(nameof(GetSubforum), new { id = subforum.Id }, subforum);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubforum(int id, [FromBody] UpdateSubforumDto dto)
    {
        var subforum = await subforumRepository.GetByIdAsync(id);
        
        Console.WriteLine("DTO");
        Console.WriteLine(dto.Title);
        Console.WriteLine(dto.Description);
        Console.WriteLine("Subforum");
        
        subforum.Title = dto.Title ?? subforum.Title;
        subforum.Description = dto.Description ?? subforum.Description;
        
        await subforumRepository.UpdateAsync(subforum);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubforum(int id)
    {
        var subforum = await subforumRepository.GetByIdAsync(id);

        await subforumRepository.DeleteAsync(subforum);
        return NoContent();
    }
    
    
}