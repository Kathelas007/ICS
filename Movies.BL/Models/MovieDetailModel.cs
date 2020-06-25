using System;
using System.Collections.Generic;
using System.Linq;

namespace Movies.BL.Models
{
    public class MovieDetailModel : ModelBase
    {
        public string OriginalName { get; set; }
        public string CzechName { get; set;}
        public string Country { get; set; }
        public string Genre { get; set; }
        public string Photo { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }

        public IEnumerable<DirectorMovieModel> Directors { get; set; } = new List<DirectorMovieModel>();
        public IEnumerable<ActorMovieModel> Actors { get; set; } = new List<ActorMovieModel>();
        public IEnumerable<RatingListModel> Ratings { get; set; } = new List<RatingListModel>();

        private sealed class MovieDetailEqualityComparer : IEqualityComparer<MovieDetailModel>
        {
            public bool Equals(MovieDetailModel x, MovieDetailModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return
                    x.Id == y.Id
                    && x.OriginalName == y.OriginalName
                    && x.CzechName == y.CzechName
                    && x.Genre == y.Genre
                    && x.Photo == y.Photo
                    && x.Duration.Equals(y.Duration)
                    && x.Description == y.Description
                    && x.Directors.OrderBy(i => i.Id).SequenceEqual(y.Directors.OrderBy(i => i.Id), DirectorMovieModel.DirectorMovieModelComparer)
                    && x.Actors.OrderBy(i => i.Id).SequenceEqual(y.Actors.OrderBy(i => i.Id), ActorMovieModel.ActorMovieModelComparer)
                    && x.Ratings.OrderBy(i => i.Id).SequenceEqual(y.Ratings.OrderBy(i => i.Id), RatingListModel.RatingListComparer);
            }

            public int GetHashCode(MovieDetailModel obj)
            {
                var hashCode = new HashCode();
                hashCode.Add(obj.OriginalName);
                hashCode.Add(obj.CzechName);
                hashCode.Add(obj.Genre);
                hashCode.Add(obj.Photo);
                hashCode.Add(obj.Duration);
                hashCode.Add(obj.Description);
                hashCode.Add(obj.Directors);
                hashCode.Add(obj.Actors);
                hashCode.Add(obj.Ratings);
                return hashCode.ToHashCode();
            }
        }

        public static IEqualityComparer<MovieDetailModel> MovieDetailComparer { get; } = new MovieDetailEqualityComparer();
    }
}
