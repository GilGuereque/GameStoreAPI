using Microsoft.EntityFrameworkCore;

namespace GameStore.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options)
     : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
}
