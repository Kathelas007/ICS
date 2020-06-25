using System;
using System.Collections.Generic;
using Movies.DAL.Interfaces;

namespace Movies.DAL.Entities
{
    public class MovieEntity : IEntity
    {
        public Guid Id { get; set; }
        public string OriginalName { get; set; }
        public string CzechName { get; set; }
        public string Country { get; set; }
        public string Genre { get; set; }
        public string Photo { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RatingEntity> Ratings { get; set; } = new List<RatingEntity>();
        public virtual ICollection<PersonDirectsMovieEntity> Directors { get; set; } = new List<PersonDirectsMovieEntity>();
        public virtual ICollection<PersonPlaysMovieEntity> Actors { get; set; } = new List<PersonPlaysMovieEntity>();

        public static IEqualityComparer<MovieEntity> PropertiesComparer { get; } = new MovieEqualityComparer();
        private sealed class MovieEqualityComparer : IEqualityComparer<MovieEntity>
        {
            public bool Equals(MovieEntity x, MovieEntity y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id &&
                    x.OriginalName == y.OriginalName &&
                    x.CzechName == y.CzechName &&
                    x.Genre == y.Genre &&
                    x.Photo == y.Photo &&
                    x.Duration == y.Duration &&
                    x.Description == y.Description;
            }

            public int GetHashCode(MovieEntity obj)
            {
                return HashCode.Combine(obj.Id, obj.OriginalName, obj.CzechName, obj.Genre, obj.Photo, obj.Duration, obj.Description);
            }
        }
    }
}