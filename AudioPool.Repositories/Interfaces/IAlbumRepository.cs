using AudioPool.Models.DTOs;

namespace AudioPool.Repositories.Interfaces;

public interface IAlbumRepository
{
    IEnumerable<AlbumDto>? GetAlbumsByArtistId(int artistId);
    IEnumerable<int> GetArtistIdsByAlbumId(int albumId);
}