using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPos.Models.AppEntity
{
    public class Purchase:BaseEntity
    {
        public double? Due { get; set; }
        public double Paid { get; set; }
        public double? GrandTotal { get; set; }

        public int? SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        [ValidateNever]
        public Supplier? Supplier { get; set; }
        [ValidateNever]
        public List<PurchaseItem>? PurchaseItems { get; set; }
    }
}
