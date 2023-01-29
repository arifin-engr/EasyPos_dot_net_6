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
    public class BrandRepository : Repository<Brand>, IBrandRepsoitory
    {
        private readonly ApplicationDbContext _db;
        public BrandRepository(ApplicationDbContext db):base(db) 
        {
            _db= db;
        }
        public void Update(Brand entity)
        {
            _db.Update(entity);
        }
    }
}
