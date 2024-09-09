using Domain;

namespace RepositoryContracts.Interfaces;

public interface ISubforumRepository
{
    public Task<Subforum> AddAsync(Subforum subforum);
    public Task<Subforum> UpdateAsync(Subforum subforum);
    public Task<Subforum> DeleteAsync(Subforum subforum);
    public Task<Subforum> GetByIdAsync(int id);
    public IQueryable<Subforum> GetAll();
}