namespace GameStore.Frontend.Models;

public class GameSummary
{
    public int Id { get; set; }

    public required string Name { get; set; } // anywhere we try to create a GameSummary this will be required

    public required string Genre { get; set;}

    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }

}