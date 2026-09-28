using AudioPool.Models.DTOs;
using AudioPool.Models.InputModels;

namespace AudioPool.Repositories.Interfaces;

public interface IArtistRepository
{
    IEnumerable<ArtistDto> GetAllArtists();
    ArtistDetailsDto? GetArtistById(int id);
    IEnumerable<int> GetGenreIdsByArtistId(int artistId);

    ArtistDto CreateArtist(ArtistInputModel model);
    ArtistDto? UpdateArtist(int id, ArtistInputModel model);
    bool LinkArtistToGenre(int artistId, int genreId);


}