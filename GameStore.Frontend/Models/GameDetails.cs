using System.ComponentModel.DataAnnotations;

namespace GameStore.Frontend.Models;

public class GameDetails
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public required string Name { get; set; } // anywhere we try to create a GameSummary this will be required

    [Required(ErrorMessage = "The Genre field is required.")]
    public int? GenreId { get; set;}

    [Range(1, 100)]
    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }

}