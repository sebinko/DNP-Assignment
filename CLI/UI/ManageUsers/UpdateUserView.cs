using RepositoryContracts.Interfaces;

namespace CLI.UI.ManageUsers;

public class UpdateUserView(IUserRepository userRepository) : IView
{
    public async Task Run()
    {
        Console.Clear();

        try
        {
            Console.WriteLine("Enter user id:");
            var id = int.Parse(Console.ReadLine() ?? throw new Exception("Id cannot be null"));
            
            var user = await userRepository.GetByIdAsync(id);

            Console.WriteLine("Enter new username:");
            var userName = Console.ReadLine();

            Console.WriteLine("Enter new password:");
            var password = Console.ReadLine();
            
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Username and password cannot be null or empty");
            }

            user.UserName = userName;
            user.Password = password;

            await userRepository.UpdateAsync(user);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error:{e.Message}");
        }
    }
}