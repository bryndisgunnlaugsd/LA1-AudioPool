using AudioPool.Models.DTOs;
using AudioPool.Repositories.Contexts;
using AudioPool.Repositories.Interfaces;
using AudioPool.Models.Entities;
using AudioPool.Models.InputModels;

namespace AudioPool.Repositories.Implementations;

public class SongRepository : ISongRepository
{
    private readonly AudioPoolDbContext _dbContext;

    public SongRepository(AudioPoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public SongDetailsDto? GetSongById(int id)
    {
        return _dbContext.Songs
            .Where(s => s.Id == id)
            .Select(s => new SongDetailsDto
            {
                Id = s.Id,
                Name = s.Name,
                Duration = s.Duration,
                Album = new AlbumDto
                {
                    Id = s.Album.Id,
                    Name = s.Album.Name,
                    ReleaseDate = s.Album.ReleaseDate,
                    CoverImageUrl = s.Album.CoverImageUrl,
                    Description = s.Album.Description
                },
                // Position of the song in the album: count the album's songs with an id up to and including this one
                TrackNumberOnAlbum = s.Album.Songs.Count(other => other.Id <= s.Id)
            })
            .FirstOrDefault();
    }

    public IEnumerable<SongDto>? GetSongsByAlbumId(int albumId)
    {
        // Return null if the album doesn't exist, so the controller can respond with 404
        var albumExists = _dbContext.Albums.Any(al => al.Id == albumId);
        if (!albumExists) return null;

        return _dbContext.Songs
            .Where(s => s.AlbumId == albumId)
            .OrderBy(s => s.Id)
            .Select(s => new SongDto
            {
                Id = s.Id,
                Name = s.Name,
                Duration = s.Duration
            })
            .ToList();
    }

    public SongDto? CreateSong(SongInputModel model)
    {
        var albumId = model.AlbumId!.Value;
        if (!_dbContext.Albums.Any(al => al.Id == albumId)) return null;

        var song = new Song
        {
            Name = model.Name,
            Duration = model.Duration!.Value,
            AlbumId = albumId,
            DateCreated = DateTime.UtcNow,
            ModifiedBy = "AudioPoolAdmin"
        };

        _dbContext.Songs.Add(song);
        _dbContext.SaveChanges();

        return new SongDto { Id = song.Id, Name = song.Name, Duration = song.Duration };
    }

    public SongDto? UpdateSong(int id, SongInputModel model)
    {
        var song = _dbContext.Songs.Find(id);
        if (song == null) return null;

        // The song can be moved to another album, but that album has to exist
        var albumId = model.AlbumId!.Value;
        if (!_dbContext.Albums.Any(al => al.Id == albumId)) return null;

        song.Name = model.Name;
        song.Duration = model.Duration!.Value;
        song.AlbumId = albumId;
        song.DateModified = DateTime.UtcNow;
        song.ModifiedBy = "AudioPoolAdmin";

        _dbContext.SaveChanges();

        return new SongDto { Id = song.Id, Name = song.Name, Duration = song.Duration };
    }

    public bool DeleteSong(int id)
    {
        var song = _dbContext.Songs.Find(id);
        if (song == null) return false;

        _dbContext.Songs.Remove(song);
        _dbContext.SaveChanges();
        return true;
    }
}