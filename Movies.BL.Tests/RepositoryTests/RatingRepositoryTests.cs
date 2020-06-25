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
                    CzechName = "Temn� ryt��",
                    OriginalName = "The Dark Knight",
                    Description =
                        "Ak�n� dobrodru�n� fantasy v re�ii Christophera Nolana navazuje na film Batman za��n� a Christian Bale si v n� zopakoval roli Batmana/Bruce Waynea v nekon��c�m boji se zlo�inem. Wayne v kost�mu netop���ho mu�e jde spolu s poru��kem Jamesem Gordonem (Gary Oldman) z Gothamsk� policie po kr�li zlo�inc� Jokerovi zn�m�ho podle symbolu - hrac� karty s �ol�kem - kterou zanech�v� na m�stech �inu. Vtip�lek s pro��zlou tv��� (Heath Ledger) m� tentokr�t v pl�nu Batmanovi dok�zat, �e slova Gotham a m�r nikdy nebudou st�t v jedn� v�t� vedle sebe. Na uvoln�n� m�sto po b�val�m kr�li m�stn� mafie used� Sal Maroni (Eric Roberts) a p�eb�r� vl�du nad v�t�inou gang� v Gothamu. Na sc�n� se rovn� objevuje nov� st�tn� n�vladn� Harvey Dent (Aaron Eckhart), kter� by rozdan�mi kartami (i s �ol�kem v bal��ku) mohl po��dn� zam�chat...",
                    Country = "USA / Velk� Brit�nie"
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
                    CzechName = "Temn� ryt��",
                    OriginalName = "The Dark Knight",
                    Description =
                        "Ak�n� dobrodru�n� fantasy v re�ii Christophera Nolana navazuje na film Batman za��n� a Christian Bale si v n� zopakoval roli Batmana/Bruce Waynea v nekon��c�m boji se zlo�inem. Wayne v kost�mu netop���ho mu�e jde spolu s poru��kem Jamesem Gordonem (Gary Oldman) z Gothamsk� policie po kr�li zlo�inc� Jokerovi zn�m�ho podle symbolu - hrac� karty s �ol�kem - kterou zanech�v� na m�stech �inu. Vtip�lek s pro��zlou tv��� (Heath Ledger) m� tentokr�t v pl�nu Batmanovi dok�zat, �e slova Gotham a m�r nikdy nebudou st�t v jedn� v�t� vedle sebe. Na uvoln�n� m�sto po b�val�m kr�li m�stn� mafie used� Sal Maroni (Eric Roberts) a p�eb�r� vl�du nad v�t�inou gang� v Gothamu. Na sc�n� se rovn� objevuje nov� st�tn� n�vladn� Harvey Dent (Aaron Eckhart), kter� by rozdan�mi kartami (i s �ol�kem v bal��ku) mohl po��dn� zam�chat...",
                    Country = "USA / Velk� Brit�nie"
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