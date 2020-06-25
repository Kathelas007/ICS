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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace Movies.BL.Repositories
{
    public class MovieRepository : RepositoryBase<MovieEntity, MovieListModel, MovieDetailModel>
    {
        public MovieRepository(IDbContextFactory dbContextFactory)
            : base(dbContextFactory,
                  MovieMapper.MapToEntity,
                  MovieMapper.MapToList,
                  MovieMapper.MapToDetail)
        { }

        internal override void IncludeList(ref IQueryable<MovieEntity> query)
        {
            query = query.Include(movie => movie.Ratings);
            query = query.Include(movie => movie.Directors).ThenInclude(dm => dm.Director);
            query = query.Include(movie => movie.Actors).ThenInclude(am => am.Actor);
        }

        internal override IEnumerable<Func<MovieEntity, IEnumerable<IEntity>>> CollectionsToBeSynchronized(MovieEntity entity)
        {
            Func<MovieEntity, IEnumerable<PersonDirectsMovieEntity>> director_fnc =
                 e =>  e.Directors;

            Func<MovieEntity, IEnumerable<PersonPlaysMovieEntity>> actor_fnc =
                e => e.Actors;

            yield return director_fnc;
            yield return actor_fnc;
        }

        public override void Delete(Guid entityId)
        {
            if (entityId == null) { return; }

            using (var dbContext = DbContextFactory.Create())
            {
                var entityType = typeof(MovieEntity);
                MovieEntity entity = (MovieEntity) dbContext.Find(entityType, entityId);
                foreach (var rating in entity.Ratings)
                {
                    dbContext.Remove(rating);
                }
                dbContext.Remove(entity);
                

                dbContext.SaveChanges();
            }
        }
    }
}
