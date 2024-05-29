using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Data.Model
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SupplierCategory Category { get; set; }
        public string Contactperson { get; set; }
        public string Socialnetid { get; set; }
        public string Mobilenum {  get; set; }
        public string Phonenum { get; set; }
        public string Extension { get; set; }
        public string Email { get; set; }
        public Region Region { get; set; }
        public Province Province { get; set; }
        public Municipality Municipality { get; set; }
        public Baranggay Baranggay { get; set; }
        public string Housenum { get; set; }
        public string Postal { get; set; }
    }
}
