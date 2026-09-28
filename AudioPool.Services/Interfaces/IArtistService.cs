using AudioPool.Models;
using AudioPool.Models.DTOs;
using AudioPool.Models.InputModels;

namespace AudioPool.Services.Interfaces;

public interface IArtistService
{
    Envelope<ArtistDto> GetAllArtists(int pageNumber, int pageSize);
    ArtistDetailsDto? GetArtistById(int id);

    ArtistDto CreateArtist(ArtistInputModel model);
    ArtistDto? UpdateArtist(int id, ArtistInputModel model);
    bool LinkArtistToGenre(int artistId, int genreId);
    
}