using System;
using Movies.BL.Interfaces;
using Movies.BL.Models;
using Movies.BL.Repositories;
using Movies.DAL.Entities;
using Xunit;

namespace Movies.BL.Tests.RepositoryTests
{
    public class RatingRepositoryTests : TestsBase
    {
        private readonly IRepository<RatingEntity, RatingListModel, RatingDetailModel> repository;

        public RatingRepositoryTests() : base()
        {
            repository = new RatingRepository(dbContextFactory);
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
            var rating = DAL.Seeds.Ratings.LOTRRating;
            var lotr = DAL.Seeds.Movies.LordOfTheRings;

            var model = new RatingDetailModel()
            {
                Id = rating.Id,
                Rating = rating.Rating,
                Text = rating.Text,
                Movie = new MovieListModel()
                {
                    Id = lotr.Id,
                    CzechName = lotr.CzechName,
                    OriginalName = lotr.OriginalName,
                    Description = lotr.Description,
                    Country = lotr.Country
                }
            };

            var returnedModel = repository.GetById(rating.Id);

            Assert.Equal(model, returnedModel, RatingDetailModel.RatingDetailComparer);
        }

        [Fact]
        public void DeleteById_Model_NotPersisted()
        {
            var rating = DAL.Seeds.Ratings.LOTRRatingDuplicate;

            repository.Delete(rating.Id);

            var returnedModel = repository.GetById(rating.Id);

            Assert.Null(returnedModel);

        }

        [Fact]
        public void DeleteByModel_Model_NotPersisted()
        {
            var rating = DAL.Seeds.Ratings.HPRating;

            var model = new RatingDetailModel()
            {
                Id = rating.Id,
                Rating = rating.Rating,
                Text = rating.Text
            };

            repository.Delete(model);

            var returnedModel = repository.GetById(rating.Id);

            Assert.Null(returnedModel);

        }

        [Fact]
        public void Insert_Model_DidntThrow()
        {
            var model = new RatingDetailModel()
            {
                Rating = 1000,
                Text = "Cool",
                Movie = new MovieListModel()
                {
                    CzechName = "Temný rytíø",
                    OriginalName = "The Dark Knight",
                    Description =
                        "Akèní dobrodružná fantasy v režii Christophera Nolana navazuje na film Batman zaèíná a Christian Bale si v ní zopakoval roli Batmana/Bruce Waynea v nekonèícím boji se zloèinem. Wayne v kostýmu netopýøího muže jde spolu s poruèíkem Jamesem Gordonem (Gary Oldman) z Gothamské policie po králi zloèincù Jokerovi známého podle symbolu - hrací karty s žolíkem - kterou zanechává na místech èinu. Vtipálek s proøízlou tváøí (Heath Ledger) má tentokrát v plánu Batmanovi dokázat, že slova Gotham a mír nikdy nebudou stát v jedné vìtì vedle sebe. Na uvolnìné místo po bývalém králi místní mafie usedá Sal Maroni (Eric Roberts) a pøebírá vládu nad vìtšinou gangù v Gothamu. Na scénì se rovnìž objevuje nový státní návladní Harvey Dent (Aaron Eckhart), který by rozdanými kartami (i s žolíkem v balíèku) mohl poøádnì zamíchat...",
                    Country = "USA / Velká Británie"
                }
            };

            var returnedModel = repository.InsertOrUpdate(model);

            Assert.NotNull(returnedModel);

            repository.Delete(returnedModel.Id);
        }

        [Fact]
        public void Insert_Model_Persisted()
        {
            
            var model = new RatingDetailModel()
            {
                Rating = 1000,
                Text = "Cool",
                Movie = new MovieListModel()
                {
                    CzechName = "Temný rytíø",
                    OriginalName = "The Dark Knight",
                    Description =
                        "Akèní dobrodružná fantasy v režii Christophera Nolana navazuje na film Batman zaèíná a Christian Bale si v ní zopakoval roli Batmana/Bruce Waynea v nekonèícím boji se zloèinem. Wayne v kostýmu netopýøího muže jde spolu s poruèíkem Jamesem Gordonem (Gary Oldman) z Gothamské policie po králi zloèincù Jokerovi známého podle symbolu - hrací karty s žolíkem - kterou zanechává na místech èinu. Vtipálek s proøízlou tváøí (Heath Ledger) má tentokrát v plánu Batmanovi dokázat, že slova Gotham a mír nikdy nebudou stát v jedné vìtì vedle sebe. Na uvolnìné místo po bývalém králi místní mafie usedá Sal Maroni (Eric Roberts) a pøebírá vládu nad vìtšinou gangù v Gothamu. Na scénì se rovnìž objevuje nový státní návladní Harvey Dent (Aaron Eckhart), který by rozdanými kartami (i s žolíkem v balíèku) mohl poøádnì zamíchat...",
                    Country = "USA / Velká Británie"
                }
            };

            var returnedModel = repository.InsertOrUpdate(model);
            var insertedModel = repository.GetById(returnedModel.Id);

            Assert.NotEqual(Guid.Empty, insertedModel.Id);
            model.Id = insertedModel.Id;

            Assert.NotEqual(Guid.Empty, insertedModel.Movie.Id);
            model.Movie.Id = insertedModel.Movie.Id;

            Assert.Equal(model, insertedModel, RatingDetailModel.RatingDetailComparer);
        }

        [Fact]
        public void Update_Model_DidntThrow()
        {
            var rating = DAL.Seeds.Ratings.LOTRRating;
            var lotr = DAL.Seeds.Movies.LordOfTheRings;

            var model = new RatingDetailModel()
            {
                Id = rating.Id,
                Rating = 91720,
                Text = rating.Text,
                Movie = new MovieListModel()
                {
                    Id = lotr.Id,
                    CzechName = lotr.CzechName,
                    OriginalName = lotr.OriginalName,
                    Description = lotr.Description,
                    Country = lotr.Country
                }
            };

            var returnedModel = repository.InsertOrUpdate(model);

            Assert.NotNull(returnedModel);
        }

        [Fact]
        public void Update_Model_Edited()
        {
            var rating = DAL.Seeds.Ratings.LOTRRating;
            var lotr = DAL.Seeds.Movies.LordOfTheRings;

            var model = new RatingDetailModel()
            {
                Id = rating.Id,
                Rating = rating.Rating,//91720,
                Text = rating.Text,
                Movie = new MovieListModel()
                {
                    Id = lotr.Id,
                    CzechName = lotr.CzechName,
                    OriginalName = lotr.OriginalName,
                    Description = lotr.Description,
                    Country = lotr.Country
                }
            };

            var returnedModel = repository.InsertOrUpdate(model);
            var editedModel = repository.GetById(returnedModel.Id);

            Assert.NotEqual(Guid.Empty, editedModel.Id);
            Assert.NotEqual(Guid.Empty, editedModel.Movie.Id);

            Assert.Equal(model, editedModel, RatingDetailModel.RatingDetailComparer);
        }
    }
}