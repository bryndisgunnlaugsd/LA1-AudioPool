namespace AudioPool.Models.DTOs;

public class AlbumDetailsDto : HyperMediaModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime ReleaseDate { get; set; }
    public string? CoverImageUrl { get; set; }
    public string? Description { get; set; }
    public IEnumerable<ArtistDto> Artists { get; set; } = new List<ArtistDto>();
    public IEnumerable<SongDto> Songs { get; set; } = new List<SongDto>();
}