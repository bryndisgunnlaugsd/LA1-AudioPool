using AudioPool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AudioPool.Models.InputModels;
using AudioPool.WebApi.Attributes;

namespace AudioPool.WebApi.Controllers;

[ApiController]
[Route("api/albums")]
public class AlbumsController : ControllerBase
{
    private readonly IAlbumService _albumService;
    private readonly ISongService _songService;

    public AlbumsController(IAlbumService albumService, ISongService songService)
    {
        _albumService = albumService;
        _songService = songService;
    }

    [HttpGet("{id:int}", Name = "GetAlbumById")]
    public IActionResult GetAlbumById(int id)
    {
        var album = _albumService.GetAlbumById(id);
        if (album == null) return NotFound();

        return Ok(album);
    }

    [HttpGet("{id:int}/songs")]
    public IActionResult GetSongsByAlbumId(int id)
    {
        var songs = _songService.GetSongsByAlbumId(id);
        if (songs == null) return NotFound();

        return Ok(songs);
    }

    [ApiTokenAuthorization]
    [HttpPost("")]
    public IActionResult CreateAlbum(AlbumInputModel model)
    {
        var album = _albumService.CreateAlbum(model);
        if (album == null) return NotFound("One or more artists were not found.");

        return CreatedAtAction(nameof(GetAlbumById), new { id = album.Id }, album);
    }

    [ApiTokenAuthorization]
    [HttpDelete("{id:int}")]
    public IActionResult DeleteAlbum(int id)
    {
        var deleted = _albumService.DeleteAlbum(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}