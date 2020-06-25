using Microsoft.EntityFrameworkCore;
using Movies.BL.Repositories;
using Movies.BL.Services;
using Movies.DAL.Factories;

namespace Movies.App.ViewModels
{
    public class DesignTimeViewModelLocator
    {
        public MovieListViewModel MovieListViewModel { get; }
        public PersonListViewModel PersonListViewModel { get; }
        public RatingListViewModel RatingListViewModel { get; }
        public MovieDetailViewModel MovieDetailViewModel { get; }
        public PersonDetailViewModel PersonDetailViewModel { get; }
        public RatingDetailViewModel RatingDetailViewModel { get; }

        protected readonly DbContextInMemoryFactory DbContextFactory = new DbContextInMemoryFactory();

        public DesignTimeViewModelLocator()
        {
            using var context = DbContextFactory.Create();
            {
                context.Database.EnsureCreated();
            }

            var movieRepository = new MovieRepository(DbContextFactory);
            var personRepository = new PersonRepository(DbContextFactory);
            var ratingRepository = new RatingRepository(DbContextFactory);
            var mediator = new Mediator();

            MovieListViewModel = new MovieListViewModel(movieRepository, mediator);
            PersonListViewModel = new PersonListViewModel(personRepository, mediator);
            RatingListViewModel = new RatingListViewModel(ratingRepository, mediator);
            MovieDetailViewModel = new MovieDetailViewModel(movieRepository, mediator);
            PersonDetailViewModel = new PersonDetailViewModel(personRepository, mediator);
            RatingDetailViewModel = new RatingDetailViewModel(ratingRepository, mediator);
        }
    }
}