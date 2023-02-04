using EasyPos.Models.AppEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyPos.DAL.Repositories.IRepositories
{
    public interface IRepository<T> where T:BaseEntity
    {
        IEnumerable<T> GetAll(string? includeProperties = null);
        T GetFirstOrDefault(Expression<Func<T,bool>>filter, string? includeProperties = null);
        void Add(T entity);
        void Delete(T entity);

    }
}
