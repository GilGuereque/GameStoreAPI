using GameStore.Dtos;

namespace GameStore.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
        new (
        1,
        "Street Fighter II",
        "Fighting",
        19.99M,
        new DateOnly(1992, 7, 15)
    ),
    new (
        2,
        "Final Fantasy XIV",
        "Roleplaying",
        59.99M,
        new DateOnly(2010, 9, 30)
    ),
    new (
        3,
        "FIFA 23",
        "Sports",
        59.99M,
        new DateOnly(2022, 9, 27)
    ),
    new (
        4,
        "Kingdom Hearts 2",
        "Action-Adventure",
        29.99M,
        new DateOnly(2005, 12, 22)
    ),
    new (
        5,
        "Dark Souls 3",
        "Action-Adventure RPG",
        24.99M,
        new DateOnly(2016, 3, 24)
    )
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
                        .WithParameterValidation();
        
        // GET /games
        group.MapGet("/", () => games);

        // Create GET request to retrieve a specific game
        // GET /games/1
        group.MapGet("/{id}", (int id) =>
        {
            GameDto? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            if (string.IsNullOrEmpty(newGame.Name))
            {
                return Results.BadRequest("A game name is required.");
            }

            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate);

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        })
        .WithParameterValidation();

        // PUT /games
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );
            // return no content as we are only updating
            return Results.NoContent();
        });

        // DELETE /games/1
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}
