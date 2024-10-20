using Domain;
using RepositoryContracts.Interfaces;
using SharedLib.Exceptions;

namespace InMemoryRepositories;

public class SubforumInMemoryRepository : ISubforumRepository
{
    private readonly List<Subforum> subforums = new();

    public SubforumInMemoryRepository()
    {
        AddAsync(new Subforum
        {
            Title = "Subforum 1",
            Description = "Description 1",
            OwnerUserId = 1
        });
    }

    public Task<Subforum> AddAsync(Subforum subforum)
    {
        subforum.Id = subforums.Any() ? subforums.Max(s => s.Id) + 1 : 1;

        subforum.CreatedAt = DateTime.Now;
        subforum.UpdatedAt = DateTime.Now;

        subforums.Add(subforum);
        return Task.FromResult(subforum);
    }

    public Task<Subforum> UpdateAsync(Subforum subforum)
    {
        var existingSubforum = subforums.FirstOrDefault(s => s.Id == subforum.Id);
        if (existingSubforum == null) throw new Exception("Subforum not found");

        subforum.UpdatedAt = DateTime.Now;

        subforums[subforums.FindIndex(s => s.Id == subforum.Id)] = subforum;
        
        Console.WriteLine(subforum.Title);

        return Task.FromResult(subforum);
    }

    public Task<Subforum> DeleteAsync(Subforum subforum)
    {
        var existingSubforum = subforums.FirstOrDefault(s => s.Id == subforum.Id);
        if (existingSubforum == null) throw new Exception("Subforum not found");

        subforums.Remove(existingSubforum);

        return Task.FromResult(subforum);
    }

    public Task<Subforum> GetByIdAsync(int id)
    {
        var subforum = subforums.FirstOrDefault(s => s.Id == id);

        if (subforum == null) throw new NotFoundException(typeof(Subforum));


        return Task.FromResult(subforum);
    }

    public IQueryable<Subforum> GetAll()
    {
        return subforums.AsQueryable();
    }
}