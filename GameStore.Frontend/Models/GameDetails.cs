namespace GameStore.Frontend.Models;

public class GameDetails
{
    public int Id { get; set; }

    public required string Name { get; set; } // anywhere we try to create a GameSummary this will be required

    public int? GenreId { get; set;}

    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }

}