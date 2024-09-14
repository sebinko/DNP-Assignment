using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI.ManagePosts;

public class ListPostView(IPostRepository postRepository) : IView
{
    public Task Run()
    {
        try
        {
            Console.Clear();

            var posts = postRepository.GetAll();

            PrettyConsole.PrintTable(posts.ToList(), Level.Success);
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError(e.Message);
        }

        return Task.CompletedTask;
    }
}