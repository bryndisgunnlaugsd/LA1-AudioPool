using System.ComponentModel.DataAnnotations;

namespace AudioPool.Models.InputModels;

public class GenreInputModel
{
    [Required]
    [MinLength(3)]
    public string Name { get; set; } = null!;
}