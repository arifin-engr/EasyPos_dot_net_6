using EasyPos.Models.AppEntity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EasyPos.Models.AppVM
{
    public class UnitVM
    {
        public Unit Unit { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? UnitList { get; set; }
    }
}
