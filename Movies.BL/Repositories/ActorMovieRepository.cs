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
    public class ActorMovieRepository : RepositoryBase<PersonPlaysMovieEntity, ActorMovieModel, ActorMovieModel>
    {
        public ActorMovieRepository(IDbContextFactory dbContextFactory)
            : base(dbContextFactory,
                  ActorMovieMapper.MapToEntity,
                  ActorMovieMapper.MapToModel,
                  ActorMovieMapper.MapToModel)
        { }

        internal override void IncludeList(ref IQueryable<PersonPlaysMovieEntity> query)
        {
            // two way
            query = query.Include(e => e.Actor).ThenInclude(a => a.PlayedMovies);
            query = query.Include(e => e.Movie).ThenInclude(m => m.Directors);
        }

        internal override IEnumerable<Func<PersonPlaysMovieEntity, IEnumerable<IEntity>>> CollectionsToBeSynchronized(PersonPlaysMovieEntity entity)
        {
            yield return null;
        }
    }
}
