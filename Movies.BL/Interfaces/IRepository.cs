using System;
using System.Collections.Generic;
using System.Text;

using Movies.DAL.Interfaces;

namespace Movies.BL.Interfaces
{
	public interface IRepository<TEntity, TListModel, TDetailModel>
		where TEntity : class, IEntity, new()
		where TDetailModel : IGuid, new()
		where TListModel : IGuid, new()
	{
		TDetailModel InsertOrUpdate(TDetailModel model);
		void Delete(TDetailModel entity);
		void Delete(Guid entityId);
		TDetailModel GetById(Guid entityId);
		IEnumerable<TListModel> GetAll();
	}
}
