using Movies.BL.Mappers;
using Movies.BL.Models;
using Movies.DAL.Entities;
using Xunit;

namespace Movies.BL.Tests.MapperTests
{
    public class MovieMapperTests : TestsBase
    {
        [Fact]
        public void Entity_To_ListModel()
        {
            var lotr = DAL.Seeds.Movies.LordOfTheRings;

            var model = new MovieListModel()
            {
                Id = lotr.Id,
                CzechName = lotr.CzechName,
                OriginalName = lotr.OriginalName,
                Description = lotr.Description,
                Country = lotr.Country
            };

            var returnedModel = MovieMapper.MapToList(lotr);

            Assert.Equal(model, returnedModel, MovieListModel.MovieListComparer);
        }

        [Fact]
        public void DetailModel_To_ListModel()
        {
            var lotr = DAL.Seeds.Movies.LordOfTheRings;
            var viggo = DAL.Seeds.Persons.Mortensen;
            var viggo2 = DAL.Seeds.Persons.MortensenDuplicate;
            var rating = DAL.Seeds.Ratings.LOTRRating;

            var detailModel = new MovieDetailModel()
            {
                Id = lotr.Id,
                CzechName = lotr.CzechName,
                OriginalName = lotr.OriginalName,
                Description = lotr.Description,
                Duration = lotr.Duration,
                Genre = lotr.Genre,
                Country = lotr.Country,
                Photo = lotr.Photo,
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

            var listModel = new MovieListModel()
            {
                Id = lotr.Id,
                CzechName = lotr.CzechName,
                OriginalName = lotr.OriginalName,
                Description = lotr.Description,
                Country = lotr.Country,
            };

            var returnedModel = MovieMapper.MapToList(detailModel);

            Assert.Equal(listModel, returnedModel, MovieListModel.MovieListComparer);
        }


        [Fact]
        public void Entity_To_DetailModel()
        {
            var movie = DAL.Seeds.Movies.NoNameFilm;

            var model = new MovieDetailModel()
            {
                Id = movie.Id,
                CzechName = movie.CzechName,
                OriginalName = movie.OriginalName,
                Description = movie.Description,
                Duration = movie.Duration,
                Genre = movie.Genre,
                Country = movie.Country,
                Photo = movie.Photo
            };

            var returnedModel = MovieMapper.MapToDetail(movie);

            Assert.Equal(model, returnedModel, MovieDetailModel.MovieDetailComparer);
        }

        [Fact]
        public void DetailModel_To_Entity()
        {
            var movie = DAL.Seeds.Movies.NoNameFilm;

            var model = new MovieDetailModel()
            {
                Id = movie.Id,
                CzechName = movie.CzechName,
                OriginalName = movie.OriginalName,
                Description = movie.Description,
                Duration = movie.Duration,
                Genre = movie.Genre,
                Country = movie.Country,
                Photo = movie.Photo
            };

            var returnedEntity = MovieMapper.MapToEntity(model);

            Assert.Equal(movie, returnedEntity, MovieEntity.PropertiesComparer);
        }

        [Fact]
        public void ListModel_To_Entity()
        {
            var lotr = DAL.Seeds.Movies.LordOfTheRings;

            var entity = new MovieEntity()
            {
                Id = lotr.Id,
                CzechName = lotr.CzechName,
                OriginalName = lotr.OriginalName,
                Description = lotr.Description,
                Country = lotr.Country
            };

            var model = new MovieListModel()
            {
                Id = lotr.Id,
                CzechName = lotr.CzechName,
                OriginalName = lotr.OriginalName,
                Description = lotr.Description,
                Country = lotr.Country
            };

            var returnedEntity = MovieMapper.MapToEntity(model);

            Assert.Equal(entity, returnedEntity, MovieEntity.PropertiesComparer);
        }
    }
}