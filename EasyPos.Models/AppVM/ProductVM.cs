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
    public class ProductVM
    {
        public Product Product { get; set; }
        public Category? Category { get; set; }
        public Brand? Brand { get; set; }
        public Unit? Unit { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? CategoryList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? UnitList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? BrandList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? SubUnitList { get; set; }

        public Unit? SubUnit { get; set; }
    }
}
