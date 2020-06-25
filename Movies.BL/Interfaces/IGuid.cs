using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.BL.Interfaces
{

    // needed by IRepository and others, Guid is only structure
    public interface IGuid
    {
        Guid Id { get; set; }
    }
}
