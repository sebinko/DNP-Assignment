using Domain;
using RepositoryContracts.Interfaces;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    private List<Post> posts = new();
    
    public PostInMemoryRepository()
    {
        this.AddAsync(new Post
        {
            Title = "This is a post",
            Body = "This is a post",
            UserId = 1,
            SubforumId = 1,
        });
    }
    
    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Count + 1;
        
        post.CreatedAt = DateTime.Now;
        post.UpdatedAt = DateTime.Now;
        
        posts.Add(post);
        return Task.FromResult(post);
    }
    
    public Task<Post> UpdateAsync(Post post)
    {
        var existingPost = posts.FirstOrDefault(p => p.Id == post.Id);
        if (existingPost == null)
        {
            throw new Exception("Post not found");
        }
        
        post.UpdatedAt = DateTime.Now;

        posts.Remove(existingPost);
        posts.Add(post);
        
        return Task.FromResult(post);
    }
    
    public Task<Post> DeleteAsync(Post post)
    {
        var existingPost = posts.FirstOrDefault(p => p.Id == post.Id);
        if (existingPost == null)
        {
            throw new Exception("Post not found");
        }
        
        posts.Remove(existingPost);
        
        return Task.FromResult(post);
    }
    
    public Task<Post> GetByIdAsync(int id)
    {
        var post = posts.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(post);
    }
    
    public IQueryable<Post> GetAll()
    {
        return posts.AsQueryable();
    }
}