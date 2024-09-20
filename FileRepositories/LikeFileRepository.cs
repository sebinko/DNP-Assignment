using System.Text.Json;
using Domain;
using RepositoryContracts.Interfaces;

namespace FileRepositories;

public class LikeFileRepository : ILikeRepository
{
    private HashSet<Like> _likes = new();

    public LikeFileRepository()
    {
        _ = ReadFileAsync();
    }

    public async Task<Like> AddAsync(Like like)
    {
        await ReadFileAsync();

        like.Id = _likes.Any() ? _likes.Max(c => c.Id) + 1 : 1;

        like.CreatedAt = DateTime.Now;
        like.UpdatedAt = DateTime.Now;

        _likes.Add(like);
        await WriteFileAsync();

        return like;
    }

    public async Task<Like> UpdateAsync(Like like)
    {
        await ReadFileAsync();

        var existingLike = _likes.FirstOrDefault(c => c.Id == like.Id);
        if (existingLike == null) throw new Exception("Like not found");

        like.UpdatedAt = DateTime.Now;

        _likes.Remove(existingLike);
        _likes.Add(like);

        await WriteFileAsync();

        return like;
    }

    public async Task<Like> DeleteAsync(Like like)
    {
        await ReadFileAsync();

        var existingLike = _likes.FirstOrDefault(c => c.Id == like.Id);
        if (existingLike == null) throw new Exception("Like not found");

        _likes.Remove(existingLike);

        await WriteFileAsync();

        return like;
    }


    public async Task<Like> GetByIdAsync(int id)
    {
        await ReadFileAsync();

        var like = _likes.FirstOrDefault(c => c.Id == id);

        if (like == null) throw new Exception("Like not found");

        return like;
    }

    public IQueryable<Like> GetAll()
    {
        ReadFileAsync().GetAwaiter().GetResult();

        return _likes.AsQueryable();
    }

    private async Task WriteFileAsync()
    {
        await File.WriteAllTextAsync("likes.json", JsonSerializer.Serialize(_likes));
    }

    private async Task ReadFileAsync()
    {
        if (!File.Exists("likes.json")) return;

        _likes = JsonSerializer.Deserialize<HashSet<Like>>(await File.ReadAllTextAsync("likes.json"));
    }
}