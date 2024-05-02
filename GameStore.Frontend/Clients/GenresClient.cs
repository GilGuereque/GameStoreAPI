namespace GameStore.Frontend.Models;

public class GenresClient
{
    private readonly GenresClient[] genres =
    [
        new (){
            id = 1,
            Name = "Fighting"
        },
        new (){
            id = 2,
            Name = "Roleplaying"
        },
        new (){
            id = 3,
            Name = "Sports"
        },
        new (){
            id = 4,
            Name = "Racing"
        },
        new (){
            id = 5,
            Name = "Kids and Family"
        }
    ];

    public Genres[] GetGenres() => genres;
}