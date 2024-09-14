using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI.Views.ManageLikes;

public class UpdateLikeView(ILikeRepository likeRepository) : IView
{
    public async Task Run()
    {
        try
        {
            Console.Clear();

            Console.WriteLine("Enter like id:");
            var likeId = int.Parse(Console.ReadLine());
            var like = await likeRepository.GetByIdAsync(likeId);

            Console.WriteLine("Enter value (1 or -1):");
            var value = int.Parse(Console.ReadLine());

            like.Value = value;

            await likeRepository.UpdateAsync(like);
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError($"Error:{e.Message}");
        }
    }
}