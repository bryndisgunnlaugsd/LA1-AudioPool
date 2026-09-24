using AudioPool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AudioPool.WebApi.Controllers;

[ApiController]
[Route("api/songs")]
public class SongsController : ControllerBase
{
    private readonly ISongService _songService;

    public SongsController(ISongService songService)
    {
        _songService = songService;
    }

    [HttpGet("{id:int}", Name = "GetSongById")]
    public IActionResult GetSongById(int id)
    {
        var song = _songService.GetSongById(id);
        if (song == null) return NotFound();

        return Ok(song);
    }
}