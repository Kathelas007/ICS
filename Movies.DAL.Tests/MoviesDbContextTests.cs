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
                CzechName = "Temný rytíø",
                OriginalName = "The Dark Knight",
                Description =
                    "Akèní dobrodružná fantasy v režii Christophera Nolana navazuje na film Batman zaèíná a Christian Bale si v ní zopakoval roli Batmana/Bruce Waynea v nekonèícím boji se zloèinem. Wayne v kostýmu netopýøího muže jde spolu s poruèíkem Jamesem Gordonem (Gary Oldman) z Gothamské policie po králi zloèincù Jokerovi známého podle symbolu - hrací karty s žolíkem - kterou zanechává na místech èinu. Vtipálek s proøízlou tváøí (Heath Ledger) má tentokrát v plánu Batmanovi dokázat, že slova Gotham a mír nikdy nebudou stát v jedné vìtì vedle sebe. Na uvolnìné místo po bývalém králi místní mafie usedá Sal Maroni (Eric Roberts) a pøebírá vládu nad vìtšinou gangù v Gothamu. Na scénì se rovnìž objevuje nový státní návladní Harvey Dent (Aaron Eckhart), který by rozdanými kartami (i s žolíkem v balíèku) mohl poøádnì zamíchat...",
                Duration = TimeSpan.FromMinutes(152),
                Genre = "Akèní, Drama, Krimi, Thriller",
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
                CzechName = "Pán Prstenù"
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
