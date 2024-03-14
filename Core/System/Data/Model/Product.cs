using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Data.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public MetricUnit MetricUnit { get; set; }

        private double _double;
        public string MetricValue 
        { 
            get { return _double.ToString(); } 
            set 
            { if (double.TryParse(value,out double result)) 
                {
                    _double = result;
                }
            }
        }
    }
}