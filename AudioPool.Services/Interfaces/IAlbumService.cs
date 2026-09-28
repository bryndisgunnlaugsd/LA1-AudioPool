using AudioPool.Models.DTOs;
using AudioPool.Models.InputModels;

namespace AudioPool.Services.Interfaces;

public interface IAlbumService
{
    AlbumDetailsDto? GetAlbumById(int id);
    IEnumerable<AlbumDto>? GetAlbumsByArtistId(int artistId);
    AlbumDto? CreateAlbum(AlbumInputModel model);
    bool DeleteAlbum(int id);
}