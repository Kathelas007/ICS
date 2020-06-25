using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.BL.Models
{
    public class RatingDetailModel : ModelBase
    {
        public int Rating { get; set; }
        public string Text { get; set; }
        public MovieListModel Movie{ get; set; }

        private sealed class RatingDetailEqualityComparer : IEqualityComparer<RatingDetailModel>
        {
            public bool Equals(RatingDetailModel x, RatingDetailModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id && x.Rating == y.Rating && x.Text == y.Text && MovieListModel.MovieListComparer.Equals(x.Movie, y.Movie);
            }

            public int GetHashCode(RatingDetailModel obj)
            {
                return HashCode.Combine(obj.Rating, obj.Text, obj.Movie);
            }
        }

        public static IEqualityComparer<RatingDetailModel> RatingDetailComparer { get; } = new RatingDetailEqualityComparer();
    }
}
