using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI.ManageUsers;

public class ListUserView(IUserRepository userRepository) : IView
{
    public Task Run()
    {
        Console.Clear();
        
        var users = userRepository.GetAll();

        foreach (var user in users)
        {
            PrettyConsole.WriteSuccess(
                $"Id: {user.Id}, Username: {user.UserName}, Password: {user.Password}, CreatedAt: {user.CreatedAt}, UpdatedAt: {user.UpdatedAt}");
        }

        return Task.CompletedTask;
    }
}