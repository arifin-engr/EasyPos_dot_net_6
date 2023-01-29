using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPos.Models.AppEntity
{
    public class Brand:BaseEntity
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
    }
}
