using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI.Views.ManageComments;

public class ListCommentView(ICommentRepository commentRepository) : IView
{
    public async Task Run()
    {
        try
        {
            Console.Clear();

            var comments = commentRepository.GetAll();

            PrettyConsole.PrintTable(comments.ToList(), Level.Success);
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError(e.Message);
        }
    }
}