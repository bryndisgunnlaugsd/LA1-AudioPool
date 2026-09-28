using AudioPool.Models.DTOs;
using AudioPool.Repositories.Contexts;
using AudioPool.Repositories.Interfaces;
using AudioPool.Models.Entities;
using AudioPool.Models.InputModels;

namespace AudioPool.Repositories.Implementations;

public class GenreRepository : IGenreRepository
{
    private readonly AudioPoolDbContext _dbContext;

    public GenreRepository(AudioPoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<GenreDto> GetAllGenres()
    {
        return _dbContext.Genres
            .OrderBy(g => g.Id)
            .Select(g => new GenreDto
            {
                Id = g.Id,
                Name = g.Name
            })
            .ToList();
    }

    public GenreDetailsDto? GetGenreById(int id)
    {
        return _dbContext.Genres
            .Where(g => g.Id == id)
            .Select(g => new GenreDetailsDto
            {
                Id = g.Id,
                Name = g.Name,
                NumberOfArtists = g.Artists.Count
            })
            .FirstOrDefault();
    }

    public IEnumerable<int> GetArtistIdsByGenreId(int genreId)
    {
        return _dbContext.Genres
            .Where(g => g.Id == genreId)
            .SelectMany(g => g.Artists.Select(a => a.Id))
            .ToList();
    }

    public GenreDto CreateGenre(GenreInputModel model)
{
    var genra = new Genre{
        Name = model.Name,
        DateCreated = DateTime.UtcNow,
        ModifiedBy = "AudioPoolAdmin"
    };

    _dbContext.Genres.Add(genra);
    _dbContext.SaveChanges();

    return new GenreDto{Id = genra.Id,Name = genra.Name};
}
}