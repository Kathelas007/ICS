using System;
using System.Collections.Generic;
using Movies.DAL.Interfaces;

namespace Movies.DAL.Entities
{

    public class PersonDirectsMovieEntity : IEntity
    {
        public Guid Id { get; set; }

        public Guid DirectorId { get; set; }
        public virtual PersonEntity Director { get; set; }

        public Guid MovieId { get; set; }
        public virtual MovieEntity Movie { get; set; }

        public static IEqualityComparer<PersonDirectsMovieEntity> PropertiesComparer { get; } = new PersonDirectsMovieEqualityComparer();
        private sealed class PersonDirectsMovieEqualityComparer : IEqualityComparer<PersonDirectsMovieEntity>
        {
            public bool Equals(PersonDirectsMovieEntity x, PersonDirectsMovieEntity y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id &&
                    x.Movie == y.Movie &&
                    x.Director == y.Director;
            }

            public int GetHashCode(PersonDirectsMovieEntity obj)
            {
                return HashCode.Combine(obj.Id, obj.Movie, obj.Director);
            }
        }
    }

}