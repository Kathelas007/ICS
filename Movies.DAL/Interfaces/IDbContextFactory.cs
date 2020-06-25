
using Microsoft.EntityFrameworkCore.Design;

namespace Movies.DAL.Interfaces
{
    public interface IDbContextFactory
    {
        MoviesDbContext Create();
    }
}
