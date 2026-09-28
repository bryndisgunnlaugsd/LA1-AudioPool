using AudioPool.Models.DTOs;
using AudioPool.Models.InputModels;

namespace AudioPool.Services.Interfaces;

public interface IGenreService
{
    IEnumerable<GenreDto> GetAllGenres();
    GenreDetailsDto? GetGenreById(int id);

    GenreDto CreateGenre(GenreInputModel model);
}