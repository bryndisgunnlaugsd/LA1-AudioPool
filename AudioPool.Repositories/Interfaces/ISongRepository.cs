using AudioPool.Models.DTOs;

namespace AudioPool.Repositories.Interfaces;

public interface ISongRepository
{
    SongDetailsDto? GetSongById(int id);
    IEnumerable<SongDto>? GetSongsByAlbumId(int albumId);
}