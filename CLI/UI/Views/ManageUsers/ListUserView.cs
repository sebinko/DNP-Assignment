using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI.Views.ManageUsers;

public class ListUserView(IUserRepository userRepository) : IView
{
    public Task Run()
    {
        try
        {
            Console.Clear();

            var users = userRepository.GetAll();

            PrettyConsole.PrintTable(users.ToList(), Level.Success);

            return Task.CompletedTask;
        }
        catch (Exception exception)
        {
            PrettyConsole.WriteError(exception.Message);
        }

        return Task.CompletedTask;
    }
}