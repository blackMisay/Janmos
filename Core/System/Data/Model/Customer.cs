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
        public enum Entity 
        {  get; set; }
        public string EntityName {  get; set; }
        public string MobileNum { get; set; }
        public string TeleNum { get; set; }
        public string Extension { get; set; }
        public string Email { get; set; }
        public string SocialNetId { get; set; }
        public string Region { get; set; }
        public string Municipality { get; set; }
        public string Baranggay { get; set; }
        public string Housenum { get; set; }
        public string Postal { get; set; }
    }
}
