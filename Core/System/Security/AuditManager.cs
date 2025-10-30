using Core.System.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Security
{
    public static class AuditManager
    {
        private static readonly Repository.AuditLogRepository repo = new Repository.AuditLogRepository();

        public static void Log(
            User user,
            string actionType,
            string tableName = null,
            int? recordId = null,
            string oldValue = null,
            string newValue = null,
            string description = null
            )
        {
            AuditLog log = new AuditLog();
            log.UserId = new User { Id = Convert.ToInt32(user.Id)};
            log.ActionType = actionType;
            log.TableName = tableName;
            log.RecordId = recordId;
            log.OldValue = oldValue;
            log.NewValue = newValue;
            log.Description = description;
            log.TimeStamp = DateTime.Now;

            repo.Insert(log);
        }
    }
}
