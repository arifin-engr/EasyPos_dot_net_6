using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Web.Mvc;

namespace EasyPos.Models.AppEntity
{
    public class Product:BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [AllowHtml]
        public string? Description { get; set; }

        public string? Code { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Brand")]
        public int? BrandId { get; set; }
        [Display(Name = "Unit")]
        public int? UnitId { get; set; }
        [Display(Name = "Sub Unit")]
        public int? SubUnitId { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Sale Price")]
        public double SalePrice { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Purchase Cost")]
        public double PurchaseCost { get; set; }

        [ValidateNever]
        public string? ImageUrl { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category? Category { get; set; }


        [ForeignKey("BrandId")]
        [ValidateNever]
        public Brand? Brand { get; set; }

        [ForeignKey("UnitId")]
        [ValidateNever]
        public Unit? Unit { get; set; }

        [ValidateNever]
        public virtual ICollection<Stock>? Stocks { get; set; }
    }
}
