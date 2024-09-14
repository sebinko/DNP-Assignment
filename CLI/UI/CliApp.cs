using CLI.UI.Menu;
using CLI.UI.Utilities;
using ConsoleTables;

namespace CLI.UI;

public class CliApp(Dictionary<string, IView> views)
{
    public async Task StartAsync()
    {
        var exit = false;

        while (!exit)
        {
            PrintMenu();

            PrettyConsole.WriteInfo("Enter your choice:");

            var choice = Console.ReadLine();

            if (!string.IsNullOrEmpty(choice) && views.TryGetValue(choice, out var view))
            {
                await view.Run();

                if (view is ExitView) exit = true;
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
        var table = new ConsoleTable("M", "E", "N", "U");
        table.Options.EnableCount = false;
        var views1 = views.ToList();

        for (var i = 0; i < views1.Count; i += 4)
        {
            var row = new List<string>();
            for (var j = 0; j < 4; j++)
                if (i + j < views1.Count)
                {
                    var view = views1[i + j];
                    var viewName = ParseViewName(view.Value.GetType().Name);
                    row.Add($"{view.Key} - {viewName}");
                }
                else
                {
                    row.Add("");
                }

            table.AddRow(row.ToArray());
        }

        table.Write(Format.Minimal);
    }

    private static string ParseViewName(string viewName)
    {
        if (viewName.EndsWith("View")) viewName = viewName.Substring(0, viewName.Length - 4);

        for (var i = 1; i < viewName.Length; i++)
            if (char.IsUpper(viewName[i]))
            {
                viewName = viewName.Insert(i, " ");
                i++;
            }

        return viewName;
    }
}