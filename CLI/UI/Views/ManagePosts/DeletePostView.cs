using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI.Views.ManagePosts;

public class DeletePostView(IPostRepository postRepository) : IView
{
    public async Task Run()
    {
        Console.Clear();

        try
        {
            PrettyConsole.WriteQuestion("Enter post id:");

            var id = int.Parse(Console.ReadLine() ?? throw new Exception("Id cannot be null"));
            var post = await postRepository.GetByIdAsync(id);

            await postRepository.DeleteAsync(post);
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError($"Error:{e.Message}");
        }
    }
}