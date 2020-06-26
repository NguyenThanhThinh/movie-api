using Microsoft.EntityFrameworkCore;
using movie.Data.Entities;

namespace movie.Data
{
    public class MovieDbContext:DbContext
    {
        public MovieDbContext(DbContextOptions options)
          : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
