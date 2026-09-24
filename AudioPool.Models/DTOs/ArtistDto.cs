namespace AudioPool.Models.DTOs;

public class ArtistDto : HyperMediaModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Bio { get; set; }
    public string? CoverImageUrl { get; set; }
    public DateTime DateOfStart { get; set; }
}