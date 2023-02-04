using EasyPos.Models.AppEntity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPos.Models.AppVM
{
    public class PosVM
    {
        public Sale? Sales { get; set; }
        public Product? Product { get; set; }


        public IEnumerable<SelectListItem>? ProductList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? CustomerList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? CategoryList { get; set; }
    }
}
