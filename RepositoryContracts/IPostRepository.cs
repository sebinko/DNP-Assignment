using Domain;

namespace RepositoryContracts.Interfaces;

public interface IPostRepository
{
    public Task<Post> AddAsync(Post post);
    public Task<Post> UpdateAsync(Post post);
    public Task<Post> DeleteAsync(Post post);
    public Task<Post> GetByIdAsync(int id);
    public IQueryable<Post> GetAll();
}