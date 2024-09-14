using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI.Views.ManageLikes;

public class DeleteLikeView(ILikeRepository likeRepository) : IView
{
    public async Task Run()
    {
        try
        {
            Console.Clear();

            Console.WriteLine("Enter like id:");
            var likeId = int.Parse(Console.ReadLine());
            var like = await likeRepository.GetByIdAsync(likeId);

            await likeRepository.DeleteAsync(like);
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError($"Error:{e.Message}");
        }
    }
}