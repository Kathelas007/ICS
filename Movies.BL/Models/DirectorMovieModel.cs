using System;
using System.Collections.Generic;
using System.Linq;

namespace Movies.BL.Models
{
    public class DirectorMovieModel : ModelBase
    {
        public Guid DirectorId { get; set; }
        public PersonListModel Director { get; set; }

        public Guid MovieId { get; set; }
        public MovieListModel Movie { get; set; }

        private sealed class DirectorMovieModelEqualityComparer : IEqualityComparer<DirectorMovieModel>
        {
            public bool Equals(DirectorMovieModel x, DirectorMovieModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return
                    x.Id == y.Id
                    && PersonListModel.PersonListComparer.Equals(x.Director, y.Director)
                    && MovieListModel.MovieListComparer.Equals(x.Movie, y.Movie);
            }

            public int GetHashCode(DirectorMovieModel obj)
            {
                var hashCode = new HashCode();
                hashCode.Add(obj.Director);
                hashCode.Add(obj.Movie);
                return hashCode.ToHashCode();
            }
        }
        public static IEqualityComparer<DirectorMovieModel> DirectorMovieModelComparer { get; } = new DirectorMovieModelEqualityComparer();
    }
}


