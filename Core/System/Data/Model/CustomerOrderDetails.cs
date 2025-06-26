using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Data.Model
{
    public class CustomerOrderDetails
    {
        public CustomerOrder CustomerOrderNumber { get; set; }
        public Inventory InventoryID { get; set; }
        public int Quantity {  get; set; }
        public double UnitPrice {  get; set; }
        public double TotalAmount {  get; set; }
        public StatusRecord.Type Status { get; set; }
    }
}
