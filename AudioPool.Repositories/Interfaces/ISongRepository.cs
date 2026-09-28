using AudioPool.Models.DTOs;
using AudioPool.Models.InputModels;

namespace AudioPool.Repositories.Interfaces;

public interface ISongRepository
{
    SongDetailsDto? GetSongById(int id);
    IEnumerable<SongDto>? GetSongsByAlbumId(int albumId);
    SongDto? CreateSong(SongInputModel model);
    SongDto? UpdateSong(int id, SongInputModel model);
    bool DeleteSong(int id);
}