using System;
using System.Collections.Generic;
using System.Linq;

namespace Movies.BL.Models
{
    public class ActorMovieModel : ModelBase
    {
        public Guid ActorId { get; set; }
        public PersonListModel Actor { get; set; }

        public Guid MovieId { get; set; }
        public MovieListModel Movie { get; set; }

        private sealed class ActorMovieModelEqualityComparer : IEqualityComparer<ActorMovieModel>
        {
            public bool Equals(ActorMovieModel x, ActorMovieModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return
                    x.Id == y.Id
                    && PersonListModel.PersonListComparer.Equals(x.Actor, y.Actor)
                    && MovieListModel.MovieListComparer.Equals(x.Movie, y.Movie);
            }

            public int GetHashCode(ActorMovieModel obj)
            {
                var hashCode = new HashCode();
                hashCode.Add(obj.Actor);
                hashCode.Add(obj.Movie);
                return hashCode.ToHashCode();
            }
        }
        public static IEqualityComparer<ActorMovieModel> ActorMovieModelComparer { get; } = new ActorMovieModelEqualityComparer();
    }
}
