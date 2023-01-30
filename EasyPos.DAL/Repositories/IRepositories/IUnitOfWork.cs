using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPos.DAL.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Category { get;}
        public IBrandRepsoitory Brand { get;}
        public IProductRepository Product { get;}
        public IUnitRepository Unit { get;}
        public ICustomerRepository Customer { get;}
        public ISupplierRepository Supplier { get;}
        public ISalesRepository Sales { get;}
        public IPurchaseRepository Purchase { get;}
       // public ICategoryRepository Category { get;}

        void Save();
    }
}
