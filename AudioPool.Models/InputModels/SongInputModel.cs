using System.ComponentModel.DataAnnotations;

namespace AudioPool.Models.InputModels;

public class SongInputModel
{
    [Required]
    [MinLength(3)]
    public string Name { get; set; } = null!;

    [Required]
    public TimeSpan? Duration { get; set; }

    [Required]
    public int? AlbumId { get; set; }
}