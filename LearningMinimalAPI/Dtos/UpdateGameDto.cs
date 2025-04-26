using System.ComponentModel.DataAnnotations;

namespace LearningMinimalAPI.Dtos;

public record UpdateGameDto(
    [Required][StringLength(50)]string Name,
    int Genre,
    [Range(1, 100)]decimal Price,
    DateOnly ReleaseDate
);