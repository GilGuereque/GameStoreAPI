// Entry point file for the GameStore API
using GameStore.Data;
using GameStore.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// DB Connection String
var connString = builder.Configuration.GetConnectionString("GameStore");
//Add services for the GameStoreContext and connecting to the DB file with the conn string (utilizies Dependency Injection)
builder.Services.AddSqlite<GameStoreContext>(connString); 

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();