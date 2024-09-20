using System.Text.Json;
using Domain;
using RepositoryContracts.Interfaces;

namespace InMemoryRepositories;

public class PostFileRepository : IPostRepository
{
    private HashSet<Post> _posts = new();

    public PostFileRepository()
    {
        ReadFileAsync().GetAwaiter().GetResult();
    }

    public async Task<Post> AddAsync(Post post)
    {
        await ReadFileAsync();

        post.Id = _posts.Any() ? _posts.Max(p => p.Id) + 1 : 1;

        post.CreatedAt = DateTime.Now;
        post.UpdatedAt = DateTime.Now;

        _posts.Add(post);

        await WriteFileAsync();

        return post;
    }

    public async Task<Post> UpdateAsync(Post post)
    {
        await ReadFileAsync();

        var existingPost = _posts.FirstOrDefault(p => p.Id == post.Id);
        if (existingPost == null) throw new Exception("Post not found");

        post.UpdatedAt = DateTime.Now;

        _posts.Remove(existingPost);
        _posts.Add(post);

        await WriteFileAsync();


        return post;
    }

    public async Task<Post> DeleteAsync(Post post)
    {
        await ReadFileAsync();

        var existingPost = _posts.FirstOrDefault(p => p.Id == post.Id);
        if (existingPost == null) throw new Exception("Post not found");

        _posts.Remove(existingPost);

        await WriteFileAsync();

        return post;
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        await ReadFileAsync();

        var post = _posts.FirstOrDefault(p => p.Id == id);

        if (post == null) throw new Exception("Post not found");

        return post;
    }

    public IQueryable<Post> GetAll()
    {
        return _posts.AsQueryable();
    }


    private async Task WriteFileAsync()
    {
        await File.WriteAllTextAsync("posts.json", JsonSerializer.Serialize(_posts));
    }

    private async Task ReadFileAsync()
    {
        if (!File.Exists("_moderators.json")) return;

        _posts = JsonSerializer.Deserialize<HashSet<Post>>(await File.ReadAllTextAsync("posts.json"));
    }
}