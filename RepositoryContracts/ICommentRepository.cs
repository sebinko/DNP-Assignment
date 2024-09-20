using Domain;

namespace RepositoryContracts.Interfaces;

public interface ICommentRepository
{
    public Task<Comment> AddAsync(Comment comment);
    public Task<Comment> UpdateAsync(Comment comment);
    public Task<Comment> DeleteAsync(Comment comment);
    public Task<Comment> GetByIdAsync(int id);
    public IQueryable<Comment> GetAll();
}