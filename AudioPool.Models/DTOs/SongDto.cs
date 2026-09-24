namespace AudioPool.Models.DTOs;

public class SongDto : HyperMediaModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public TimeSpan Duration { get; set; }
}