using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Repository
{
    public class InventoryBranchRepository
    {   
        UpgradeManager upgradeManager;//FOR DATABASE CONNECTION

        //INVENTORY BRANCH RECORD LOAD
        public DataTable LoadInventoryBranchData()
        {
            string query = "SELECT inventory.id AS `Inventory ID`, product.`name` AS `Product Name`, inventory.`description` AS `Description`, inventory.price AS `Price`, inventory.quantity AS `Quantity`, inventory.`expiration` AS `Expiration`, inventory.`day` AS `Day/s Remaining`, inventory.availability AS `Availability` FROM inventory JOIN product ON inventory.`name` = product.id WHERE inventory.expiration <= DATE_ADD(CURDATE(), INTERVAL 3 MONTH) AND inventory.`status` = 'Active' ORDER BY CASE WHEN inventory.expiration >= CURDATE() THEN 0 ELSE 1 END, inventory.expiration DESC;";
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }

        //INVENTORY BRANCH RECORD LOAD WHILE SEARCHING
        public DataTable LoadInventoryBranchData(string searchValue)
        {
            string query = "SELECT inventory.id AS `Inventory ID`, product.`name` AS `Product Name`, inventory.`description` AS `Description`, inventory.price AS `Price`, inventory.quantity AS `Quantity`, inventory.`expiration` AS `Expiration`, inventory.`day` AS `Day/s Remaining`, inventory.availability AS `Availability` FROM inventory JOIN product ON inventory.`name` = product.id WHERE inventory.expiration <= DATE_ADD(CURDATE(), INTERVAL 3 MONTH) AND inventory.`status` = 'Active' AND inventory.id LIKE @val OR inventory.expiration <= DATE_ADD(CURDATE(), INTERVAL 3 MONTH) AND inventory.`status` = 'Active' AND product.`name` LIKE @val ORDER BY CASE WHEN inventory.expiration >= CURDATE() THEN 0 ELSE 1 END, inventory.expiration DESC;";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> inventoryParams = new Dictionary<string, string>()
            {
                { "@val", "%" + searchValue + "%" }
            };

            return upgradeManager.Load(query, inventoryParams);
        }
    }
}
