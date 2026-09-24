using System.Dynamic;
using AudioPool.Models;
using AudioPool.Models.DTOs;
using AudioPool.Repositories.Interfaces;
using AudioPool.Services.Interfaces;

namespace AudioPool.Services.Implementations;

public class ArtistService : IArtistService
{
    private readonly IArtistRepository _artistRepository;

    public ArtistService(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public Envelope<ArtistDto> GetAllArtists(int pageNumber, int pageSize)
    {
        var artists = _artistRepository.GetAllArtists();
        var envelope = new Envelope<ArtistDto>(pageNumber, pageSize, artists);

        // Only add links to the artists on the requested page
        foreach (var artist in envelope.Items)
        {
            AddArtistLinks(artist.Links, artist.Id);
        }

        return envelope;
    }

    public ArtistDetailsDto? GetArtistById(int id)
    {
        var artist = _artistRepository.GetArtistById(id);
        if (artist == null) return null;

        AddArtistLinks(artist.Links, artist.Id);
        return artist;
    }

    private void AddArtistLinks(ExpandoObject links, int artistId)
    {
        links.AddReference("self", $"/api/artists/{artistId}");
        links.AddReference("edit", $"/api/artists/{artistId}");
        links.AddReference("delete", $"/api/artists/{artistId}");
        links.AddReference("albums", $"/api/artists/{artistId}/albums");
        links.AddListReference("genres",
            _artistRepository.GetGenreIdsByArtistId(artistId).Select(genreId => $"/api/genres/{genreId}"));
    }
}