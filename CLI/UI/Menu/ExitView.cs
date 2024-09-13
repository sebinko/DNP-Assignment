namespace CLI.UI.Menu;

public class ExitView : IView
{
    public Task Run()
    {
        return Task.CompletedTask;
    }
}