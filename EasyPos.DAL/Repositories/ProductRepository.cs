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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db):base(db) 
        {
            _db= db;
        }
        public void Update(Product entity)
        {
            _db.Update(entity);
        }
    }
}
