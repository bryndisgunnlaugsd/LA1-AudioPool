using AudioPool.Models.DTOs;
using AudioPool.Repositories.Contexts;
using AudioPool.Repositories.Interfaces;

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
}