using CLI.UI.Utilities;
using Domain;
using RepositoryContracts.Interfaces;

namespace CLI.UI.ManageLikes;

public class CreateLikeView(
    ILikeRepository likeRepository,
    IUserRepository userRepository,
    IPostRepository postRepository,
    ICommentRepository commentRepository) : IView
{
    public async Task Run()
    {
        try
        {
            Console.Clear();

            Console.WriteLine("Enter user id:");
            var userId = int.Parse(Console.ReadLine());
            await userRepository.GetByIdAsync(userId);

            Console.WriteLine($"Enter LikeableType ({string.Join(", ", Like.LikeableTypes)}):");
            var likeableTypeStr = Console.ReadLine();


            Console.WriteLine("Enter LikeableId:");
            var likeableId = int.Parse(Console.ReadLine());

            if (likeableTypeStr == typeof(Post).ToString())
                await postRepository.GetByIdAsync(likeableId);
            else if (likeableTypeStr == typeof(Comment).ToString()) await commentRepository.GetByIdAsync(likeableId);
            // if it is invalid LikeableType, it will throw an exception from the Like Class

            Console.WriteLine("Enter value (1 or -1):");
            var value = int.Parse(Console.ReadLine());

            var like = new Like
            {
                UserId = userId,
                LikeableType = likeableTypeStr,
                LikeableId = likeableId,
                Value = value
            };

            await likeRepository.AddAsync(like);
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError($"Error:{e.Message}");
        }
    }
}