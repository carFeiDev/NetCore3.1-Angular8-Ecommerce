using Project.TakuGames.Model.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Proyect.TakuGames.Test.FakeDAL
{
    public class FakeRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        List<TEntity> _data;

        public FakeRepository()
        {
            _data = new List<TEntity>();
        }

        public List<TEntity> GetAll()
        {
            return _data;
        }

        public void Delete(TEntity entityToDelete)
        {

            _data.Remove(entityToDelete);

        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return _data.AsQueryable();
        }

        public TEntity GetByID(object id)
        {
            return _data.FirstOrDefault();
        }

        public void Insert(TEntity entity)
        {
            _data.Add(entity);
        }

        public void Update(TEntity entityToUpdate)
        {
            _data.Add(entityToUpdate);

            //throw new NotImplementedException();
        }
    }
}
