
namespace Pharmacy.Data
{
    using Models;
    using Repositories;
    using Repositories.Interfaces;

    public class UnitOfWork :IUnitOfWork
    {
        PharmacyEntities _db;

        public UnitOfWork()
        {
            _db = new PharmacyEntities();
        }

        private IRepository<Supplier> _SupplierRepository; 

        public IRepository<Supplier> SupplierRepository
        {
            get
            {
                if (_SupplierRepository == null)
                    _SupplierRepository = new Repository<Supplier>(_db);
                return _SupplierRepository;
            }
        }

        public int Save() 
        {
            return _db.SaveChanges();
        }

        
    }
}
