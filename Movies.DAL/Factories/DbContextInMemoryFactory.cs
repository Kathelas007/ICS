using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Movies.DAL.Interfaces;

namespace Movies.DAL.Factories
{
    public class DbContextInMemoryFactory : IDbContextFactory
    {
        public MoviesDbContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MoviesDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "MoviesInMemoryDatabase");
            optionsBuilder.EnableSensitiveDataLogging();
            return new MoviesDbContext(optionsBuilder.Options);
        }
    }
}