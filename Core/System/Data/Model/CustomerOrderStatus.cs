using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Data.Model
{
    public class CustomerOrderStatus
    {
        public int Id { get; set; }
        public string OrderStatus { get; set; }
        public StatusRecord.Type Status { get; set; }
    }
}
