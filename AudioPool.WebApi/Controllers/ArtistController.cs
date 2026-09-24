using AudioPool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AudioPool.WebApi.Controllers;

[ApiController]
[Route("api/artists")]
public class ArtistsController : ControllerBase
{
    private readonly IArtistService _artistService;
    private readonly IAlbumService _albumService;

    public ArtistsController(IArtistService artistService, IAlbumService albumService)
    {
        _artistService = artistService;
        _albumService = albumService;
    }

    [HttpGet("")]
    public IActionResult GetAllArtists([FromQuery] int pageSize = 25, [FromQuery] int pageNumber = 1)
    {
        if (pageSize < 1 || pageNumber < 1)
        {
            return BadRequest("pageSize and pageNumber must be greater than 0.");
        }

        return Ok(_artistService.GetAllArtists(pageNumber, pageSize));
    }

    [HttpGet("{id:int}", Name = "GetArtistById")]
    public IActionResult GetArtistById(int id)
    {
        var artist = _artistService.GetArtistById(id);
        if (artist == null) return NotFound();

        return Ok(artist);
    }

    [HttpGet("{id:int}/albums")]
    public IActionResult GetAlbumsByArtistId(int id)
    {
        var albums = _albumService.GetAlbumsByArtistId(id);
        if (albums == null) return NotFound();

        return Ok(albums);
    }
}