using System;
using System.Collections.Generic;

using Movies.DAL.Interfaces;

namespace Movies.DAL.Entities
{

    public class RatingEntity : IEntity
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public Guid MovieId { get; set; }

        public virtual MovieEntity Movie { get; set; }

        public static IEqualityComparer<RatingEntity> PropertiesComparer { get; } = new RatingEqualityComparer();
        private sealed class RatingEqualityComparer : IEqualityComparer<RatingEntity>
        {
            public bool Equals(RatingEntity x, RatingEntity y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id.Equals(y.Id) && x.Rating == y.Rating && x.Text == y.Text;
            }

            public int GetHashCode(RatingEntity obj)
            {
                return HashCode.Combine(obj.Id, obj.Rating, obj.Text);
            }
        }
    }

}