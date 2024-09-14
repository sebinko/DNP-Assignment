using CLI.UI.Utilities;
using Domain;
using RepositoryContracts.Interfaces;

namespace CLI.UI.ManageModerators;

public class CreateModeratorView(
    IModeratorRepository moderatorRepository,
    IUserRepository userRepository,
    ISubforumRepository subforumRepository) : IView
{
    public async Task Run()
    {
        try
        {
            Console.Clear();
            PrettyConsole.WriteInfo("Create Moderator");
            PrettyConsole.WriteInfo("Enter user id:");
            var userId = Console.ReadLine();

            if (!int.TryParse(userId, out var moderatorUserId))
            {
                PrettyConsole.WriteError("Invalid user id");
                return;
            }

            await userRepository.GetByIdAsync(moderatorUserId);

            PrettyConsole.WriteInfo("Enter subforum id:");
            var subforumId = Console.ReadLine();

            if (!int.TryParse(subforumId, out var moderatorSubforumId))
            {
                PrettyConsole.WriteError("Invalid subforum id");
                return;
            }

            await subforumRepository.GetByIdAsync(moderatorSubforumId);

            var moderator = new Moderator
            {
                UserId = moderatorUserId,
                SubforumId = moderatorSubforumId
            };

            await moderatorRepository.AddAsync(moderator);

            PrettyConsole.WriteSuccess("Moderator created successfully");
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError(e.Message);
        }
    }
}