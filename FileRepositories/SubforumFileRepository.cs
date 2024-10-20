using System.Text.Json;
using Domain;
using RepositoryContracts.Interfaces;

namespace InMemoryRepositories;

public class SubforumFileRepository : ISubforumRepository
{
    private HashSet<Subforum> _subforums = new();

    public SubforumFileRepository()
    {
        ReadFileAsync().GetAwaiter().GetResult();
    }

    public async Task<Subforum> AddAsync(Subforum subforum)
    {
        await ReadFileAsync();

        subforum.Id = _subforums.Any() ? _subforums.Max(s => s.Id) + 1 : 1;

        subforum.CreatedAt = DateTime.Now;
        subforum.UpdatedAt = DateTime.Now;

        _subforums.Add(subforum);

        await WriteFileAsync();

        return subforum;
    }

    public async Task<Subforum> UpdateAsync(Subforum subforum)
    {
        await ReadFileAsync();

        var existingSubforum = _subforums.FirstOrDefault(s => s.Id == subforum.Id);
        if (existingSubforum == null) throw new Exception("Subforum not found");

        subforum.UpdatedAt = DateTime.Now;

        _subforums.Remove(existingSubforum);
        _subforums.Add(subforum);

        await WriteFileAsync();

        return subforum;
    }

    public async Task<Subforum> DeleteAsync(Subforum subforum)
    {
        await ReadFileAsync();

        var existingSubforum = _subforums.FirstOrDefault(s => s.Id == subforum.Id);
        if (existingSubforum == null) throw new Exception("Subforum not found");

        _subforums.Remove(existingSubforum);

        await WriteFileAsync();

        return subforum;
    }

    public async Task<Subforum> GetByIdAsync(int id)
    {
        await ReadFileAsync();

        var subforum = _subforums.FirstOrDefault(s => s.Id == id);

        if (subforum == null) throw new Exception("Subforum not found");

        return subforum;
    }

    public IQueryable<Subforum> GetAll()
    {
        return _subforums.AsQueryable();
    }


    private async Task WriteFileAsync()
    {
        await File.WriteAllTextAsync("subforums.json", JsonSerializer.Serialize(_subforums));
    }

    private async Task ReadFileAsync()
    {
        if (!File.Exists("subforums.json")) return;

        _subforums = JsonSerializer.Deserialize<HashSet<Subforum>>(await File.ReadAllTextAsync("moderators.json"));
    }
}