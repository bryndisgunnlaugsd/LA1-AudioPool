using System.Dynamic;
using AudioPool.Models;
using AudioPool.Models.DTOs;
using AudioPool.Repositories.Interfaces;
using AudioPool.Services.Interfaces;

namespace AudioPool.Services.Implementations;

public class AlbumService : IAlbumService
{
    private readonly IAlbumRepository _albumRepository;

    public AlbumService(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public IEnumerable<AlbumDto>? GetAlbumsByArtistId(int artistId)
    {
        var albums = _albumRepository.GetAlbumsByArtistId(artistId)?.ToList();
        if (albums == null) return null;

        foreach (var album in albums)
        {
            AddAlbumLinks(album.Links, album.Id);
        }

        return albums;
    }

    private void AddAlbumLinks(ExpandoObject links, int albumId)
    {
        links.AddReference("self", $"/api/albums/{albumId}");
        links.AddReference("delete", $"/api/albums/{albumId}");
        links.AddReference("songs", $"/api/albums/{albumId}/songs");
        links.AddListReference("artists",
            _albumRepository.GetArtistIdsByAlbumId(albumId).Select(artistId => $"/api/artists/{artistId}"));
    }
}