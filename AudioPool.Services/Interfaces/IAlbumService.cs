using AudioPool.Models.DTOs;

namespace AudioPool.Services.Interfaces;

public interface IAlbumService
{
    IEnumerable<AlbumDto>? GetAlbumsByArtistId(int artistId);
}