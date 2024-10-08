using Domain;
using RepositoryContracts.Interfaces;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private readonly List<User> users = new();

    public UserInMemoryRepository()
    {
        AddAsync(new User
        {
            UserName = "user1",
            Password = "password1"
        });
    }

    public Task<User> AddAsync(User user)
    {
        user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;

        user.CreatedAt = DateTime.Now;
        user.UpdatedAt = DateTime.Now;

        users.Add(user);
        return Task.FromResult(user);
    }

    public Task<User> UpdateAsync(User user)
    {
        var existingUser = users.FirstOrDefault(u => u.UserName == user.UserName);
        if (existingUser == null) throw new Exception("User not found");

        user.UpdatedAt = DateTime.Now;

        users[users.FindIndex(u => u.UserName == user.UserName)] = user;

        return Task.FromResult(user);
    }

    public Task<User> DeleteAsync(User user)
    {
        var existingUser = users.FirstOrDefault(u => u.UserName == user.UserName);
        if (existingUser == null) throw new Exception("User not found");

        users.Remove(existingUser);

        return Task.FromResult(user);
    }

    public Task<User> GetByIdAsync(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);

        if (user == null) throw new Exception("User not found");

        return Task.FromResult(user);
    }

    public IQueryable<User> GetAll()
    {
        return users.AsQueryable();
    }
}