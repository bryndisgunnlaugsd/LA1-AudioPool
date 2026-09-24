using AudioPool.Models.DTOs;

namespace AudioPool.Repositories.Interfaces;

public interface IGenreRepository
{
    IEnumerable<GenreDto> GetAllGenres();
    GenreDetailsDto? GetGenreById(int id);
    IEnumerable<int> GetArtistIdsByGenreId(int genreId);
}