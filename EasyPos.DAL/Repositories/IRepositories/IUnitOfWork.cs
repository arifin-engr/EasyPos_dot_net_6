﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPos.DAL.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Category { get;}

        void Save();
    }
}
