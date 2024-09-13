using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using CLI.UI.Menu;
using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI;

public class CliApp
{
    private readonly Dictionary<string, IView> _views;

    public CliApp(
        ICommentRepository commentRepository,
        ILikeRepository likeRepository,
        IModeratorRepository moderatorRepository,
        IPostRepository postRepository,
        ISubforumRepository subforumRepository,
        IUserRepository userRepository)
    {
        _views = new Dictionary<string, IView>
        {
            { "1", new ListUserView(userRepository) },
            { "2", new CreateUserView(userRepository) },
            { "3", new UpdateUserView(userRepository) },
            { "4", new DeleteUserView(userRepository) },
            { "5", new ListPostView(postRepository) },
            { "6", new CreatePostView(postRepository, userRepository, subforumRepository) },
            { "7", new UpdatePostView(postRepository, userRepository, subforumRepository) },
            { "8", new DeletePostView(postRepository) },
            { "10", new ExitView() }
        };
    }

    public async Task StartAsync()
    {
        var exit = false;

        while (!exit)
        {
            PrintMenu();

            PrettyConsole.WriteInfo("Enter your choice:");

            var choice = Console.ReadLine();

            if (!string.IsNullOrEmpty(choice) && _views.TryGetValue(choice, out var view))
            {
                await view.Run();

                if (view is ExitView)
                {
                    exit = true;
                }
            }
            else
            {
                Console.Clear();
                PrettyConsole.WriteError($"{choice} - Invalid choice");
            }
        }
    }

    private void PrintMenu()
    {
        foreach (var view in _views)
        {
            var viewName = ParseViewName(view.Value.GetType().Name);
            PrettyConsole.WriteInfo($"{view.Key} - {viewName}");
        }
    }

    private static string ParseViewName(string viewName)
    {
        if (viewName.EndsWith("View"))
        {
            viewName = viewName.Substring(0, viewName.Length - 4);
        }

        for (int i = 1; i < viewName.Length; i++)
        {
            if (char.IsUpper(viewName[i]))
            {
                viewName = viewName.Insert(i, " ");
                i++;
            }
        }

        return viewName;
    }
}