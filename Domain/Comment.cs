namespace Domain;

public class Comment
{
    public int Id { get; set; }
    public required string Body { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public required int UserId { get; set; }
    public required int CommentableId { get; set; }

    private string _commentableType;

    public string CommentableType
    {
        get => _commentableType;
        set => _commentableType = CommentableTypes.Contains(value)
            ? value
            : throw new ArgumentException($"CommentableType must be either {string.Join(", ", CommentableTypes)}");
    }


    public static List<string> CommentableTypes = [typeof(Post).ToString(), typeof(Comment).ToString()];
}