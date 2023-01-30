using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPos.Models.AppEntity
{
    public class Sale:BaseEntity
    {
        public string? CustomerName { get; set; }
        public int? MobileNo { get; set; }
        public double? Discount { get; set; }
        public string? Note { get; set; }
        public double? Paid { get; set; }
        public double? GrandTotal { get; set; }
        public double? Receivable { get; set; }
        public double? Due { get; set; }
        public double? Profit { get; set; }
        [ValidateNever]
        public List<SalesItem>? SalesItems { get; set; }
    }
}
