using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI.Views.ManageModerators;

public class ListModeratorView(IModeratorRepository moderatorRepository) : IView
{
    public Task Run()
    {
        try
        {
            Console.Clear();

            var moderators = moderatorRepository.GetAll();

            PrettyConsole.PrintTable(moderators.ToList(), Level.Success);
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError(e.Message);
        }

        return Task.CompletedTask;
    }
}