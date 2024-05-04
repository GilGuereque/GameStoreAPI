using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient
{
    // Changed GameSummary to a List as it is dynamic changing array instead
    private readonly List<GameSummary> games = new List<GameSummary>
    {
        new GameSummary {
            Id = 1,
            Name = "Street Fighter II",
            Genre = "Fighting",
            Price = 19.99M,
            ReleaseDate = new DateOnly(1992, 7, 15)
        },
        new GameSummary {
            Id = 2,
            Name = "Final Fantasy XIV",
            Genre = "Roleplaying",
            Price = 59.99M,
            ReleaseDate = new DateOnly(2010, 9, 30)
        },
        new GameSummary {
            Id = 3,
            Name = "FIFA 23",
            Genre = "Sports",
            Price = 69.99M,
            ReleaseDate = new DateOnly(2022, 9, 27)
        }
    };

    private readonly Genre[] genres = new GenresClient().GetGenres();

    public GameSummary[] GetGames() => [.. games];

    public void AddGame(GameDetails game)
    {
        if (game.GenreId == null)
            throw new ArgumentException("Genre ID cannot be null");
            // ArgumentException.ThrowIfNullOrWhiteSpace(game.GenreId); // throw exception if null Id

            var genre = genres.Single(genre => genre.Id == int.Parse(game.GenreId.Value.ToString()));

            var gameSummary = new GameSummary
            {
                Id = games.Count + 1,
                Name = game.Name,
                Genre = genre.Name,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate
            };

            games.Add(gameSummary);
    }
}