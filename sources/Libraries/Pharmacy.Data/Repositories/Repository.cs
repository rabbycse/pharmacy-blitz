
namespace Pharmacy.Data.Repositories
{
    using Interfaces;
    using System.Linq;
    using System.Data.Entity;

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class

    {
        protected DbContext db;

        public Repository(DbContext _db)
        {
            db = _db;
        }

        public void Insert(TEntity entity) 
        {
            db.Set<TEntity>().Add(entity);
        }

        public void DeleteById(object Id)
        {
            var entity = db.Set<TEntity>().Find(Id);

            if (entity != null)
            {
                db.Set<TEntity>().Remove(entity);
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            return db.Set<TEntity>();
        }

        public TEntity GetById(object Id)
        {
            return db.Set<TEntity>().Find(Id);
        }

        public void Update(TEntity entity)
        {
            db.Entry<TEntity>(entity).State = EntityState.Modified;
        }
    }
}
