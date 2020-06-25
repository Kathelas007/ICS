using System.Linq;
using System.Collections.Generic;

using Movies.DAL;

using Movies.BL.Models;
using Movies.DAL.Entities;

namespace Movies.BL.Mappers
{
    public static class PersonMapper
    {
        public static PersonListModel MapToList(PersonEntity person)
        {
            if (person == null) return null;

            return new PersonListModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName
            };
        }

        public static PersonListModel MapToList(PersonDetailModel person)
        {
            if (person == null) return null;

            return new PersonListModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName
            };
        }

        public static PersonDetailModel MapToDetail(PersonEntity person)
        {
            if (person == null) return null;

            return new PersonDetailModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age,
                Photo = person.Photo,
                DirectedMovies = person.DirectedMovies.Select(sl_m => DirectorMovieMapper.MapToModel(sl_m)).ToList(),
                PlayedMovies = person.PlayedMovies.Select(sl_m => ActorMovieMapper.MapToModel(sl_m)).ToList()
            };
        }
        public static PersonEntity MapToEntity(PersonDetailModel person)
        {
            if (person == null) return null;

            return new PersonEntity
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age,
                Photo = person.Photo,
                DirectedMovies = person.DirectedMovies.Select(sl_m => DirectorMovieMapper.MapToEntity(sl_m)).ToList(),
                PlayedMovies = person.PlayedMovies.Select(sl_m => ActorMovieMapper.MapToEntity(sl_m)).ToList()
            };
        }

        public static PersonEntity MapToEntity(PersonListModel person)
        {
            if (person == null) return null;

            return new PersonEntity
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
            };
        }
    }
}
