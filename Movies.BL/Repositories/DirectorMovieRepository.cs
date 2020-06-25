using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Movies.DAL.Entities;
using Movies.DAL.Interfaces;
using Movies.BL.Models;
using Movies.BL.Mappers;
using Movies.DAL;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Movies.BL.Repositories
{
    public class DirectorMovieRepository : RepositoryBase<PersonDirectsMovieEntity, DirectorMovieModel, DirectorMovieModel>
    {
        public DirectorMovieRepository(IDbContextFactory dbContextFactory)
            : base(dbContextFactory,
                  DirectorMovieMapper.MapToEntity,
                  DirectorMovieMapper.MapToModel,
                  DirectorMovieMapper.MapToModel)
        { }

        internal override void IncludeList(ref IQueryable<PersonDirectsMovieEntity> query)
        {
            query = query.Include(e => e.Director).ThenInclude(d => d.DirectedMovies);
            query = query.Include(e => e.Movie).ThenInclude(m => m.Directors);
        }

        internal override IEnumerable<Func<PersonDirectsMovieEntity, IEnumerable<IEntity>>> CollectionsToBeSynchronized(PersonDirectsMovieEntity entity)
        {
            yield return null;
        }
    }
}
