using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Data.Model
{
    public class CustomerOrderDetails
    {
        public int Id { get; set; }
        public CustomerOrder CustomerOrderID { get; set; }
        public Product ProductID { get; set; }
        public int Quantity {  get; set; }
        public double UnitPrice {  get; set; }
        public double TotalPrice {  get; set; }
    }
}
