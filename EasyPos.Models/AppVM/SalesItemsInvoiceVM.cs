using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPos.Models.AppVM
{
    public class SalesItemsInvoiceVM
    {
        public string? ProductName { get; set; }
        public string? ProductCode { get; set; }
        public string? UnitName { get; set; }
        public string? SubUnitName { get; set; }
        public string? Price { get; set; }
        public string? MainUnitQuantity { get; set; }
        public string? SubUnitQuantity { get; set; }
        public string? SubTotal { get; set; }
    }
}
