using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPos.Models.AppEntity
{
    public class PurchaseItem:BaseEntity    {
        public int PurchaseId { get; set; }
        public int ProductId { get; set; }
        public int UnitId { get; set; }
        public double Price { get; set; }
        public float Quantity { get; set; }
        public double? SubTotal { get; set; }

        [ForeignKey("PurchaseId")]
        [ValidateNever]
        public Purchase? Purchase { get; set; }

        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product? Product { get; set; }
        public Unit? Unit { get; set; }
    }
}
