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
    public class RatingRepository : RepositoryBase<RatingEntity, RatingListModel, RatingDetailModel>
    {
        public RatingRepository(IDbContextFactory dbContextFactory)
            : base(dbContextFactory,
                  RatingMapper.MapToEntity,
                  RatingMapper.MapToList,
                  RatingMapper.MapToDetail)
        {
        }

        internal override IEnumerable<Func<RatingEntity, IEnumerable<IEntity>>> CollectionsToBeSynchronized(RatingEntity entity) {

            Func<RatingEntity, IEnumerable<MovieEntity>> movie_fnc =
                e => new List<MovieEntity> { e.Movie };

            yield return movie_fnc;
        }


        internal override void IncludeList(ref IQueryable<RatingEntity> query)
        {
            query = query.Include(rating => rating.Movie);
        }
    }
}
