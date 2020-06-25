using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

using Movies.DAL.Entities;
using Movies.DAL.Factories;
using Movies.DAL.Seeds;

namespace Movies.DAL.Tests
{
    public class MoviesDbContextTests: IDisposable
    {
        private readonly DbContextInMemoryFactory dbContextFactory;
        private readonly MoviesDbContext actContext;

        public MoviesDbContextTests()
        {
            dbContextFactory = new DbContextInMemoryFactory();
            actContext = dbContextFactory.Create();
            actContext.Database.EnsureCreated();
        }

        [Fact]
        public void AddNew_Movie_Persisted()
        {
            var movie = new MovieEntity()
            {
                CzechName = "Temn� ryt��",
                OriginalName = "The Dark Knight",
                Description =
                    "Ak�n� dobrodru�n� fantasy v re�ii Christophera Nolana navazuje na film Batman za��n� a Christian Bale si v n� zopakoval roli Batmana/Bruce Waynea v nekon��c�m boji se zlo�inem. Wayne v kost�mu netop���ho mu�e jde spolu s poru��kem Jamesem Gordonem (Gary Oldman) z Gothamsk� policie po kr�li zlo�inc� Jokerovi zn�m�ho podle symbolu - hrac� karty s �ol�kem - kterou zanech�v� na m�stech �inu. Vtip�lek s pro��zlou tv��� (Heath Ledger) m� tentokr�t v pl�nu Batmanovi dok�zat, �e slova Gotham a m�r nikdy nebudou st�t v jedn� v�t� vedle sebe. Na uvoln�n� m�sto po b�val�m kr�li m�stn� mafie used� Sal Maroni (Eric Roberts) a p�eb�r� vl�du nad v�t�inou gang� v Gothamu. Na sc�n� se rovn� objevuje nov� st�tn� n�vladn� Harvey Dent (Aaron Eckhart), kter� by rozdan�mi kartami (i s �ol�kem v bal��ku) mohl po��dn� zam�chat...",
                Duration = TimeSpan.FromMinutes(152),
                Genre = "Ak�n�, Drama, Krimi, Thriller",
                Photo = "photo.png"
            };

            actContext.Movies.Add(movie);
            actContext.SaveChanges();

            using var arrangeContext = dbContextFactory.Create();
            var result = arrangeContext.Movies.Single(i => i.Id == movie.Id);
           
            Assert.Equal(movie, result, MovieEntity.PropertiesComparer);
        }

        [Fact]
        public void AddNew_Person_Persisted()
        {
            var person = new PersonEntity()
            {
                FirstName = "Heath",
                LastName = "Ledger",
                Age = 28,
                Photo = "photo.png"
            };

            actContext.Persons.Add(person);
            actContext.SaveChanges();

            using var arrangeContext = dbContextFactory.Create();
            var result = arrangeContext.Persons.Single(i => i.Id == person.Id);

            Assert.Equal(person, result, PersonEntity.PropertiesComparer);
        }

        [Fact]
        public void AddNew_Rating_Persisted()
        {
            var rating = new RatingEntity()
            {
                Rating = 1000,
                Text = "Cool"
            };

            actContext.Ratings.Add(rating);
            actContext.SaveChanges();

            using var arrangeContext = dbContextFactory.Create();
            var result = arrangeContext.Ratings.Single(i => i.Id == rating.Id);

            Assert.Equal(rating, result, RatingEntity.PropertiesComparer);
        }


        [Fact]
        public void Remove_Movie_NotPersisted()
        {
            var victim = Seeds.Movies.LordOfTheRings;

            actContext.Movies.Remove(victim);
            actContext.SaveChanges();

            using var arrangeContext = dbContextFactory.Create();
            var result = arrangeContext.Movies.SingleOrDefault(i => i.Id == victim.Id);

            Assert.Null(result);
        }

