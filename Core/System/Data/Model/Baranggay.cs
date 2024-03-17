using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Data.Model
{
    public class Baranggay
    {
        public int Id { get; set; }
        public Municipality Municipality { get; set; }
        public string Name { get; set; }
    }
}
