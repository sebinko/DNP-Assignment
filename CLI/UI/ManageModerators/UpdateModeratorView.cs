using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI.ManageModerators;

public class UpdateModeratorView(
    IModeratorRepository moderatorRepository,
    IUserRepository userRepository,
    ISubforumRepository subforumRepository) : IView
{
    public async Task Run()
    {
        try
        {
            Console.Clear();
            PrettyConsole.WriteInfo("Enter user id:");
            var userId = Console.ReadLine();

            if (!int.TryParse(userId, out var moderatorUserId)) throw new Exception("Invalid user id");

            await userRepository.GetByIdAsync(moderatorUserId);

            PrettyConsole.WriteInfo("Enter subforum id:");
            var subforumId = Console.ReadLine();

            if (!int.TryParse(subforumId, out var moderatorSubforumId)) throw new Exception("Invalid subforum id");

            await subforumRepository.GetByIdAsync(moderatorSubforumId);

            var moderator = await moderatorRepository.GetByIdAsync(moderatorUserId, moderatorSubforumId);

            PrettyConsole.WriteInfo("Enter new user id:");
            var newUserId = Console.ReadLine();

            if (!int.TryParse(newUserId, out var newModeratorUserId)) throw new Exception("Invalid user id");

            await userRepository.GetByIdAsync(newModeratorUserId);

            PrettyConsole.WriteInfo("Enter new subforum id:");
            var newSubforumId = Console.ReadLine();

            if (!int.TryParse(newSubforumId, out var newModeratorSubforumId))
                throw new Exception("Invalid subforum id");

            await subforumRepository.GetByIdAsync(newModeratorSubforumId);

            moderator.UserId = newModeratorUserId;
            moderator.SubforumId = newModeratorSubforumId;

            await moderatorRepository.UpdateAsync(moderator);

            PrettyConsole.WriteSuccess("Moderator updated successfully");
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError(e.Message);
        }
    }
}