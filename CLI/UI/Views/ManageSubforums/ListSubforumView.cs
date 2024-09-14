using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI.Views.ManageSubforums;

public class ListSubforumView(ISubforumRepository subforumRepository) : IView
{
    public Task Run()
    {
        try
        {
            Console.Clear();

            var subforums = subforumRepository.GetAll();

            PrettyConsole.PrintTable(subforums.ToList(), Level.Success);
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError(e.Message);
        }

        return Task.CompletedTask;
    }
}