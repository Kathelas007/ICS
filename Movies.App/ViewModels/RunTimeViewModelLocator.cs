using System.Linq;
using Microsoft.EntityFrameworkCore;
using Movies.BL.Repositories;
using Movies.BL.Services;
using Movies.DAL.Factories;
using Movies.DAL.Interfaces;

namespace Movies.App.ViewModels
{
    public class RunTimeViewModelLocator
    {
        public MovieListViewModel MovieListViewModel { get; }
        public PersonListViewModel PersonListViewModel { get; }
        public RatingListViewModel RatingListViewModel { get; }
        public MovieDetailViewModel MovieDetailViewModel { get; }
        public PersonDetailViewModel PersonDetailViewModel { get; }
        public RatingDetailViewModel RatingDetailViewModel { get; }
        public SearchViewModel SearchViewModel { get; }

        protected readonly IDbContextFactory DbContextFactory = new DbContextRunTimeFactory();

        public RunTimeViewModelLocator()
        {
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
            SearchViewModel = new SearchViewModel(MovieListViewModel.Id, PersonListViewModel.Id, RatingListViewModel.Id, mediator);

            using var context = DbContextFactory.Create();
            context.Database.Migrate();
        }
    }
}
