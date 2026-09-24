using AudioPool.Models.DTOs;

namespace AudioPool.Services.Interfaces;

public interface IGenreService
{
    IEnumerable<GenreDto> GetAllGenres();
    GenreDetailsDto? GetGenreById(int id);
}