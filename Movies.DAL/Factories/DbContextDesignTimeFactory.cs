using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Movies.DAL.Interfaces;

namespace Movies.DAL.Factories
{
    public class DbContextDesignTimeFactory : IDesignTimeDbContextFactory<MoviesDbContext>
    {
        public MoviesDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MoviesDbContext>();
            optionsBuilder.UseSqlServer(@"
                Data Source=(LocalDB)\MSSQLLocalDB;
                Initial Catalog = Movies;
                MultipleActiveResultSets = True;
                Integrated Security = True; 
            ");
            optionsBuilder.EnableSensitiveDataLogging();
            return new MoviesDbContext(optionsBuilder.Options);
        }
    }
}