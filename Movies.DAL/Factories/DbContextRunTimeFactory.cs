using Microsoft.EntityFrameworkCore;
using Movies.DAL.Interfaces;

namespace Movies.DAL.Factories
{
    public class DbContextRunTimeFactory : IDbContextFactory
    {
        public MoviesDbContext Create()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MoviesDbContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog = Movies;MultipleActiveResultSets = True;Integrated Security = True;");
            return new MoviesDbContext(optionsBuilder.Options);
        }
    }
}