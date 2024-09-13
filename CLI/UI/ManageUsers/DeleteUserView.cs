using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI.ManageUsers;

public class DeleteUserView(IUserRepository userRepository) : IView
{
    public async Task Run()
    {
        Console.Clear();

        try
        {
            PrettyConsole.WriteQuestion("Enter user id:");
            
            var id = int.Parse(Console.ReadLine() ?? throw new Exception("Id cannot be null"));
            var user = await userRepository.GetByIdAsync(id);

            await userRepository.DeleteAsync(user);
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError($"Error:{e.Message}");
        }
    }
}