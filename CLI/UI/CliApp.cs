using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using CLI.UI.Menu;
using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI;

public class CliApp
{
    private readonly Dictionary<string, IView> _views;

    public CliApp(Dictionary<string, IView> views)
    {
        _views = views;
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