namespace AudioPool.Models.Entities;

public class Song
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public TimeSpan Duration { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
    public string? ModifiedBy { get; set; }

    // One-to-many: each Song belongs to one Album
    public int AlbumId { get; set; }
    public Album Album { get; set; } = null!;
}