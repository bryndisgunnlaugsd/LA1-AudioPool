using AudioPool.Models.DTOs;
using AudioPool.Repositories.Contexts;
using AudioPool.Repositories.Interfaces;
using AudioPool.Models.Entities;
using AudioPool.Models.InputModels;

namespace AudioPool.Repositories.Implementations;

public class AlbumRepository : IAlbumRepository
{
    private readonly AudioPoolDbContext _dbContext;

    public AlbumRepository(AudioPoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public AlbumDetailsDto? GetAlbumById(int id)
    {
        return _dbContext.Albums
            .Where(al => al.Id == id)
            .Select(al => new AlbumDetailsDto
            {
                Id = al.Id,
                Name = al.Name,
                ReleaseDate = al.ReleaseDate,
                CoverImageUrl = al.CoverImageUrl,
                Description = al.Description,
                Artists = al.Artists
                    .OrderBy(a => a.Id)
                    .Select(a => new ArtistDto
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Bio = a.Bio,
                        CoverImageUrl = a.CoverImageUrl,
                        DateOfStart = a.DateOfStart
                    })
                    .ToList(),
                Songs = al.Songs
                    .OrderBy(s => s.Id)
                    .Select(s => new SongDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Duration = s.Duration
                    })
                    .ToList()
            })
            .FirstOrDefault();
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

    public AlbumDto? CreateAlbum(AlbumInputModel model)
    {
        var artistIds = model.ArtistIds.Distinct().ToList();
        var artists = _dbContext.Artists
            .Where(a => artistIds.Contains(a.Id))
            .ToList();

        // Every artist id in the request must exist, otherwise the controller returns 404
        if (artists.Count != artistIds.Count) return null;

        var album = new Album
        {
            Name = model.Name,
            ReleaseDate = model.ReleaseDate!.Value,
            CoverImageUrl = model.CoverImageUrl,
            Description = model.Description,
            DateCreated = DateTime.UtcNow,
            ModifiedBy = "AudioPoolAdmin",
            Artists = artists
        };

        _dbContext.Albums.Add(album);
        _dbContext.SaveChanges();

        return new AlbumDto
        {
            Id = album.Id,
            Name = album.Name,
            ReleaseDate = album.ReleaseDate,
            CoverImageUrl = album.CoverImageUrl,
            Description = album.Description
        };
    }

    public bool DeleteAlbum(int id)
    {
        var album = _dbContext.Albums.Find(id);
        if (album == null) return false;

        // Songs and AlbumArtist rows are removed by the cascade delete set up in the migration
        _dbContext.Albums.Remove(album);
        _dbContext.SaveChanges();
        return true;
    }
}