using System.Text.Json;
using Domain;
using RepositoryContracts.Interfaces;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private HashSet<Comment> _comments = new();

    public CommentFileRepository()
    {
        _ = ReadFileAsync();
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        await ReadFileAsync();

        comment.Id = _comments.Any() ? _comments.Max(c => c.Id) + 1 : 1;

        comment.CreatedAt = DateTime.Now;
        comment.UpdatedAt = DateTime.Now;

        _comments.Add(comment);
        await WriteFileAsync();

        return comment;
    }

    public async Task<Comment> UpdateAsync(Comment comment)
    {
        await ReadFileAsync();

        var existingComment = _comments.FirstOrDefault(c => c.Id == comment.Id);
        if (existingComment == null) throw new Exception("Comment not found");

        comment.UpdatedAt = DateTime.Now;

        _comments.Remove(existingComment);
        _comments.Add(comment);

        await WriteFileAsync();

        return comment;
    }

    public async Task<Comment> DeleteAsync(Comment comment)
    {
        await ReadFileAsync();

        var existingComment = _comments.FirstOrDefault(c => c.Id == comment.Id);
        if (existingComment == null) throw new Exception("Comment not found");

        _comments.Remove(existingComment);

        await WriteFileAsync();

        return comment;
    }

    public async Task<Comment> GetByIdAsync(int id)
    {
        await ReadFileAsync();

        var comment = _comments.FirstOrDefault(c => c.Id == id);

        if (comment == null) throw new Exception("Comment not found");

        return comment;
    }

    public IQueryable<Comment> GetAll()
    {
        ReadFileAsync().GetAwaiter().GetResult();

        return _comments.AsQueryable();
    }

    private async Task WriteFileAsync()
    {
        await File.WriteAllTextAsync("comments.json", JsonSerializer.Serialize(_comments));
    }

    private async Task ReadFileAsync()
    {
        if (!File.Exists("comments.json")) return;

        _comments = JsonSerializer.Deserialize<HashSet<Comment>>(await File.ReadAllTextAsync("comments.json"));
    }
}