using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Data.Model
{
    public class AuditLog
    {
        public int Id { get; set; }
        public User UserId { get; set; }
        public String ActionType { get; set; }
        public String TableName { get; set; }
        public int? RecordId { get; set; }
        public String OldValue { get; set; }
        public String NewValue { get; set; }
        public String Description { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
