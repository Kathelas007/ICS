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
                CzechName = "Temn� ryt��",
                OriginalName = "The Dark Knight",
                Description =
                    "Ak�n� dobrodru�n� fantasy v re�ii Christophera Nolana navazuje na film Batman za��n� a Christian Bale si v n� zopakoval roli Batmana/Bruce Waynea v nekon��c�m boji se zlo�inem. Wayne v kost�mu netop���ho mu�e jde spolu s poru��kem Jamesem Gordonem (Gary Oldman) z Gothamsk� policie po kr�li zlo�inc� Jokerovi zn�m�ho podle symbolu - hrac� karty s �ol�kem - kterou zanech�v� na m�stech �inu. Vtip�lek s pro��zlou tv��� (Heath Ledger) m� tentokr�t v pl�nu Batmanovi dok�zat, �e slova Gotham a m�r nikdy nebudou st�t v jedn� v�t� vedle sebe. Na uvoln�n� m�sto po b�val�m kr�li m�stn� mafie used� Sal Maroni (Eric Roberts) a p�eb�r� vl�du nad v�t�inou gang� v Gothamu. Na sc�n� se rovn� objevuje nov� st�tn� n�vladn� Harvey Dent (Aaron Eckhart), kter� by rozdan�mi kartami (i s �ol�kem v bal��ku) mohl po��dn� zam�chat...",
                Duration = TimeSpan.FromMinutes(152),
                Genre = "Ak�n�, Drama, Krimi, Thriller",
                Country = "USA / Velk� Brit�nie",
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
