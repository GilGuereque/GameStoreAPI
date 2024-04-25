namespace GameStore.Entities;

public class Genre
{
    public int Id { get; set; }

    public required string? Name { get; set; }

    public int GenreId { get; set; }
    
}
