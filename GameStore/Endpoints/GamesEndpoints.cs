using GameStore.Data;
using GameStore.Dtos;
using GameStore.Entities;
using GameStore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
                        .WithParameterValidation();
        
        // GET /games
        group.MapGet("/", (GameStoreContext dbContext) => 
            dbContext.Games
                    .Include(game => game.Genre) //Include GenreId so we can query games
                    .Select(game => game.ToGameSummaryDto()) //query all games
                    .AsNoTracking()); //telling .NET we don't need to do any tracking

        // Create GET request to retrieve a specific game
        // GET /games/1
        group.MapGet("/{id}", (int id, GameStoreContext dbContext) =>
        {
            Game? game = dbContext.Games.Find(id);

            return game is null ? 
                Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            if (string.IsNullOrEmpty(newGame.Name)) //may have to comment out
            {
                return Results.BadRequest("A game name is required.");
            }

            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            dbContext.SaveChanges(); //translates to SQL to insert the new record to DB

            return Results.CreatedAtRoute(
                GetGameEndpointName,
                new { id = game.Id },
                game.ToGameDetailsDto());
        })
        .WithParameterValidation();

        // PUT /games
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = dbContext.Games.Find(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame)
                .CurrentValues
                .SetValues(updatedGame.ToEntity(id));
            
            dbContext.SaveChanges(); //make sure changes are saved back in the Db
            // return no content as we are only updating
            return Results.NoContent();
        });

        // DELETE /games/1
        group.MapDelete("/{id}", (int id, GameStoreContext dbContext) =>
        {
            //This function is known as batch delete
            dbContext.Games
                    .Where(game => game.Id == id) //provide the where condition of what we want to delete
                    .ExecuteDelete(); //delete the above game

            return Results.NoContent();
        });

        return group;
    }
}
