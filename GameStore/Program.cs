// Entry point file for the GameStore API
using GameStore.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
