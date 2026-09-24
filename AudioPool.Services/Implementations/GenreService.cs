using System.Dynamic;
using AudioPool.Models;
using AudioPool.Models.DTOs;
using AudioPool.Repositories.Interfaces;
using AudioPool.Services.Interfaces;

namespace AudioPool.Services.Implementations;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;

    public GenreService(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public IEnumerable<GenreDto> GetAllGenres()
    {
        var genres = _genreRepository.GetAllGenres().ToList();

        foreach (var genre in genres)
        {
            AddGenreLinks(genre.Links, genre.Id);
        }

        return genres;
    }

    public GenreDetailsDto? GetGenreById(int id)
    {
        var genre = _genreRepository.GetGenreById(id);
        if (genre == null) return null;

        AddGenreLinks(genre.Links, genre.Id);
        return genre;
    }

    private void AddGenreLinks(ExpandoObject links, int genreId)
    {
        links.AddReference("self", $"/api/genres/{genreId}");
        links.AddListReference("artists",
            _genreRepository.GetArtistIdsByGenreId(genreId).Select(artistId => $"/api/artists/{artistId}"));
    }
}