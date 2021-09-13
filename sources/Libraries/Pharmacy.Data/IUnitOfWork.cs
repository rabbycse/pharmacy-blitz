
namespace Pharmacy.Data
{
    using Models;
    using Repositories.Interfaces;

    public interface IUnitOfWork
    {
        IRepository<Supplier> SupplierRepository { get; } 

        int Save(); 
    }
}