        [Fact]
        public void Remove_Person_NotPersisted()
        {
            var victim = Seeds.Persons.Mortensen;

            actContext.Persons.Remove(victim);
            actContext.SaveChanges();

            using var arrangeContext = dbContextFactory.Create();
            var result = arrangeContext.Persons.SingleOrDefault(i => i.Id == victim.Id);

            Assert.Null(result);
        }

        [Fact]
        public void Remove_Rating_NotPersisted()
        {
            var victim = Seeds.Ratings.LOTRRating;

            actContext.Ratings.Remove(victim);
            actContext.SaveChanges();

            using var arrangeContext = dbContextFactory.Create();
            var result = arrangeContext.Ratings.SingleOrDefault(i => i.Id == victim.Id);

            Assert.Null(result);
        }

        [Fact]
        public void Edit_Movie_Persisted()
        {
            var victim = new MovieEntity()
            {
                Id = Seeds.Movies.LordOfTheRings.Id,
                CzechName = "P�n Prsten�"
            };

            actContext.Movies.Update(victim);
            actContext.SaveChanges();

            using var arrangeContext = dbContextFactory.Create();
            var result = arrangeContext.Movies.Single(i => i.Id == victim.Id);


            Assert.Equal(victim, result, MovieEntity.PropertiesComparer);

        }

        [Fact]
        public void Edit_Person_Persisted()
        {
            var victim = new PersonEntity()
            {
                Id = Seeds.Persons.Mortensen.Id,
                Age = 43
            };

            actContext.Persons.Update(victim);
            actContext.SaveChanges();

            using var arrangeContext = dbContextFactory.Create();
            var result = arrangeContext.Persons.Single(i => i.Id == victim.Id);


            Assert.Equal(victim, result, PersonEntity.PropertiesComparer);
        }

        [Fact]
        public void Edit_Rating_Persisted()
        {
            var victim = new RatingEntity()
            {
                Id = Seeds.Ratings.LOTRRating.Id,
                Rating = 100
            };

            actContext.Ratings.Update(victim);
            actContext.SaveChanges();

            using var arrangeContext = dbContextFactory.Create();
            var result = arrangeContext.Ratings.Single(i => i.Id == victim.Id);


            Assert.Equal(victim, result, RatingEntity.PropertiesComparer);

        }

        [Fact]
        public void SearchByProperty_Movie_Selected()
        {
            var validList = new List<MovieEntity>
            {
                Seeds.Movies.LordOfTheRings,
                Seeds.Movies.LordOfTheRingsDuplicate
            };


            using var arrangeContext = dbContextFactory.Create();
            var result = arrangeContext.Movies.Where(i => i.CzechName == Seeds.Movies.LordOfTheRings.CzechName).ToList();


            Assert.Equal(validList, result, MovieEntity.PropertiesComparer);

        }

        [Fact]
        public void SearchByProperty_Person_Selected()
        {
            var validList = new List<PersonEntity>
            {
                Seeds.Persons.Mortensen,
                Seeds.Persons.MortensenDuplicate
            };

            using var arrangeContext = dbContextFactory.Create();
            var result = arrangeContext.Persons.Where(i => i.FirstName == Seeds.Persons.Mortensen.FirstName).ToList();


            Assert.Equal(validList, result, PersonEntity.PropertiesComparer);

        }

        [Fact]
        public void SearchByProperty_Rating_Selected()
        {
            var validList = new List<RatingEntity>
            {
                Seeds.Ratings.LOTRRating,
                Seeds.Ratings.LOTRRatingDuplicate
            };


            using var arrangeContext = dbContextFactory.Create();
            var result = arrangeContext.Ratings.Where(i => i.Text == Seeds.Ratings.LOTRRating.Text).ToList();


            Assert.Equal(validList, result, RatingEntity.PropertiesComparer);

        }

        public void Dispose()
        {
            actContext.Database.EnsureDeleted();
            actContext.Dispose();
        }
    }
}
