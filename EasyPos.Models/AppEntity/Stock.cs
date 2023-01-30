using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPos.Models.AppEntity
{
    public class Stock:BaseEntity
    {
        public float OpeningStock { get; set; }
        public float RunningStock { get; set; }
        public float SaleItem { get; set; }
        public float PurchaseItem { get; set; }
        public int ProductId { get; set; }

        public int UnitId { get; set; }
        public Unit? Unit { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
