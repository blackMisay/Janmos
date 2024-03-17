using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Data.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public entity Entity { get; set; }
        public string Entityname {  get; set; }
        public string Mobilenum { get; set; }
        public string Telenum { get; set; }
        public string Extension { get; set; }
        public string Email { get; set; }
        public string Socialnetid { get; set; }
        public Region Region { get; set; }
        public Province Province { get; set; }
        public Municipality Municipality { get; set; }
        public Baranggay Baranggay { get; set; }
        public string Housenum { get; set; }
        public string Postal { get; set; }
    }
    public enum entity
    {
        Individual = 1,
        Business = 2,
        Organization = 3
    }
}
