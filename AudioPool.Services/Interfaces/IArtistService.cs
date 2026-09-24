using AudioPool.Models;
using AudioPool.Models.DTOs;

namespace AudioPool.Services.Interfaces;

public interface IArtistService
{
    Envelope<ArtistDto> GetAllArtists(int pageNumber, int pageSize);
    ArtistDetailsDto? GetArtistById(int id);
}