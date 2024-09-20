using Domain;

namespace RepositoryContracts.Interfaces;

public interface ILikeRepository
{
    public Task<Like> AddAsync(Like like);
    public Task<Like> UpdateAsync(Like like);
    public Task<Like> DeleteAsync(Like like);
    public Task<Like> GetByIdAsync(int id);
    public IQueryable<Like> GetAll();
}