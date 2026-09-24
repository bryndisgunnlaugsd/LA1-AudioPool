using AudioPool.Models.DTOs;
using AudioPool.Repositories.Contexts;
using AudioPool.Repositories.Interfaces;

namespace AudioPool.Repositories.Implementations;

public class ArtistRepository : IArtistRepository
{
    private readonly AudioPoolDbContext _dbContext;

    public ArtistRepository(AudioPoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<ArtistDto> GetAllArtists()
    {
        return _dbContext.Artists
            .OrderByDescending(a => a.DateOfStart)
            .Select(a => new ArtistDto
            {
                Id = a.Id,
                Name = a.Name,
                Bio = a.Bio,
                CoverImageUrl = a.CoverImageUrl,
                DateOfStart = a.DateOfStart
            })
            .ToList();
    }

    public ArtistDetailsDto? GetArtistById(int id)
    {
        return _dbContext.Artists
            .Where(a => a.Id == id)
            .Select(a => new ArtistDetailsDto
            {
                Id = a.Id,
                Name = a.Name,
                Bio = a.Bio,
                CoverImageUrl = a.CoverImageUrl,
                DateOfStart = a.DateOfStart,
                Albums = a.Albums
                    .OrderBy(al => al.Id)
                    .Select(al => new AlbumDto
                    {
                        Id = al.Id,
                        Name = al.Name,
                        ReleaseDate = al.ReleaseDate,
                        CoverImageUrl = al.CoverImageUrl,
                        Description = al.Description
                    })
                    .ToList(),
                Genres = a.Genres
                    .OrderBy(g => g.Id)
                    .Select(g => new GenreDto
                    {
                        Id = g.Id,
                        Name = g.Name
                    })
                    .ToList()
            })
            .FirstOrDefault();
    }

    public IEnumerable<int> GetGenreIdsByArtistId(int artistId)
    {
        return _dbContext.Artists
            .Where(a => a.Id == artistId)
            .SelectMany(a => a.Genres.Select(g => g.Id))
            .ToList();
    }
}