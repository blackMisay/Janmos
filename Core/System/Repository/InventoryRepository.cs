using System.Collections.Generic;
using Core.System.Data.Model;
using System.Data;
using System;
using System.Data.SqlClient;

namespace Core.System.Repository
{
    public class InventoryRepository
    {
        UpgradeManager upgradeManager;//for database connection
        public DataTable LoadInventoryData()//for record load
        {
            string query = "SELECT inventory.id AS `Inventory ID`, product.`name` AS `Product Name`, inventory.`description` AS `Description`, inventory.price AS `Price`, inventory.quantity AS `Quantity`, inventory.`expiration` AS `Expiration`, inventory.day AS `Day/s Remaining`, inventory.availability AS `Availability` FROM inventory JOIN product ON inventory.`name` = product.id WHERE inventory.`status` = 'Active' ORDER BY inventory.id DESC;";
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }

        public DataTable LoadInventoryData(string searchValue)//for record load while searching
        {
            string query = "SELECT inventory.id AS `Inventory ID`, product.`name` AS `Product Name`, inventory.`description` AS `Description`, inventory.price AS `Price`, inventory.quantity AS `Quantity`, inventory.`expiration` AS `Expiration`, inventory.day AS `Day/s Remaining`, inventory.availability AS `Availability` FROM inventory JOIN product ON inventory.`name` = product.id WHERE inventory.`name` LIKE @val AND inventory.`status` = 'Active' ORDER BY inventory.id DESC;";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> inventoryParams = new Dictionary<string, string>()
            {
                { "@val", "%" + searchValue + "%" }
            };

            return upgradeManager.Load(query, inventoryParams);
        }
        public bool DeleteInventoryData(int inventoryId)//for deleting selected record
        {
            string query = "UPDATE inventory SET `status` = 'Deleted' WHERE id = @id";
            upgradeManager = new UpgradeManager();
            Dictionary<string, string> inventoryParams = new Dictionary<string, string>()
            {
                { "@id", inventoryId.ToString() }
            };
            return upgradeManager.ExecuteQuery(query, inventoryParams);
        }

        public Inventory FetchInventoryData(int inventoryId)//fetching data from database to form
        {
            DataTable dt = new DataTable();
            upgradeManager = new UpgradeManager();
            dt = upgradeManager.Load("SELECT * FROM dbjanmos.inventory WHERE inventory.id=" + inventoryId);

            if (dt.Rows.Count > 0)
            {
                Inventory inventory = new Inventory();
                foreach (DataRow row in dt.Rows)
                {
                    inventory.Name = new Product() { Id = Convert.ToInt32(row["name"]) };
                    inventory.Description = row["description"].ToString();
                    inventory.Price = row["price"].ToString();
                    inventory.Quantity = Convert.ToInt32(row["quantity"]);
                    inventory.Expiration = row["expiration"].ToString();
                    inventory.Day = row["day"].ToString();
                    inventory.Availability = new Availability();
                }
                return inventory;
            }
            return null;
        }

        public DataTable LoadDataList(string query)//load data list
        {
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }

        public bool Save(Inventory inventory)//saving data entry
        {
            string query;

            if (inventory.Id > 0) //for updating existing record
            {
                query = "UPDATE dbjanmos.inventory SET name=@Name,description=@Description,price=@Price,quantity=@Quantity,expiration=@Expiration,day=@Day,availability=@Availability,status=@Status WHERE id=@Id;";
            }
            else //for add new record
            {
                query = "INSERT INTO dbjanmos.inventory(name,description,price,quantity,expiration,day,availability,status) VALUES(@Name,@Description,@Price,@Quantity,@Expiration,@Day,@Availability,@Status);";
            }

            Dictionary<string, string> inventoryParameters = new Dictionary<string, string>()
            {
                {"@Id", inventory.Id.ToString()},
                {"@Name", inventory.Name.Id.ToString()},
                {"@Description", inventory.Description},
                {"@Price", inventory.Price},
                {"@Quantity", inventory.Quantity.ToString()},
                {"@Expiration", inventory.Expiration},
                {"@Day", inventory.Day.ToString() },
                {"@Availability", inventory.Availability.ToString()},
                {"@Status", inventory.Status.ToString()}
            };

            upgradeManager = new UpgradeManager();
            if (upgradeManager.ExecuteQuery(query, inventoryParameters))
                return true;
            return false;
        }
    }
}