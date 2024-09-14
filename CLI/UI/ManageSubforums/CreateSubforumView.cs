using CLI.UI.Utilities;
using Domain;
using RepositoryContracts.Interfaces;

namespace CLI.UI.ManageSubforums;

public class CreateSubforumView(ISubforumRepository subforumRepository, IUserRepository userRepository) : IView
{
    public async Task Run()
    {
        try
        {
            Console.Clear();
            PrettyConsole.WriteInfo("Create Subforum");
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

            var subforum = new Subforum
            {
                Title = title,
                Description = description,
                OwnerUserId = ownerUserId
            };

            await subforumRepository.AddAsync(subforum);

            // TODO add this user as a moderator

            PrettyConsole.WriteSuccess("Subforum created successfully");
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError(e.Message);
        }
    }
}