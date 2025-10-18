using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Data.Model
{
    public class Inventory
    {
        public int Id { get; set; }
        public Product Name {  get; set; }
        public string Description { get; set; }

        private double _Price;
        public string Price { get { return _Price.ToString(); } set { if (double.TryParse(value, out double result)) { _Price = result; } } }
        public int Quantity {  get; set; }
        public string EntryDate { get; set; }

        private string expiration;
        public string Expiration { get { return expiration; } set { expiration = value; } }
        public string Day {  get; set; }
        public Availability Availability { get; set; }
        public User CreatedBy { get; set; }
        public StatusRecord.Type Status { get; set; }
    }
    public enum Availability
    {
        Available,
        Unavailable
    }
}
