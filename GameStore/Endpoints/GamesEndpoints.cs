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
        group.MapGet("/", async (GameStoreContext dbContext) => 
            await dbContext.Games
                    .Include(game => game.Genre) //Include GenreId so we can query games
                    .Select(game => game.ToGameSummaryDto()) //query all games
                    .AsNoTracking()
                    .ToListAsync()); //telling .NET we don't need to do any tracking

        // Create GET request to retrieve a specific game
        // GET /games/1
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ? 
                Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            if (string.IsNullOrEmpty(newGame.Name)) //may have to comment out
            {
                return Results.BadRequest("A game name is required.");
            }

            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync(); //translates to SQL to insert the new record to DB

            return Results.CreatedAtRoute(
                GetGameEndpointName,
                new { id = game.Id },
                game.ToGameDetailsDto());
        })
        .WithParameterValidation();

        // PUT /games
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame)
                .CurrentValues
                .SetValues(updatedGame.ToEntity(id));
            
            await dbContext.SaveChangesAsync(); //make sure changes are saved back in the Db
            // return no content as we are only updating
            return Results.NoContent();
        });

        // DELETE /games/1
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            //This function is known as batch delete
            await dbContext.Games
                    .Where(game => game.Id == id) //provide the where condition of what we want to delete
                    .ExecuteDeleteAsync(); //delete the above game

            return Results.NoContent();
        });

        return group;
    }
}
