using CLI.UI.Utilities;
using Domain.Validation;
using RepositoryContracts.Interfaces;

namespace CLI.UI.ManagePosts;

public class UpdatePostView (IPostRepository postRepository, IUserRepository userRepository, ISubforumRepository subforumRepository):IView
{
    public async Task Run()
    {
        Console.Clear();
        
        try
        {
            PrettyConsole.WriteQuestion("Enter post id:");
            var id = int.Parse(Console.ReadLine() ?? throw new Exception("Id cannot be null"));
            var post = await postRepository.GetByIdAsync(id);
            
            PrettyConsole.WriteQuestion("Title:");
            var title = Console.ReadLine() ?? throw new Exception("Title is required");

            PrettyConsole.WriteQuestion("Body:");
            var body = Console.ReadLine() ?? throw new Exception("Body is required");

            PrettyConsole.WriteQuestion("User ID:");
            var userId = int.Parse(Console.ReadLine() ?? throw new Exception("User ID is required"));

            PrettyConsole.WriteQuestion("Subforum ID:");
            var subforumId = int.Parse(Console.ReadLine() ?? throw new Exception("Subforum ID is required"));

            PostValidation postValidation = new(userRepository, subforumRepository);
            
            await postValidation.ValidatePost(title, body, userId, subforumId);

            post.Title = title;
            post.Body = body;
            post.UserId = userId;
            post.SubforumId = subforumId;

            await postRepository.UpdateAsync(post);
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError(e.Message);
        }
    }
    
}