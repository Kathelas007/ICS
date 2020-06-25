using System;
using System.Collections.Generic;

using Movies.DAL.Interfaces;

namespace Movies.DAL.Entities
{
    public class PersonEntity : IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Photo { get; set; }

        public virtual ICollection<PersonDirectsMovieEntity> DirectedMovies { get; set; } = new List<PersonDirectsMovieEntity>();
        public virtual ICollection<PersonPlaysMovieEntity> PlayedMovies { get; set; } = new List<PersonPlaysMovieEntity>();

        public static IEqualityComparer<PersonEntity> PropertiesComparer { get; } = new PersonEqualityComparer();
        private sealed class PersonEqualityComparer : IEqualityComparer<PersonEntity>
        {
            public bool Equals(PersonEntity x, PersonEntity y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id.Equals(y.Id) && x.FirstName == y.FirstName && x.LastName == y.LastName && x.Age == y.Age && x.Photo == y.Photo;
            }

            public int GetHashCode(PersonEntity obj)
            {
                return HashCode.Combine(obj.Id, obj.FirstName, obj.LastName, obj.Age, obj.Photo);
            }
        }
    }
}