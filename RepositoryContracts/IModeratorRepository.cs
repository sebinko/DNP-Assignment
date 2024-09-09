using Domain;

namespace RepositoryContracts.Interfaces;

public interface IModeratorRepository
{
    public Task<Moderator> AddAsync(Moderator moderator);
    public Task<Moderator> UpdateAsync(Moderator moderator);
    public Task<Moderator> DeleteAsync(Moderator moderator);
    public Task<Moderator> GetByIdAsync(int userId, int subforumId);
    public IQueryable<Moderator> GetAll();
}