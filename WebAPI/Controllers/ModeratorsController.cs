using Domain;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts.Interfaces;
using SharedLib.Exceptions;

namespace WebAPI.Controllers;

[Route("moderators")]
public class ModeratorsController(IModeratorRepository moderatorRepository, ISubforumRepository subforumRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(int subforumId)
    {
        var subforum = await subforumRepository.GetByIdAsync(subforumId);
        
        if (subforum == null)
            throw new NotFoundException(typeof(Subforum));
        
        var moderators = moderatorRepository.GetAll();
        
        moderators = moderators.Where(m => m.SubforumId == subforumId);

        return Ok(moderators);
    }

    [HttpPost]
    public async Task<IActionResult> Create(int subforumId, [FromBody] Moderator moderator)
    {
        var subforum = await subforumRepository.GetByIdAsync(subforumId);
        
        if (subforum == null)
            throw new NotFoundException(typeof(Subforum));
        
        moderator.SubforumId = subforumId;
        await moderatorRepository.AddAsync(moderator);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int subforumId, [FromBody] Moderator moderator)
    {
        var subforum = await subforumRepository.GetByIdAsync(subforumId);
        
        if (subforum == null)
            throw new NotFoundException(typeof(Subforum));
        
        if (moderator.SubforumId != subforumId)
            throw new NotFoundException(typeof(Moderator));

        await moderatorRepository.DeleteAsync(moderator);

        return Ok();
    }
}