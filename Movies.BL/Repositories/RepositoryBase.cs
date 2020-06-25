using System;
using System.Collections.Generic;
using System.Linq;

using Movies.BL.Interfaces;
using Movies.DAL.Interfaces;
using Movies.DAL;

using Microsoft.EntityFrameworkCore.Query;
using Movies.DAL.Repositories;

namespace Movies.BL.Repositories
{
    public abstract class RepositoryBase<TEntity, TListModel, TDetailModel> : IRepository<TEntity, TListModel, TDetailModel>
        where TEntity : class, IEntity, new()
        where TDetailModel : IGuid, new()
        where TListModel : IGuid, new()
    {
        protected readonly IDbContextFactory DbContextFactory;
        protected readonly Func<TDetailModel, TEntity> MapToEntity;
        protected readonly Func<TEntity, TListModel> MapToList;
        protected readonly Func<TEntity, TDetailModel> MapToDetail;


        public RepositoryBase(
            IDbContextFactory dbContextFactory,
            Func<TDetailModel, TEntity> mapToEntity,
            Func<TEntity, TListModel> mapToList,
            Func<TEntity, TDetailModel> mapToDetail
            )
        {
            DbContextFactory = dbContextFactory;
            MapToEntity = mapToEntity;
            MapToList = mapToList;
            MapToDetail = mapToDetail;
        }

        internal abstract void IncludeList(ref IQueryable<TEntity> query);

        internal abstract IEnumerable<Func<TEntity, IEnumerable<IEntity>>> CollectionsToBeSynchronized(TEntity entity);

        public void Delete(TDetailModel model)
        {
            Delete(model.Id);
        }

        public virtual void Delete(Guid entityId)
        {
            if (entityId == null) { return; }

            using (var dbContext = DbContextFactory.Create())
            {
                var entityType = typeof(TEntity);
                dbContext.Remove(dbContext.Find(entityType, entityId));
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<TListModel> GetAll()
        {
            using (var dbContext = DbContextFactory.Create())
            {
                IQueryable<TEntity> query = dbContext.Set<TEntity>();
                IncludeList(ref query);

                return query.AsEnumerable().Select(entity => MapToList(entity)).ToArray();
            }
        }

        public TDetailModel GetById(Guid entityId)
        {
            if (entityId == null) { return default(TDetailModel); }

            using (var dbContext = DbContextFactory.Create())
            {
                IQueryable<TEntity> query = dbContext.Set<TEntity>();
                IncludeList(ref query);
                            
                var entity = query.FirstOrDefault(e => e.Id == entityId);
                return MapToDetail(entity);
            }
        }

        public TDetailModel InsertOrUpdate(TDetailModel model)
        {

            using (var dbContext = DbContextFactory.Create())
            {
                var entity = MapToEntity(model);
                dbContext.Update<TEntity>(entity);


                SynchronizeCollections(dbContext, entity);

                dbContext.SaveChanges();

                return MapToDetail(entity);
            }
        }

        private void SynchronizeCollections(MoviesDbContext dbContext, TEntity entity)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();
            TEntity entityInDb;
            using (var dbContextGetById = DbContextFactory.Create())
            {
                var modelInDb = GetById(entity.Id);
                entityInDb = MapToEntity(modelInDb);

                if (entityInDb == null)
                {
                    return;
                }
            }

            foreach (var collectionSelector in CollectionsToBeSynchronized(entity))
            {
                if (collectionSelector == null) return;

                var updatedCollection = collectionSelector(entity).ToArray();
                var collectionInDb = collectionSelector(entityInDb);

                foreach (var item in collectionInDb)
                {
                    if (!updatedCollection.Contains(item, RepositoryExtensions.IdComparer))
                    {
                        dbContext.Remove(dbContext.Find(item.GetType(), item.Id));
                    }
                }
            }
        }
    }

}
