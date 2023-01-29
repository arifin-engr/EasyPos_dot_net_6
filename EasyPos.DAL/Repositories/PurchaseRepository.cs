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
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        private readonly ApplicationDbContext _db;
        public PurchaseRepository(ApplicationDbContext db):base(db) 
        {
            _db= db;
        }
        public void Update(Purchase entity)
        {
            _db.Update(entity);
        }
    }
}
