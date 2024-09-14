using CLI.UI.Utilities;
using RepositoryContracts.Interfaces;

namespace CLI.UI.Views.ManageComments;

public class UpdateCommentView(ICommentRepository commentRepository) : IView
{
    public async Task Run()
    {
        try
        {
            Console.Clear();

            Console.WriteLine("Enter comment id:");
            var commentId = int.Parse(Console.ReadLine());
            var comment = await commentRepository.GetByIdAsync(commentId);

            Console.WriteLine("Enter body:");
            var body = Console.ReadLine();

            comment.Body = body;

            await commentRepository.UpdateAsync(comment);
        }
        catch (Exception e)
        {
            PrettyConsole.WriteError($"Error:{e.Message}");
        }
    }
}