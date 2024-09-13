using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI.ManagePosts;

public class ListPostView(IPostRepository postRepository) : IView
{
    public async Task Run()
    {
        Console.Clear();
        
        var posts = postRepository.GetAll();

        foreach (var post in posts)
        {
            PrettyConsole.WriteSuccess(
                $"Id: {post.Id}, Title: {post.Title}, Body: {post.Body}, UserId: {post.UserId}, SubforumId: {post.SubforumId}");
        }
    }
}