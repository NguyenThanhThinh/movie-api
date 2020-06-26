using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace movie.Data
{
    public class MovieDbContextFactory : IDesignTimeDbContextFactory<MovieDbContext>
	{


		MovieDbContext IDesignTimeDbContextFactory<MovieDbContext>.CreateDbContext(string[] args)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = configuration.GetConnectionString("MovieDbContext");

			var optionsBuilder = new DbContextOptionsBuilder<MovieDbContext>();
			optionsBuilder.UseSqlServer(connectionString);

			return new MovieDbContext(optionsBuilder.Options);
		}
	}
}
