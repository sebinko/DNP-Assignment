using CLI.UI.Utilities;
using Domain;
using RepositoryContracts.Interfaces;

namespace CLI.UI.Views.SingleEntities;

public class SinglePostView(
    IPostRepository postRepository,
    IUserRepository userRepository,
    ISubforumRepository subforumRepository,
    ICommentRepository commentRepository,
    ILikeRepository likeRepository,
    IModeratorRepository moderatorRepository) : IView
{
    private Post _post;

    public async Task Run()
    {
        try
        {
            Console.WriteLine("Enter the ID of the post you want to view");
            var postId = int.Parse(Console.ReadLine() ?? throw new Exception("Id cannot be null"));

            _post = await postRepository.GetByIdAsync(postId);

            Console.Clear();
            DisplayPostData();
            await DisplayLikes();
            await DisplayOwner();
            await DisplayModerators();
            await DisplayComments();
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError(e.Message);
        }
    }

    private void DisplayPostData()
    {
        PrettyConsole.WriteSuccess($"ID: {_post.Id}");
        PrettyConsole.WriteSuccess($"Title: {_post.Title}");
        PrettyConsole.WriteSuccess($"Body: {_post.Body}");
    }

    private async Task DisplayModerators()
    {
        var moderators = moderatorRepository.GetAll().Where(m => m.SubforumId == _post.SubforumId).ToList();

        if (moderators.Any())
        {
            PrettyConsole.WriteSuccess($"Moderators ({moderators.Count}):");
            foreach (var moderator in moderators)
            {
                var user = await userRepository.GetByIdAsync(moderator.UserId);
                PrettyConsole.WriteInfo($"\tID: {user.Id}");
                PrettyConsole.WriteInfo($"\tUsername: {user.UserName}");
            }
        }
    }

    private async Task DisplayOwner()
    {
        var user = await userRepository.GetByIdAsync(_post.UserId);
        PrettyConsole.WriteSuccess($"Owner:");
        PrettyConsole.WriteInfo($"\tID: {user.Id}");
        PrettyConsole.WriteInfo($"\tUsername: {user.UserName}");
    }

    private async Task DisplayLikes()
    {
        var likes = likeRepository.GetAll().Count(l => l.LikeableId == _post.Id && l.LikeableType == typeof(Post).ToString());
        PrettyConsole.WriteSuccess($"Likes: {likes}");
    }

    private async Task DisplayComments()
    {
        var comments = commentRepository.GetAll()
            .Where(c => c.CommentableId == _post.Id && c.CommentableType == typeof(Post).ToString()).ToList();

        if (comments.Any())
        {
            PrettyConsole.WriteSuccess($"Comments ({comments.Count}):");
            foreach (var comment in comments)
            {
                await DisplayComment(comment, 0);
            }
        }
    }

    private async Task DisplayComment(Comment comment, int level)
    {
        var user = await userRepository.GetByIdAsync(comment.UserId);
        var indent = new string('\t', level);
        var likes = likeRepository.GetAll().Count(l => l.LikeableId == comment.Id && l.LikeableType == typeof(Comment).ToString());
        PrettyConsole.WriteInfo($"{indent}ID: {comment.Id}");
        PrettyConsole.WriteInfo($"{indent}Body: {comment.Body}");
        PrettyConsole.WriteInfo($"{indent}User: {user.UserName}");
        PrettyConsole.WriteInfo($"{indent}Likes: {likes}");

        var nestedComments = commentRepository.GetAll()
            .Where(c => c.CommentableId == comment.Id && c.CommentableType == typeof(Comment).ToString()).ToList();

        foreach (var nestedComment in nestedComments)
        {
            await DisplayComment(nestedComment, level + 1);
        }
    }
}