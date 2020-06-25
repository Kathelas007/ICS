using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Movies.BL.Models
{
    public class PersonDetailModel : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Photo { get; set; }

        public IEnumerable<DirectorMovieModel> DirectedMovies { get; set; } = new List<DirectorMovieModel>();
        public IEnumerable<ActorMovieModel> PlayedMovies { get; set; } = new List<ActorMovieModel>();

        private sealed class PersonDetailEqualityComparer : IEqualityComparer<PersonDetailModel>
        {
            public bool Equals(PersonDetailModel x, PersonDetailModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id
                       && x.FirstName == y.FirstName
                       && x.LastName == y.LastName
                       && x.Age == y.Age
                       && x.Photo == y.Photo
                       && x.DirectedMovies.OrderBy(i => i.Id).SequenceEqual(y.DirectedMovies.OrderBy(i => i.Id), DirectorMovieModel.DirectorMovieModelComparer)
                       && x.PlayedMovies.OrderBy(i => i.Id).SequenceEqual(y.PlayedMovies.OrderBy(i => i.Id), ActorMovieModel.ActorMovieModelComparer);
            }

            public int GetHashCode(PersonDetailModel obj)
            {
                return HashCode.Combine(obj.FirstName, obj.LastName, obj.Age, obj.Photo, obj.DirectedMovies, obj.PlayedMovies);
            }
        }

        public static IEqualityComparer<PersonDetailModel> PersonDetailComparer { get; } = new PersonDetailEqualityComparer();
    }
}
