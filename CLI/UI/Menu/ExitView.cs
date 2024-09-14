using CLI.UI.Views;

namespace CLI.UI.Menu;

public class ExitView : IView
{
    public Task Run()
    {
        return Task.CompletedTask;
    }
}