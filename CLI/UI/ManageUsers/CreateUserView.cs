using CLI.UI.Utilities;
using Domain;
using RepositoryContracts.Interfaces;

namespace CLI.UI.ManageUsers;

public class CreateUserView(IUserRepository userRepository) : IView
{
    public async Task Run()
    {
        Console.Clear();

        PrettyConsole.WriteQuestion("Enter username:");
        var userName = Console.ReadLine();

        PrettyConsole.WriteQuestion("Enter password:");
        var password = Console.ReadLine();

        try
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                throw new Exception("Username and password cannot be null or empty");

            await userRepository.AddAsync(new User
            {
                UserName = userName,
                Password = password
            });
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError($"Error:{e.Message}");
        }
    }
}