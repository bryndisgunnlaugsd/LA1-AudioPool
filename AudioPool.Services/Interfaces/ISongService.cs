using AudioPool.Models.DTOs;

namespace AudioPool.Services.Interfaces;

public interface ISongService
{
    IEnumerable<SongDto>? GetSongsByAlbumId(int albumId);
}