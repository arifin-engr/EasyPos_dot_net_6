using EasyPos.DAL.Data;
using EasyPos.DAL.Repositories.IRepositories;
using EasyPos.Models.AppEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPos.DAL.Repositories
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        private readonly ApplicationDbContext _db;
        public SupplierRepository(ApplicationDbContext db):base(db) 
        {
            _db= db;
        }
        public void Update(Supplier entity)
        {
            _db.Update(entity);
        }
    }
}
