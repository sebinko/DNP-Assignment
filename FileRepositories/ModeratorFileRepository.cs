using System.Text.Json;
using Domain;
using RepositoryContracts.Interfaces;

namespace FileRepositories;

public class ModeratorFileRepository : IModeratorRepository
{
    private HashSet<Moderator> _moderators = new();

    public ModeratorFileRepository()
    {
        ReadFileAsync().GetAwaiter().GetResult();
    }

    public async Task<Moderator> AddAsync(Moderator moderator)
    {
        await ReadFileAsync();

        if (_moderators.FirstOrDefault(m => m.UserId == moderator.UserId && m.SubforumId == moderator.SubforumId) !=
            null)
            throw new Exception("This user is already a moderator of this subforum");

        _moderators.Add(moderator);

        await WriteFileAsync();

        return moderator;
    }


    public async Task<Moderator> UpdateAsync(Moderator moderator)
    {
        await ReadFileAsync();

        var existingModerator =
            _moderators.FirstOrDefault(m => m.UserId == moderator.UserId && m.SubforumId == moderator.SubforumId);

        if (existingModerator != null && (existingModerator.UserId != moderator.UserId ||
                                          existingModerator.SubforumId != moderator.SubforumId))
            throw new Exception("This user is already a moderator of this subforum");

        _moderators.Remove(existingModerator);
        _moderators.Add(moderator);

        await WriteFileAsync();

        return moderator;
    }

    public async Task<Moderator> DeleteAsync(Moderator moderator)
    {
        await ReadFileAsync();

        var existingModerator =
            _moderators.FirstOrDefault(m => m.UserId == moderator.UserId && m.SubforumId == moderator.SubforumId);
        if (existingModerator == null) throw new Exception("Moderator not found");

        _moderators.Remove(existingModerator);

        await WriteFileAsync();

        return moderator;
    }

    public async Task<Moderator> GetByIdAsync(int userId, int subforumId)
    {
        await ReadFileAsync();

        var moderator = _moderators.FirstOrDefault(m => m.UserId == userId && m.SubforumId == subforumId);

        if (moderator == null) throw new Exception("Moderator not found");

        return moderator;
    }

    public IQueryable<Moderator> GetAll()
    {
        ReadFileAsync().GetAwaiter().GetResult();

        return _moderators.AsQueryable();
    }


    private async Task WriteFileAsync()
    {
        await File.WriteAllTextAsync("moderators.json", JsonSerializer.Serialize(_moderators));
    }

    private async Task ReadFileAsync()
    {
        if (!File.Exists("moderators.json")) return;

        _moderators = JsonSerializer.Deserialize<HashSet<Moderator>>(await File.ReadAllTextAsync("posts.json"));
    }
}