namespace Domain;

public class Like
{
    public int Id { get; set; }

    private int _value;

    public required int Value
    {
        get => _value;
        set => _value = value == 1 || value == -1 ? value : throw new ArgumentException("Value must be 1 or -1");
    }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public required int UserId { get; set; }
    public required int LikeableId { get; set; }

    private string _likeableType;

    public required string LikeableType
    {
        get => _likeableType;
        set => _likeableType = LikeableTypes.Contains(value)
            ? value
            : throw new ArgumentException($"LikeableType must be either {string.Join(", ", LikeableTypes)}");
    }

    public static List<String> LikeableTypes = new List<string> { typeof(Post).ToString(), typeof(Comment).ToString() };
}