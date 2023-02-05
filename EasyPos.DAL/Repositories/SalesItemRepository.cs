using EasyPos.DAL.Data;
using EasyPos.DAL.Repository.IRpository;
using EasyPos.Models.AppEntity;
using EasyPos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyPos.DAL.Repositories;

namespace EasyPos.DAL.Repository
{
    public class SalesItemRepository : Repository<SalesItem>, ISalesItemRepository
    {
        private ApplicationDbContext _db;
        public SalesItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       
    }
}
