using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.BL.Models
{
    public class PersonListModel : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        private sealed class PersonListEqualityComparer : IEqualityComparer<PersonListModel>
        {
            public bool Equals(PersonListModel x, PersonListModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id == y.Id && x.FirstName == y.FirstName && x.LastName == y.LastName;
            }

            public int GetHashCode(PersonListModel obj)
            {
                return HashCode.Combine(obj.FirstName, obj.LastName);
            }
        }

        public static IEqualityComparer<PersonListModel> PersonListComparer { get; } = new PersonListEqualityComparer();
    }
}
