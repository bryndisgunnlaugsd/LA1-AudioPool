using AudioPool.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AudioPool.Models.InputModels;
using AudioPool.WebApi.Attributes;

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

    [ApiTokenAuthorization]
    [HttpPost]

    public IActionResult CreateArtist(ArtistInputModel model)
    {
        var artist = _artistService.CreateArtist(model);

        return CreatedAtAction(nameof(GetArtistById),
        new {id = artist.Id }, 
        artist);
    }
    [ApiTokenAuthorization]
    [HttpPut("{id:int}")]
    public IActionResult UpdateArtist(int id, ArtistInputModel model)
    {
       var artist = _artistService.UpdateArtist(id, model);
        if (artist == null)
        {
            return NotFound();
        }
    
        return Ok(artist);
    }

    [ApiTokenAuthorization]
    [HttpPut("{artistId:int}/genres/{genreId:int}")]
    public IActionResult LinkArtistToGenre(int artistId, int genreId)
    {
    
        bool status = _artistService.LinkArtistToGenre(artistId, genreId);

        if (status)
        {
            return NoContent();
        }
        
        return NotFound();
    }

}