using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Data.Model
{
    public class Roles
    {
        public int Id { get; set; }
        public string RoleTitle { get; set; }
        public string Description { get; set; }
        public User CreatedBy { get; set; }
        public string CreatedDate { get; set; }
    }
}
