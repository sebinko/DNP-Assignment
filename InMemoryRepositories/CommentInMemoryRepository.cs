using Domain;
using RepositoryContracts.Interfaces;

namespace InMemoryRepositories;

public class CommentInMemoryRepository: ICommentRepository
{
    private List<Comment> comments = new();
    
    public CommentInMemoryRepository()
    {
        this.AddAsync(new Comment
        {
            Body= "This is a comment",
            UserId = 1,
            CommentableType = typeof(Post).ToString(),
            CommentableId = 1,
        });
        
        this.AddAsync(new Comment
        {
            Body= "This is a comment",
            UserId = 1,
            CommentableType = typeof(Post).ToString(),
            CommentableId = 1,
        });
            
        this.AddAsync(new Comment
        {
            Body= "This is a comment",
            UserId = 1,
            CommentableType = typeof(Comment).ToString(),
            CommentableId = 1,
        });
    }
    
    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Count + 1;
        
        comment.CreatedAt = DateTime.Now;
        comment.UpdatedAt = DateTime.Now;
        
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task<Comment> UpdateAsync(Comment comment)
    {
        var existingComment = comments.FirstOrDefault(c => c.Id == comment.Id);
        if (existingComment == null)
        {
            throw new Exception("Comment not found");
        }
        
        comment.UpdatedAt = DateTime.Now;

        comments.Remove(existingComment);
        comments.Add(comment);
        
        return Task.FromResult(comment);
    }

    public Task<Comment> DeleteAsync(Comment comment)
    {
        var existingComment = comments.FirstOrDefault(c => c.Id == comment.Id);
        if (existingComment == null)
        {
            throw new Exception("Comment not found");
        }

        comments.Remove(existingComment);
        
        return Task.FromResult(comment);
    }

    public Task<Comment> GetByIdAsync(int id)
    {
        var comment = comments.FirstOrDefault(c => c.Id == id);
        
        if (comment == null)
        {
            throw new Exception("Comment not found");
        }
        
        return Task.FromResult(comment);
    }

    public IQueryable<Comment> GetAll()
    {
        return comments.AsQueryable();
    }
}