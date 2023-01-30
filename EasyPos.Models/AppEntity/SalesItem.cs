using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPos.Models.AppEntity
{
    public class SalesItem:BaseEntity
    {
        public int SalesId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public float Quantity { get; set; }
        public double? SubTotal { get; set; }
        public double? PurchaseCost { get; set; }

        [ForeignKey("SalesId")]
        [ValidateNever]
        public Sale? Sales { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product? Product { get; set; }
    }
}
