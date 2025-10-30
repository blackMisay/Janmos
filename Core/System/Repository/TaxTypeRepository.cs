using Core.System.Data.Model;
using Core.System.Security;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Repository
{
    public class TaxTypeRepository
    {
        public bool Save(TaxType taxtype, User user)
        {
            string query = "INSERT INTO dbjanmos.taxtype(taxname, rate, createdby, createddate) VALUES(@TaxName, @Rate, @CreatedBy, @CreatedDate);";
            UpgradeManager manager = new UpgradeManager();
            int newId = manager.ExecuteQuery(query, new Dictionary<string, string>
            {
                { "@TaxName", taxtype.TaxName},
                { "@Rate", taxtype.Rate.ToString()},
                { "@CreatedBy", taxtype.CreatedBy.Id.ToString() },
                { "@CreatedDate", taxtype.CreatedDate}
            }, true);
            if (newId == 0)
            {
                return false;
            }
            AuditManager.Log(user, ActionType.CREATE, tableName: "Tax Type", recordId: newId, description: "Added new tax type '" + taxtype.TaxName + "'.");
            return true;
        }
    }
}
