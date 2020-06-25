using System.Reflection.PortableExecutable;
using Movies.BL.Mappers;
using Movies.BL.Models;
using Movies.DAL.Entities;
using Xunit;

namespace Movies.BL.Tests.MapperTests
{
    public class RatingMapperTest : TestsBase
    {
        [Fact]
        public void DetailModel_To_ListModel()
        {
            var rating = DAL.Seeds.Ratings.LOTRRating;
            var lotr = DAL.Seeds.Movies.LordOfTheRings;

            var detailModel = new RatingDetailModel()
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

            var listModel = new RatingListModel()
            {
                Id = rating.Id,
                Rating = rating.Rating,
                Text = rating.Text
            };

            var returnedModel = RatingMapper.MapToList(detailModel);

            Assert.Equal(listModel, returnedModel, RatingListModel.RatingListComparer);
        }

        [Fact]
        public void Entity_To_ListModel()
        {
            var rating = DAL.Seeds.Ratings.LOTRRating;

            var model = new RatingListModel()
            {
                Id = rating.Id,
                Rating = rating.Rating,
                Text = rating.Text
            };

            var returnedModel = RatingMapper.MapToList(rating);

            Assert.Equal(model, returnedModel, RatingListModel.RatingListComparer);
        }

        [Fact]
        public void Entity_To_DetailModel()
        {
            var rating = DAL.Seeds.Ratings.LOTRRatingDuplicate;

            var model = new RatingDetailModel()
            {
                Id = rating.Id,
                Rating = rating.Rating,
                Text = rating.Text
            };

            var returnedModel = RatingMapper.MapToDetail(rating);

            Assert.Equal(model, returnedModel, RatingDetailModel.RatingDetailComparer);
        }

        [Fact]
        public void DetailModel_To_Entity()
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

            var returnedEntity = RatingMapper.MapToEntity(model);

            Assert.Equal(rating, returnedEntity, RatingEntity.PropertiesComparer);
        }
    }
}