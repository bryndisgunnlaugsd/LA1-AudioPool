using AudioPool.Models.DTOs;

namespace AudioPool.Repositories.Interfaces;

public interface IArtistRepository
{
    IEnumerable<ArtistDto> GetAllArtists();
    ArtistDetailsDto? GetArtistById(int id);
    IEnumerable<int> GetGenreIdsByArtistId(int artistId);
}