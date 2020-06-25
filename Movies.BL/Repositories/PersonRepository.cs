using System;
using System.Collections.Generic;
using System.Text;

using Movies.DAL.Entities;
using Movies.DAL.Interfaces;
using Movies.BL.Models;
using Movies.BL.Mappers;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;

namespace Movies.BL.Repositories
{
    public class PersonRepository : RepositoryBase<PersonEntity, PersonListModel, PersonDetailModel>
    {
        public PersonRepository(IDbContextFactory dbContextFactory)
            : base(dbContextFactory,
                  PersonMapper.MapToEntity,
                  PersonMapper.MapToList,
                  PersonMapper.MapToDetail)
        {
        }

        internal override void IncludeList(ref IQueryable<PersonEntity> query)
        {
            query = query.Include(e => e.DirectedMovies).ThenInclude(dm => dm.Movie);
            query = query.Include(e => e.PlayedMovies).ThenInclude(pm => pm.Movie);
        }

        internal override IEnumerable<Func<PersonEntity, IEnumerable<IEntity>>> CollectionsToBeSynchronized(PersonEntity entity)
        {
            Func<PersonEntity, IEnumerable<PersonDirectsMovieEntity>> directed_fnc =
                 e => e.DirectedMovies;

            Func<PersonEntity, IEnumerable<PersonPlaysMovieEntity>> played_fnc =
                e => e.PlayedMovies;

            yield return directed_fnc;
            yield return played_fnc;
        }
    }
}
