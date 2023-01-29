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
    public class SalesRepository : Repository<Sale>, ISalesRepository
    {
        private readonly ApplicationDbContext _db;
        public SalesRepository(ApplicationDbContext db):base(db) 
        {
            _db= db;
        }
        public void Update(Sale entity)
        {
            _db.Update(entity);
        }
    }
}
