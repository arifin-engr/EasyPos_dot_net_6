using EasyPos.DAL.Data;
using EasyPos.DAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPos.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            Category = new CategoryRepository(_db);
            Brand = new BrandRepository(_db);
            Unit = new UnitRepository(_db);
            Customer = new CustomerRepository(_db);
            Supplier = new SupplierRepository(_db);
            Sales = new SalesRepository(_db);
            Purchase = new PurchaseRepository(_db);
        }
        public ICategoryRepository Category { get; private set; }

        public IBrandRepsoitory Brand { get; private set; }

        public IProductRepository Product { get; private set; }

        public IUnitRepository Unit { get; private set; }

        public ICustomerRepository Customer { get; private set; }

        public ISupplierRepository Supplier { get; private set; }

        public ISalesRepository Sales { get; private set; }

        public IPurchaseRepository Purchase { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
