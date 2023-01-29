using EasyPos.Models.AppEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPos.DAL.Repositories.IRepositories
{
    public interface IPurchaseRepository : IRepository<Purchase>
    {
        void Update(Purchase entity);
    }
}
