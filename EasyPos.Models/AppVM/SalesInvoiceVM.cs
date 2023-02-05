using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPos.Models.AppVM
{
    public class SalesInvoiceVM
    {
        public string? InvoiceId { get; set; }
        public string? Date { get; set; }
        public string? Note { get; set; }

        public string? Due { get; set; }
        public string? Paid { get; set; }
        public string? GrandTotal { get; set; }

        public string? TransactionAccount { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public List<SalesItemsInvoiceVM>? SalesItems { get; set; }
    }
}
