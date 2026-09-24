namespace AudioPool.Models.DTOs;

public class GenreDetailsDto : HyperMediaModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int NumberOfArtists { get; set; }
}