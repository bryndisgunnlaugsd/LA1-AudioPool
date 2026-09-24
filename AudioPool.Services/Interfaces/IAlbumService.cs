using AudioPool.Models.DTOs;

namespace AudioPool.Services.Interfaces;

public interface IAlbumService
{
    AlbumDetailsDto? GetAlbumById(int id);
    IEnumerable<AlbumDto>? GetAlbumsByArtistId(int artistId);
}