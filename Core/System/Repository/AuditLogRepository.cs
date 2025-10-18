using Core.System.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Repository
{
    public class AuditLogRepository
    {
        public bool Insert(AuditLog log)
        {
            string query = "INSERT INTO dbjanmos.auditlog(userid, actiontype, tablename, recordid, oldvalue, newvalue, `description`, `timestamp`) VALUES(@UserId, @ActionType, @TableName, @RecordId, @OldValue, @NewValue, @`Description`, @`Timestamp`);";

            UpgradeManager manager = new UpgradeManager();
            if (manager.ExecuteQuery(query, new Dictionary<string, object>
            {
                { "@UserId", log.UserId.Id},
                { "@ActionType", log.ActionType ?? (object)DBNull.Value},
                { "@TableName", log.TableName ?? (object)DBNull.Value},
                { "@RecordId", log.RecordId ?? (object)DBNull.Value},
                { "@OldValue", log.OldValue ?? (object)DBNull.Value},
                { "@NewValue", log.NewValue ?? (object)DBNull.Value},
                { "@Description", log.Description ?? (object)DBNull.Value},
                { "@Timestamp", log.TimeStamp}
            }))
                return true;
            return false;
        }
    }
}
