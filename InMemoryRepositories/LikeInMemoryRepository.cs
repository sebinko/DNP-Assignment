using Domain;
using RepositoryContracts.Interfaces;

namespace InMemoryRepositories;

public class LikeInMemoryRepository : ILikeRepository
{
    private readonly List<Like> likes = new();

    public LikeInMemoryRepository()
    {
        AddAsync(new Like
        {
            UserId = 1,
            LikeableType = typeof(Post).ToString(),
            LikeableId = 1,
            Value = 1
        });

        AddAsync(new Like
        {
            UserId = 1,
            LikeableType = typeof(Comment).ToString(),
            LikeableId = 1,
            Value = -1
        });
    }

    public Task<Like> AddAsync(Like like)
    {
        like.Id = likes.Any() ? likes.Max(l => l.Id) + 1 : 1;

        like.CreatedAt = DateTime.Now;
        like.UpdatedAt = DateTime.Now;

        likes.Add(like);
        return Task.FromResult(like);
    }

    public Task<Like> UpdateAsync(Like like)
    {
        var existingLike = likes.FirstOrDefault(l => l.Id == like.Id);
        if (existingLike == null) throw new Exception("Like not found");

        like.UpdatedAt = DateTime.Now;

        likes[likes.FindIndex(l => l.Id == like.Id)] = like;

        return Task.FromResult(like);
    }

    public Task<Like> DeleteAsync(Like like)
    {
        var existingLike = likes.FirstOrDefault(l => l.Id == like.Id);
        if (existingLike == null) throw new Exception("Like not found");

        likes.Remove(existingLike);

        return Task.FromResult(like);
    }

    public Task<Like> GetByIdAsync(int id)
    {
        var like = likes.FirstOrDefault(l => l.Id == id);

        if (like == null) throw new Exception("Like not found");

        return Task.FromResult(like);
    }

    public IQueryable<Like> GetAll()
    {
        return likes.AsQueryable();
    }
}