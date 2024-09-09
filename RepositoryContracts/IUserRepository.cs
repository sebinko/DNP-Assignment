using Domain;

namespace RepositoryContracts.Interfaces;

public interface IUserRepository
{
    public Task<User> AddAsync(User user);
    public Task<User> UpdateAsync(User user);
    public Task<User> DeleteAsync(User user);
    public Task<User> GetByIdAsync(int id);
    public IQueryable<User> GetAll();
}