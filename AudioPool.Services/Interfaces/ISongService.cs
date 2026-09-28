using AudioPool.Models.DTOs;
using AudioPool.Models.InputModels;

namespace AudioPool.Services.Interfaces;

public interface ISongService
{
    SongDetailsDto? GetSongById(int id);
    IEnumerable<SongDto>? GetSongsByAlbumId(int albumId);
    SongDto? CreateSong(SongInputModel model);
    SongDto? UpdateSong(int id, SongInputModel model);
    bool DeleteSong(int id);
}