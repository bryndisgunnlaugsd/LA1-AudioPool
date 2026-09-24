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

    public IEnumerable<SongDto>? GetSongsByAlbumId(int albumId)
    {
        // Return null if the album doesn't exist so the controller can respond with 404
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