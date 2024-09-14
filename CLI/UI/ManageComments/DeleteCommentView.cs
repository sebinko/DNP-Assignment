using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI.ManageComments;

public class DeleteCommentView(ICommentRepository commentRepository) : IView
{
    public async Task Run()
    {
        try
        {
            Console.Clear();

            Console.WriteLine("Enter comment id:");
            var commentId = int.Parse(Console.ReadLine());
            var comment = await commentRepository.GetByIdAsync(commentId);

            await commentRepository.DeleteAsync(comment);
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError($"Error:{e.Message}");
        }
    }
}