using AudioPool.Models.DTOs;
using AudioPool.Models.InputModels;

namespace AudioPool.Repositories.Interfaces;

public interface IAlbumRepository
{
    AlbumDetailsDto? GetAlbumById(int id);
    IEnumerable<AlbumDto>? GetAlbumsByArtistId(int artistId);
    IEnumerable<int> GetArtistIdsByAlbumId(int albumId);

    AlbumDto? CreateAlbum(AlbumInputModel model);
    bool DeleteAlbum(int id);
}