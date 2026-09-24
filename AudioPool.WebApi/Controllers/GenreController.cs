using AudioPool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AudioPool.WebApi.Controllers;

[ApiController]
[Route("api/genres")]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenresController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet("")]
    public IActionResult GetAllGenres()
    {
        return Ok(_genreService.GetAllGenres());
    }

    [HttpGet("{id:int}", Name = "GetGenreById")]
    public IActionResult GetGenreById(int id)
    {
        var genre = _genreService.GetGenreById(id);
        if (genre == null) return NotFound();

        return Ok(genre);
    }
}