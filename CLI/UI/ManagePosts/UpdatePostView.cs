using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI.ManagePosts;

public class UpdatePostView(
    IPostRepository postRepository,
    IUserRepository userRepository,
    ISubforumRepository subforumRepository) : IView
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

            post.Title = title;
            post.Body = body;

            await postRepository.UpdateAsync(post);
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError(e.Message);
        }
    }
}