using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI.ManageSubforums;

public class DeleteSubforumView (ISubforumRepository subforumRepository): IView
{
    public async Task Run()
    {
        try
        {
            Console.Clear();
            PrettyConsole.WriteInfo("Delete Subforum");
            PrettyConsole.WriteInfo("Enter subforum id:");
            var subforumId = Console.ReadLine();

            if (!int.TryParse(subforumId, out var id))
            {
                PrettyConsole.WriteError("Invalid subforum id");
                return;
            }

            var post = await subforumRepository.GetByIdAsync(id);
            
            await subforumRepository.DeleteAsync(post);

            PrettyConsole.WriteSuccess("Subforum deleted successfully");
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError(e.Message);
        }
    }
    
    
}