using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI.ManageModerators;

public class DeleteModeratorView(
    IModeratorRepository moderatorRepository,
    ISubforumRepository subforumRepository,
    IUserRepository userRepository) : IView
{
    public async Task Run()
    {
        try
        {
            Console.Clear();

            PrettyConsole.WriteQuestion("Enter user id:");
            var userId = int.Parse(Console.ReadLine() ?? throw new Exception("Id cannot be null"));
            await userRepository.GetByIdAsync(userId);

            PrettyConsole.WriteQuestion("Enter subforum id:");
            var subforumId = int.Parse(Console.ReadLine() ?? throw new Exception("Id cannot be null"));
            await subforumRepository.GetByIdAsync(subforumId);

            var moderator = await moderatorRepository.GetByIdAsync(userId, subforumId);

            await moderatorRepository.DeleteAsync(moderator);
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError(e.Message);
        }
    }
}