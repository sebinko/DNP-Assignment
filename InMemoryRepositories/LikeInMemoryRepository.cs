using Domain;
using RepositoryContracts.Interfaces;

namespace InMemoryRepositories;

public class LikeInMemoryRepository : ILikeRepository
{
    private List<Like> likes = new();
    
    public LikeInMemoryRepository()
    {
        this.AddAsync(new Like
        {
            UserId = 1,
            LikeableType = typeof(Post).ToString(),
            LikeableId = 1,
        });
            
        this.AddAsync(new Like
        {
            UserId = 1,
            LikeableType = typeof(Comment).ToString(),
            LikeableId = 1,
        });
    }
    
    public Task<Like> AddAsync(Like like)
    {
        like.Id = likes.Count + 1;
        
        like.CreatedAt = DateTime.Now;
        like.UpdatedAt = DateTime.Now;
        
        likes.Add(like);
        return Task.FromResult(like);
    }
    
    public Task<Like> UpdateAsync(Like like)
    {
        var existingLike = likes.FirstOrDefault(l => l.Id == like.Id);
        if (existingLike == null)
        {
            throw new Exception("Like not found");
        }
        
        like.UpdatedAt = DateTime.Now;

        likes.Remove(existingLike);
        likes.Add(like);
        
        return Task.FromResult(like);
    }
    
    public Task<Like> DeleteAsync(Like like)
    {
        var existingLike = likes.FirstOrDefault(l => l.Id == like.Id);
        if (existingLike == null)
        {
            throw new Exception("Like not found");
        }

        likes.Remove(existingLike);
        
        return Task.FromResult(like);
    }
    
    public Task<Like> GetByIdAsync(int id)
    {
        var like = likes.FirstOrDefault(l => l.Id == id);
        return Task.FromResult(like);
    }
    
    public IQueryable<Like> GetAll()
    {
        return likes.AsQueryable();
    }
}