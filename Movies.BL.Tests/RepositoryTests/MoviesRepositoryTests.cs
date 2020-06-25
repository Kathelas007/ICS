using System;
using System.Collections.Generic;
using System.Linq;
using Movies.BL.Interfaces;
using Movies.BL.Mappers;
using Movies.BL.Models;
using Movies.BL.Repositories;
using Movies.DAL.Entities;
using Xunit;

namespace Movies.BL.Tests.RepositoryTests
{
    public class MoviesRepositoryTests : TestsBase
    {
        private readonly IRepository<MovieEntity, MovieListModel, MovieDetailModel> repository;

        public MoviesRepositoryTests() : base()
        {
            repository = new MovieRepository(dbContextFactory);
        }

        [Fact]
        public void GetALL_List_NotEmpty()
        {
            var models = repository.GetAll();
            
            Assert.NotEmpty(models);
        }

        [Fact]
        public void GetById_Detail_Matches()
        {
            var lotr = DAL.Seeds.Movies.LordOfTheRings;
            var rating = DAL.Seeds.Ratings.LOTRRating;

            var viggoPlays = DAL.Seeds.PersonPlaysMovie.MortensenPlaysLordOfTheRings;
            var viggo2Plays = DAL.Seeds.PersonPlaysMovie.Mortensen2PlaysLordOfTheRings;

            viggoPlays.Actor = DAL.Seeds.Persons.Mortensen;
            viggoPlays.Movie = lotr;
            viggo2Plays.Actor = DAL.Seeds.Persons.MortensenDuplicate;
            viggo2Plays.Movie = lotr;

            var model = new MovieDetailModel()
            {
                Id = lotr.Id,
                CzechName = lotr.CzechName,
                OriginalName = lotr.OriginalName,
                Description = lotr.Description,
                Duration = lotr.Duration,
                Genre = lotr.Genre,
                Country = lotr.Country,
                Photo = lotr.Photo,
                Actors = new List<ActorMovieModel>()
                {
                    ActorMovieMapper.MapToModel(viggoPlays),
                    ActorMovieMapper.MapToModel(viggo2Plays),
                },
                Ratings = new List<RatingListModel>()
                {
                    new RatingListModel()
                    {
                        Id = rating.Id,
                        Text = rating.Text,
                        Rating = rating.Rating
                    }
                }
            };
            
            var returnedModel = repository.GetById(lotr.Id);

            Assert.Equal(model, returnedModel, MovieDetailModel.MovieDetailComparer);
        }

        [Fact]
        public void DeleteById_Model_NotPersisted()
        {
            var lotr = DAL.Seeds.Movies.LordOfTheRingsDuplicate;

            repository.Delete(lotr.Id);

            var returnedModel = repository.GetById(lotr.Id);
            
            Assert.Null(returnedModel);

        }

        [Fact]
        public void DeleteByModel_Model_NotPersisted()
        {
            var lotr = DAL.Seeds.Movies.HarryPotter;

            var model = new MovieDetailModel()
            {
                Id = lotr.Id,
                CzechName = lotr.CzechName,
                OriginalName = lotr.OriginalName,
                Description = lotr.Description,
                Duration = lotr.Duration,
                Genre = lotr.Genre,
                Country = lotr.Country,
                Photo = lotr.Photo
            };

            repository.Delete(model);

            var returnedModel = repository.GetById(lotr.Id);

            Assert.Null(returnedModel);

        }

        [Fact]
        public void Insert_Model_DidntThrow()
        {
            var model = new MovieDetailModel()
            {
                CzechName = "Temný rytíø",
                OriginalName = "The Dark Knight",
                Description =
                    "Akèní dobrodružná fantasy v režii Christophera Nolana navazuje na film Batman zaèíná a Christian Bale si v ní zopakoval roli Batmana/Bruce Waynea v nekonèícím boji se zloèinem. Wayne v kostýmu netopýøího muže jde spolu s poruèíkem Jamesem Gordonem (Gary Oldman) z Gothamské policie po králi zloèincù Jokerovi známého podle symbolu - hrací karty s žolíkem - kterou zanechává na místech èinu. Vtipálek s proøízlou tváøí (Heath Ledger) má tentokrát v plánu Batmanovi dokázat, že slova Gotham a mír nikdy nebudou stát v jedné vìtì vedle sebe. Na uvolnìné místo po bývalém králi místní mafie usedá Sal Maroni (Eric Roberts) a pøebírá vládu nad vìtšinou gangù v Gothamu. Na scénì se rovnìž objevuje nový státní návladní Harvey Dent (Aaron Eckhart), který by rozdanými kartami (i s žolíkem v balíèku) mohl poøádnì zamíchat...",
                Duration = TimeSpan.FromMinutes(152),
                Genre = "Akèní, Drama, Krimi, Thriller",
                Country = "USA / Velká Británie",
                Photo = "photo.png",
                Actors = new []
                {  
                    new ActorMovieModel(){
                        Actor =  new PersonListModel()
                        {
                            FirstName = "Heath",
                            LastName = "Ledger"
                        }
                    }

                },
                Directors = new []
                {
                    new DirectorMovieModel(){ 
                        Director = new PersonListModel()
                        {
                            FirstName = "Christopher",
                            LastName = "Nolan"
                        }
                    }
                },
                Ratings = new List<RatingListModel>()
                {
                    new RatingListModel()
                    {
                        Rating = 1000,
                        Text = "Cool"
                    }
                }
            };

            var returnedModel = repository.InsertOrUpdate(model);

            Assert.NotNull(returnedModel);

        }
                
