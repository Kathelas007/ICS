using Microsoft.EntityFrameworkCore;
using Movies.DAL.Entities;

namespace Movies.DAL
{
    public class MoviesDbContext: DbContext
    {
        public MoviesDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<RatingEntity> Ratings { get; set; }
        public DbSet<PersonDirectsMovieEntity> PersonsDirectMovies { get; set; }
        public DbSet<PersonPlaysMovieEntity> PersonsPlayMovies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            Seeds.Movies.Seed(modelBuilder);
            Seeds.Persons.Seed(modelBuilder);

            Seeds.PersonPlaysMovie.Seed(modelBuilder);
            Seeds.PersonDirectsMovie.Seed(modelBuilder);

            Seeds.Ratings.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }

}
