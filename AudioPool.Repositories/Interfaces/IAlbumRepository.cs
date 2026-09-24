using AudioPool.Models.DTOs;

namespace AudioPool.Repositories.Interfaces;

public interface IAlbumRepository
{
    AlbumDetailsDto? GetAlbumById(int id);
    IEnumerable<AlbumDto>? GetAlbumsByArtistId(int artistId);
    IEnumerable<int> GetArtistIdsByAlbumId(int albumId);
}