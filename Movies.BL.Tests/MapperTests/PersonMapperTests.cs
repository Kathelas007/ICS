using Movies.BL.Mappers;
using Movies.BL.Models;
using Movies.DAL.Entities;
using Xunit;

namespace Movies.BL.Tests.MapperTests
{
    public class PersonMapperTests : TestsBase
    {
        [Fact]
        public void Entity_To_ListModel()
        {
            var viggo = DAL.Seeds.Persons.Mortensen;

            var model = new PersonListModel()
            {
                Id = viggo.Id,
                FirstName = viggo.FirstName,
                LastName = viggo.LastName
            };

            var returnedModel = PersonMapper.MapToList(viggo);

            Assert.Equal(model, returnedModel, PersonListModel.PersonListComparer);
        }

        [Fact]
        public void DetailModel_To_ListModel()
        {
            var viggo = DAL.Seeds.Persons.Mortensen;
            var lotr = DAL.Seeds.Movies.LordOfTheRings;
            var lotr2 = DAL.Seeds.Movies.LordOfTheRingsDuplicate;

            var detailModel = new PersonDetailModel()
            {
                Id = viggo.Id,
                FirstName = viggo.FirstName,
                LastName = viggo.LastName,
                Age = viggo.Age,
                Photo = viggo.Photo
            };

            var listModel = new PersonListModel()
            {
                Id = viggo.Id,
                FirstName = viggo.FirstName,
                LastName = viggo.LastName
            };

            var returnedModel = PersonMapper.MapToList(detailModel);

            Assert.Equal(listModel, returnedModel, PersonListModel.PersonListComparer);
        }

        [Fact]
        public void Entity_To_DetailModel()
        {
            var noNameActor = DAL.Seeds.Persons.NoNameActor;

            var model = new PersonDetailModel()
            {
                Id = noNameActor.Id,
                FirstName = noNameActor.FirstName,
                LastName = noNameActor.LastName,
                Age = noNameActor.Age,
                Photo = noNameActor.Photo
            };

            var returnedModel = PersonMapper.MapToDetail(noNameActor);

            Assert.Equal(model, returnedModel, PersonDetailModel.PersonDetailComparer);
        }

        [Fact]
        public void DetailModel_To_Entity()
        {
            var viggo = DAL.Seeds.Persons.Mortensen;
            var lotr = DAL.Seeds.Movies.LordOfTheRings;
            var lotr2 = DAL.Seeds.Movies.LordOfTheRingsDuplicate;

            var model = new PersonDetailModel()
            {
                Id = viggo.Id,
                FirstName = viggo.FirstName,
                LastName = viggo.LastName,
                Age = viggo.Age,
                Photo = viggo.Photo
            };

            var returnedEntity = PersonMapper.MapToEntity(model);

            Assert.Equal(viggo, returnedEntity, PersonEntity.PropertiesComparer);
        }

        [Fact]
        public void ListModel_To_Entity()
        {
            var viggo = DAL.Seeds.Persons.Mortensen;

            var entity = new PersonEntity()
            {
                Id = viggo.Id,
                FirstName = viggo.FirstName,
                LastName = viggo.LastName
            };

            var model = new PersonListModel()
            {
                Id = viggo.Id,
                FirstName = viggo.FirstName,
                LastName = viggo.LastName
            };

            var returnedEntity = PersonMapper.MapToEntity(model);

            Assert.Equal(entity, returnedEntity, PersonEntity.PropertiesComparer);
        }
    }
}