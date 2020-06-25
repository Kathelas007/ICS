using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.BL.Models
{
    public class MovieListModel: ModelBase
    {
        public string OriginalName { get; set; }
        public string CzechName { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }

        private sealed class MovieListEqualityComparer : IEqualityComparer<MovieListModel>
        {
            public bool Equals(MovieListModel x, MovieListModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id && x.OriginalName == y.OriginalName && x.CzechName == y.CzechName && x.Country == y.Country && x.Description == y.Description;
            }

            public int GetHashCode(MovieListModel obj)
            {
                return HashCode.Combine(obj.OriginalName, obj.CzechName, obj.Country, obj.Description);
            }
        }

        public static IEqualityComparer<MovieListModel> MovieListComparer { get; } = new MovieListEqualityComparer();
    }
}
