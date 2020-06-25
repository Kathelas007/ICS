using System;
using System.Collections.Generic;
using Movies.DAL.Interfaces;

namespace Movies.DAL.Entities
{

    public class PersonPlaysMovieEntity : IEntity
    {
        public Guid Id { get; set; }


        public Guid ActorId { get; set; }
        public virtual PersonEntity Actor { get; set; }

        public Guid MovieId { get; set; }
        public virtual MovieEntity Movie { get; set; }

        public static IEqualityComparer<PersonPlaysMovieEntity> PropertiesComparer { get; } = new PersonPlaysMovieEqualityComparer();
        private sealed class PersonPlaysMovieEqualityComparer : IEqualityComparer<PersonPlaysMovieEntity>
        {
            public bool Equals(PersonPlaysMovieEntity x, PersonPlaysMovieEntity y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id &&
                    x.Movie == y.Movie &&
                    x.Actor == y.Actor;
            }

            public int GetHashCode(PersonPlaysMovieEntity obj)
            {
                return HashCode.Combine(obj.Id, obj.Movie, obj.Actor);
            }
        }
    }

}