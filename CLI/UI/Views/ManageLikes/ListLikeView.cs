using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI.Views.ManageLikes;

public class ListLikeView(ILikeRepository likeRepository) : IView
{
    public async Task Run()
    {
        try
        {
            Console.Clear();

            var likes = likeRepository.GetAll();

            PrettyConsole.PrintTable(likes.ToList(), Level.Success);
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError(e.Message);
        }
    }
}