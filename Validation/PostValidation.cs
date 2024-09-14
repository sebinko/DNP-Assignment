using RepositoryContracts.Interfaces;

namespace Domain.Validation;

// TODO REMOVE THIS WHOLE , VERY BAD ( good for the future, but not right now)
public class PostValidation (IUserRepository userRepository, ISubforumRepository subforumRepository)
{
    private void ValidateTitle(string? title)
    {
        if (string.IsNullOrEmpty(title))
        {
            throw new Exception("Title is required");
        }
        
        if (title.Length > 100)
        {
            throw new Exception("Title is too long");
        }
    }
   
    private void ValidateBody(string? body)
    {
        if (string.IsNullOrEmpty(body))
        {
            throw new Exception("Body is required");
        }
        
        if (body.Length > 1000)
        {
            throw new Exception("Body is too long");
        }
    }
    
    private async Task ValidateUserId(int userId)
    {
        await userRepository.GetByIdAsync(userId);
    }
    
    private async Task ValidateSubforumId(int subforumId)
    {
        await subforumRepository.GetByIdAsync(subforumId);
    }
    
    public async Task ValidatePost(string? title, string? body, int userId, int subforumId)
    {
        ValidateTitle(title);
        ValidateBody(body);
        await ValidateUserId(userId);
        await ValidateSubforumId(subforumId);
    }
    
}