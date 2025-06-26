using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Data.Model
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string Contact {  get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public StatusRecord.Type Status { get; set; }
    }
    public enum Gender
    {
        Male,
        Female
    }
}
