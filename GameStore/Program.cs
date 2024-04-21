using GameStore.Dtos;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

List<GameDto> games = [
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

app.MapGet("games", () => games);

app.MapGet("/", () => "Hello World!");

app.Run();
