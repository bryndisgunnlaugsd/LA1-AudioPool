using AudioPool.Models.DTOs;
using AudioPool.Models.InputModels;
namespace AudioPool.Repositories.Interfaces;

public interface IGenreRepository
{
    IEnumerable<GenreDto> GetAllGenres();
    GenreDetailsDto? GetGenreById(int id);
    IEnumerable<int> GetArtistIdsByGenreId(int genreId);
    GenreDto CreateGenre(GenreInputModel model);
}