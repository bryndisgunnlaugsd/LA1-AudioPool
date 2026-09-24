using AudioPool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
}