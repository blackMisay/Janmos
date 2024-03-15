using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Data.Model
{
    public class MetricUnit
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public QuantityType Quantity { get; set; }
    }

    public enum QuantityType
    {
            g = 1,
            Kg = 2,
            mL = 3,
            L = 4,
            pcs = 5
    }
}
