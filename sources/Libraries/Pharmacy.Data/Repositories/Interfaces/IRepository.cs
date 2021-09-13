
namespace Pharmacy.Data.Repositories.Interfaces
{
    using System;
    using System.Linq;

    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        TEntity GetById(Object Id);

        void Insert(TEntity entity); 

        void Update(TEntity entity);

        void DeleteById(Object Id);

    }
}
