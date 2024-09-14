using Domain;
using RepositoryContracts.Interfaces;

namespace InMemoryRepositories;

public class ModeratorInMemoryRepository : IModeratorRepository
{
    private List<Moderator> moderators = new();
    
    public ModeratorInMemoryRepository()
    {
        AddAsync(new Moderator
        {
            UserId = 1,
            SubforumId= 1,
        });
    }
    
    public Task<Moderator> AddAsync(Moderator moderator)
    {
        if (moderators.FirstOrDefault(m => m.UserId == moderator.UserId && m.SubforumId == moderator.SubforumId) != null)
        {
            throw new Exception("This user is already a moderator of this subforum");
        }
        
        moderators.Add(moderator);
        return Task.FromResult(moderator);
    }


    public Task<Moderator> UpdateAsync(Moderator moderator)
    {
        var existingModerator = moderators.FirstOrDefault(m => m.UserId == moderator.UserId && m.SubforumId == moderator.SubforumId);

        if (existingModerator != null && (existingModerator.UserId != moderator.UserId || existingModerator.SubforumId != moderator.SubforumId))
        {
            throw new Exception("This user is already a moderator of this subforum");
        }

        moderators[moderators.FindIndex(m => m.UserId == moderator.UserId && m.SubforumId == moderator.SubforumId)] = moderator;

        return Task.FromResult(moderator);
    }
    
    public Task<Moderator> DeleteAsync(Moderator moderator)
    {
        var existingModerator = moderators.FirstOrDefault(m => m.UserId == moderator.UserId && m.SubforumId == moderator.SubforumId);
        if (existingModerator == null)
        {
            throw new Exception("Moderator not found");
        }
        
        moderators.Remove(existingModerator);
        
        return Task.FromResult(moderator);
    }
    
    public Task<Moderator> GetByIdAsync(int userId, int subforumId)
    {
        var moderator = moderators.FirstOrDefault(m => m.UserId == userId && m.SubforumId == subforumId);
        return Task.FromResult(moderator);
    }
    
    public IQueryable<Moderator> GetAll()
    {
        return moderators.AsQueryable();
    }
    
    private void CheckIfModeratorExists(Moderator moderator)
    {
     
    }
}