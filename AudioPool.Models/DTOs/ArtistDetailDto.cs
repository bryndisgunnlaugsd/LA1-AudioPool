namespace AudioPool.Models.DTOs;

public class ArtistDetailsDto : HyperMediaModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Bio { get; set; }
    public string? CoverImageUrl { get; set; }
    public DateTime DateOfStart { get; set; }
    public IEnumerable<AlbumDto> Albums { get; set; } = new List<AlbumDto>();
    public IEnumerable<GenreDto> Genres { get; set; } = new List<GenreDto>();
}