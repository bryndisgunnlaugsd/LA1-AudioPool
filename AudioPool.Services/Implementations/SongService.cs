using System.Dynamic;
using AudioPool.Models;
using AudioPool.Models.DTOs;
using AudioPool.Repositories.Interfaces;
using AudioPool.Services.Interfaces;

namespace AudioPool.Services.Implementations;

public class SongService : ISongService
{
    private readonly ISongRepository _songRepository;

    public SongService(ISongRepository songRepository)
    {
        _songRepository = songRepository;
    }

    public SongDetailsDto? GetSongById(int id)
    {
        var song = _songRepository.GetSongById(id);
        if (song == null) return null;

        AddSongLinks(song.Links, song.Id, song.Album.Id);
        return song;
    }

    public IEnumerable<SongDto>? GetSongsByAlbumId(int albumId)
    {
        var songs = _songRepository.GetSongsByAlbumId(albumId)?.ToList();
        if (songs == null) return null;

        foreach (var song in songs)
        {
            AddSongLinks(song.Links, song.Id, albumId);
        }

        return songs;
    }

    private static void AddSongLinks(ExpandoObject links, int songId, int albumId)
    {
        links.AddReference("self", $"/api/songs/{songId}");
        links.AddReference("delete", $"/api/songs/{songId}");
        links.AddReference("edit", $"/api/songs/{songId}");
        links.AddReference("album", $"/api/albums/{albumId}");
    }
}