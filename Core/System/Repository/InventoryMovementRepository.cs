using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Repository
{
    public class InventoryMovementRepository
    {
        UpgradeManager upgradeManager;

        public bool InsertInventoryMovement(int productId, int quantity, string transactionType, string remarks, int userId)
        {
            string query = @" INSERT INTO inventory_movement (product_id, quantity, transaction_type, date_entry, remarks, user_id)
                VALUES (@ProductId, @Qty, @Type, NOW(), @Remarks, @UserId);"
            ;

            Dictionary<string, string> movementParams = new Dictionary<string, string>()
            {
                { "@ProductId", productId.ToString() },
                { "@Qty", quantity.ToString() },
                { "@Type", transactionType },
                { "@Remarks", remarks },
                { "@UserId", userId.ToString() }
            };

            upgradeManager = new UpgradeManager();
            return upgradeManager.ExecuteQuery(query, movementParams);
        }
    }
}
