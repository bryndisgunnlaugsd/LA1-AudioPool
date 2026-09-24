using AudioPool.Models.DTOs;
using AudioPool.Repositories.Contexts;
using AudioPool.Repositories.Interfaces;

namespace AudioPool.Repositories.Implementations;

public class AlbumRepository : IAlbumRepository
{
    private readonly AudioPoolDbContext _dbContext;

    public AlbumRepository(AudioPoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<AlbumDto>? GetAlbumsByArtistId(int artistId)
    {
        // Return null if the artist doesn't exist so the controller can respond with 404
        var artistExists = _dbContext.Artists.Any(a => a.Id == artistId);
        if (!artistExists) return null;

        return _dbContext.Albums
            .Where(al => al.Artists.Any(a => a.Id == artistId))
            .OrderBy(al => al.Id)
            .Select(al => new AlbumDto
            {
                Id = al.Id,
                Name = al.Name,
                ReleaseDate = al.ReleaseDate,
                CoverImageUrl = al.CoverImageUrl,
                Description = al.Description
            })
            .ToList();
    }

    public IEnumerable<int> GetArtistIdsByAlbumId(int albumId)
    {
        return _dbContext.Albums
            .Where(al => al.Id == albumId)
            .SelectMany(al => al.Artists.Select(a => a.Id))
            .ToList();
    }
}