        [Fact]
        public void Insert_Model_Persisted()
        {
            var movieEntity = DAL.Seeds.Movies.NoNameFilm;
            var model = MovieMapper.MapToDetail(movieEntity);

            var returnedModel = repository.InsertOrUpdate(model);
            var insertedModel = repository.GetById(returnedModel.Id);

            Assert.NotEqual(Guid.Empty, insertedModel.Id);
        }

        [Fact]
        public void Update_Model_DidntThrow()
        {
            var lotr = DAL.Seeds.Movies.LordOfTheRings;
            var viggo = DAL.Seeds.Persons.Mortensen;
            var viggo2 = DAL.Seeds.Persons.MortensenDuplicate;
            var rating = DAL.Seeds.Ratings.LOTRRating;

            var model = new MovieDetailModel()
            {
                Id = lotr.Id,
                CzechName = lotr.CzechName,
                OriginalName = "LOTR",
                Description = lotr.Description,
                Duration = lotr.Duration,
                Genre = lotr.Genre,
                Country = lotr.Country,
                Photo = lotr.Photo,
                Actors = new[]
                {
                    new ActorMovieModel(){
                        Actor =  new PersonListModel()
                        {
                            Id = viggo.Id,
                            FirstName = viggo.FirstName,
                            LastName = viggo.LastName
                        }
                    },
                    new ActorMovieModel()
                    {
                        Actor = new PersonListModel()
                        {
                            Id = viggo2.Id,
                            FirstName = viggo2.FirstName,
                            LastName = viggo2.LastName
                        }
                    }
                },
                Ratings = new[]
                {
                    new RatingListModel()
                    {
                        Id = rating.Id,
                        Text = rating.Text,
                        Rating = rating.Rating
                    }
                }
            };

            var returnedModel = repository.InsertOrUpdate(model);

            Assert.NotNull(returnedModel);
        }

        [Fact]
        public void Update_Model_Edited()
        {
            var newOriginalName = "LOTR";

            var lotr = DAL.Seeds.Movies.LordOfTheRings;
            var rating = DAL.Seeds.Ratings.LOTRRating;

            var viggoPlays = DAL.Seeds.PersonPlaysMovie.MortensenPlaysLordOfTheRings;
            var viggo2Plays = DAL.Seeds.PersonPlaysMovie.Mortensen2PlaysLordOfTheRings;

            var newLOTR = lotr;
            newLOTR.OriginalName = newOriginalName;

            viggoPlays.Actor = DAL.Seeds.Persons.Mortensen;
            viggoPlays.Movie = newLOTR;
            viggo2Plays.Actor = DAL.Seeds.Persons.MortensenDuplicate;
            viggo2Plays.Movie = newLOTR;

            var model = new MovieDetailModel()
            {
                Id = lotr.Id,
                CzechName = lotr.CzechName,
                OriginalName = newOriginalName,
                Description = lotr.Description,
                Duration = lotr.Duration,
                Genre = lotr.Genre,
                Country = lotr.Country,
                Photo = lotr.Photo,
                Actors = new List<ActorMovieModel>()
                {
                    ActorMovieMapper.MapToModel(viggoPlays),
                    ActorMovieMapper.MapToModel(viggo2Plays)
                },
                Ratings = new List<RatingListModel>()
                {
                    new RatingListModel()
                    {
                        Id = rating.Id,
                        Text = rating.Text,
                        Rating = rating.Rating
                    }
                }
            };

            var returnedModel = repository.InsertOrUpdate(model);
            var editedModel = repository.GetById(returnedModel.Id);

            Assert.Equal(model, editedModel, MovieDetailModel.MovieDetailComparer);
        }
    }
}
