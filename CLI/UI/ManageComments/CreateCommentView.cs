using CLI.UI.Utilities;
using Domain;
using RepositoryContracts.Interfaces;

namespace CLI.UI.ManageComments;

public class CreateCommentView(
    ICommentRepository commentRepository,
    IUserRepository userRepository,
    IPostRepository postRepository) : IView
{
    public async Task Run()
    {
        try
        {
            Console.Clear();

            Console.WriteLine("Enter user id:");
            var userId = int.Parse(Console.ReadLine());
            await userRepository.GetByIdAsync(userId);

            Console.WriteLine($"Enter CommentableType ({string.Join(", ", Comment.CommentableTypes)}):");
            var commentableTypeStr = Console.ReadLine();

            Console.WriteLine("Enter CommentableId:");
            var commentableId = int.Parse(Console.ReadLine());

            if (commentableTypeStr == typeof(Post).ToString())
                await postRepository.GetByIdAsync(commentableId);
            else if (commentableTypeStr == typeof(Comment).ToString())
                await commentRepository.GetByIdAsync(commentableId);

            Console.WriteLine("Enter body:");
            var body = Console.ReadLine();

            var comment = new Comment
            {
                UserId = userId,
                CommentableType = commentableTypeStr,
                CommentableId = commentableId,
                Body = body
            };

            await commentRepository.AddAsync(comment);
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError($"Error:{e.Message}");
        }
    }
}