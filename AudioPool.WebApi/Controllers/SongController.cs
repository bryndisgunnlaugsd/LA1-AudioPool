using AudioPool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AudioPool.Models.InputModels;
using AudioPool.WebApi.Attributes;

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

    [ApiTokenAuthorization]
    [HttpPost("")]
    public IActionResult CreateSong(SongInputModel model)
    {
        var song = _songService.CreateSong(model);
        if (song == null) return NotFound("Album not found.");

        return CreatedAtAction(nameof(GetSongById), new { id = song.Id }, song);
    }

    [ApiTokenAuthorization]
    [HttpPut("{id:int}")]
    public IActionResult UpdateSong(int id, SongInputModel model)
    {
        var song = _songService.UpdateSong(id, model);
        if (song == null) return NotFound();

        return Ok(song);
    }

    [ApiTokenAuthorization]
    [HttpDelete("{id:int}")]
    public IActionResult DeleteSong(int id)
    {
        var deleted = _songService.DeleteSong(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}