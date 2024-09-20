namespace Domain;

public class Like
{
    public static List<string> LikeableTypes = [typeof(Post).ToString(), typeof(Comment).ToString()];

    private string _likeableType;

    private int _value;
    public int Id { get; set; }

    public required int Value
    {
        get => _value;
        set => _value = value == 1 || value == -1 ? value : throw new ArgumentException("Value must be 1 or -1");
    }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public required int UserId { get; set; }
    public required int LikeableId { get; set; }

    public required string LikeableType
    {
        get => _likeableType;
        set => _likeableType = LikeableTypes.Contains(value)
            ? value
            : throw new ArgumentException($"LikeableType must be either {string.Join(", ", LikeableTypes)}");
    }
}