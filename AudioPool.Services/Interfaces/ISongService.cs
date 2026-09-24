using AudioPool.Models.DTOs;

namespace AudioPool.Services.Interfaces;

public interface ISongService
{
    SongDetailsDto? GetSongById(int id);
    IEnumerable<SongDto>? GetSongsByAlbumId(int albumId);
}