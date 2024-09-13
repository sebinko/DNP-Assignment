using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI.ManageSubforums;

public class ListSubforumView(ISubforumRepository subforumRepository) : IView
{
    public async Task Run()
    {
        try
        {
            Console.Clear();

            var subforums = subforumRepository.GetAll();

            foreach (var subforum in subforums)
            {
                PrettyConsole.WriteSuccess(
                    $"Id: {subforum.Id}, Title: {subforum.Title}, Description: {subforum.Description}, OwnerUserId: {subforum.OwnerUserId}, CreatedAt: {subforum.CreatedAt}, UpdatedAt: {subforum.UpdatedAt}");
            }
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError(e.Message);
        }
    }
}