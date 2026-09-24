using AudioPool.Models.DTOs;

namespace AudioPool.Repositories.Interfaces;

public interface ISongRepository
{
    IEnumerable<SongDto>? GetSongsByAlbumId(int albumId);
}