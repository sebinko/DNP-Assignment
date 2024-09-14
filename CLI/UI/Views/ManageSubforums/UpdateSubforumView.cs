using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI.Views.ManageSubforums;

public class UpdateSubforumView(ISubforumRepository subforumRepository, IUserRepository userRepository) : IView
{
    public async Task Run()
    {
        try
        {
            Console.Clear();
            PrettyConsole.WriteInfo("Update Subforum");
            PrettyConsole.WriteInfo("Enter subforum id:");
            var subforumId = Console.ReadLine();

            if (!int.TryParse(subforumId, out var id))
            {
                PrettyConsole.WriteError("Invalid subforum id");
                return;
            }

            var subforum = await subforumRepository.GetByIdAsync(id);

            PrettyConsole.WriteInfo("Enter title:");
            var title = Console.ReadLine();

            PrettyConsole.WriteInfo("Enter description:");
            var description = Console.ReadLine();

            PrettyConsole.WriteInfo("Enter owner user id:");
            var ownerId = Console.ReadLine();

            if (!int.TryParse(ownerId, out var ownerUserId))
            {
                PrettyConsole.WriteError("Invalid owner user id");
                return;
            }

            await userRepository.GetByIdAsync(ownerUserId);

            subforum.Title = title;
            subforum.Description = description;
            subforum.OwnerUserId = ownerUserId;

            await subforumRepository.UpdateAsync(subforum);

            PrettyConsole.WriteSuccess("Subforum updated successfully");
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError(e.Message);
        }
    }
}