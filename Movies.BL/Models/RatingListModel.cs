using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.BL.Models
{
    public class RatingListModel : ModelBase
    {
        public int Rating { get; set; }
        public string Text { get; set; }

        private sealed class RatingListEqualityComparer : IEqualityComparer<RatingListModel>
        {
            public bool Equals(RatingListModel x, RatingListModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id && x.Rating == y.Rating && x.Text == y.Text;
            }

            public int GetHashCode(RatingListModel obj)
            {
                return HashCode.Combine(obj.Rating, obj.Text);
            }
        }

        public static IEqualityComparer<RatingListModel> RatingListComparer { get; } = new RatingListEqualityComparer();
    }
}
