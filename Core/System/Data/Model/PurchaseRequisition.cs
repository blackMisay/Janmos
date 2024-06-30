using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Data.Model
{
    public class PurchaseRequisition
    {
        public int Id { get; set; }
        public string Vendor { get; set; }
        public string ItemNumber { get; set; }
        public DateTime DeliveryDate { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public double UnitPrice { get; set; }
        public double Amount { get; set; }
        public DateTime RequisitionDate { get; set; }
        public string Currency { get; set; }
        public DateTime TermDate { get; set; }

    }
}
