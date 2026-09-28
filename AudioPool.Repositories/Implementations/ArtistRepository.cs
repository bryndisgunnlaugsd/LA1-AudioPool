using AudioPool.Models.DTOs;
using AudioPool.Models.InputModels;
using AudioPool.Repositories.Contexts;
using AudioPool.Repositories.Interfaces;
using AudioPool.Models.Entities;

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

    public ArtistDto CreateArtist(ArtistInputModel model)
{
    var artist = new Artist{
        Name = model.Name,
        Bio = model.Bio,
        CoverImageUrl = model.CoverImageUrl,
        DateOfStart = model.DateOfStart.Value,
        DateCreated = DateTime.UtcNow,
        ModifiedBy = "AudioPoolAdmin"
    };

    _dbContext.Artists.Add(artist);
    _dbContext.SaveChanges();

    return new ArtistDto{
        Id = artist.Id,
        Name = artist.Name,
        Bio = artist.Bio,
        CoverImageUrl = artist.CoverImageUrl,
        DateOfStart = artist.DateOfStart
    };
}
 public ArtistDto? UpdateArtist(int id, ArtistInputModel model)
    {
        var artist = _dbContext.Artists.Find(id);

        if (artist == null)
        {
            return null;
        }

        if (model.DateOfStart == null)
        {
            throw new ArgumentException("DateOfStart is needed");
        }

        artist.Name = model.Name;
        artist.Bio = model.Bio;
        artist.CoverImageUrl = model.CoverImageUrl;
        artist.DateOfStart = model.DateOfStart.Value;;
        artist.DateModified = DateTime.UtcNow;
        artist.ModifiedBy = "AudioPoolAdmin";

        _dbContext.SaveChanges();

        return new ArtistDto
        {Id = artist.Id, Name = artist.Name, Bio = artist.Bio,
        CoverImageUrl = artist.CoverImageUrl, DateOfStart = artist.DateOfStart
        };
}

    public bool LinkArtistToGenre(int artistId, int genreId)
    {
       var genre = _dbContext.Genres.Find(genreId);
       var artist = _dbContext.Artists.Find(artistId);

       if (genre == null || artist == null){
            return false;
        }

        _dbContext.Entry(artist).Collection(a => a.Genres).Load();

        if (!artist.Genres.Any(g => g.Id == genreId)) {
            artist.Genres.Add(genre);
            artist.DateModified = DateTime.UtcNow;
            artist.ModifiedBy = "AudioPoolAdmin";
            _dbContext.SaveChanges();
        };

        return true;
    }


    };