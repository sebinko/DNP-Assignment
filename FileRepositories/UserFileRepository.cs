using System.Text.Json;
using Domain;
using RepositoryContracts.Interfaces;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private HashSet<User> _users = new();

    public UserFileRepository()
    {
        _ = ReadFileAsync();
    }

    public async Task<User> AddAsync(User user)
    {
        await ReadFileAsync();

        user.Id = _users.Any() ? _users.Max(c => c.Id) + 1 : 1;

        user.CreatedAt = DateTime.Now;
        user.UpdatedAt = DateTime.Now;

        _users.Add(user);
        await WriteFileAsync();

        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        await ReadFileAsync();

        var existingUser = _users.FirstOrDefault(c => c.Id == user.Id);
        if (existingUser == null) throw new Exception("User not found");

        user.UpdatedAt = DateTime.Now;

        _users.Remove(existingUser);
        _users.Add(user);

        await WriteFileAsync();

        return user;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        await ReadFileAsync();

        var user = _users.FirstOrDefault(c => c.Id == id);

        if (user == null) throw new Exception("User not found");

        return user;
    }

    public async Task<User> DeleteAsync(User user)
    {
        await ReadFileAsync();

        var existingUser = _users.FirstOrDefault(c => c.Id == user.Id);
        if (existingUser == null) throw new Exception("User not found");

        _users.Remove(existingUser);

        await WriteFileAsync();

        return user;
    }

    public IQueryable<User> GetAll()
    {
        ReadFileAsync().GetAwaiter().GetResult();

        return _users.AsQueryable();
    }

    private async Task WriteFileAsync()
    {
        await File.WriteAllTextAsync("users.json", JsonSerializer.Serialize(_users));
    }

    private async Task ReadFileAsync()
    {
        if (!File.Exists("users.json")) return;

        _users = JsonSerializer.Deserialize<HashSet<User>>(await File.ReadAllTextAsync("users.json"));
    }
}