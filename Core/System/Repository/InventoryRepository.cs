using Core.System.Data.Model;
using Core.System.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Core.System.Repository
{
    public class InventoryRepository
    {
        UpgradeManager upgradeManager;//for database connection
        private User user = new User();
        public DataTable LoadInventoryData()//for record load
        {
            string query = "SELECT i.id AS `Inventory ID`, p.`name` AS `Product Name`, i.`description` AS `Description`, i.price AS `Price`, i.quantity AS `Quantity`, i.`expiration` AS `Expiration`, i.`day` AS `Day/s Remaining`, i.availability AS `Availability`, CONCAT(r.roletitle, ' - ', ui.givenname, ' ', ui.lastname) AS `Created By` FROM inventory i JOIN product p ON i.`name` = p.id JOIN `user` u ON i.createdby = u.id JOIN roles r ON u.roleid = r.id JOIN userinfo ui ON u.userinfoid = ui.id WHERE i.`status` = 'Active' ORDER BY i.id DESC;";
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }

        public DataTable LoadInventoryData(string searchValue)//for record load while searching
        {
            string query = "SELECT i.id AS `Inventory ID`, p.`name` AS `Product Name`, i.`description` AS `Description`, i.price AS `Price`, i.quantity AS `Quantity`, i.`expiration` AS `Expiration`, i.`day` AS `Day/s Remaining`, i.availability AS `Availability`, CONCAT(r.roletitle, ' - ', ui.givenname, ' ', ui.lastname) AS `Created By` FROM inventory i JOIN product p ON i.`name` = p.id JOIN `user` u ON i.createdby = u.id JOIN roles r ON u.roleid = r.id JOIN userinfo ui ON u.userinfoid = ui.id WHERE i.`status` = 'Active' AND(p.`name` LIKE @val OR i.id LIKE @val OR i.`description` LIKE @val OR i.availability LIKE @val OR CONCAT(ui.givenname, ' ', ui.lastname) LIKE @val) ORDER BY i.id DESC;";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> inventoryParams = new Dictionary<string, string>()
            {
                { "@val", "%" + searchValue + "%" }
            };

            return upgradeManager.Load(query, inventoryParams);
        }
        public bool UpdateInventoryStock(int productId, int quantity)
        {
            string query = @"
                UPDATE inventory
                SET quantity = quantity - @Qty
                WHERE `name` = @ProductId;
            ";

            upgradeManager = new UpgradeManager();

            Dictionary<string, string> stockParams = new Dictionary<string, string>()
            {
                { "@Qty", quantity.ToString() },
                { "@ProductId", productId.ToString() }
            };

            return upgradeManager.ExecuteQuery(query, stockParams);
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
                    inventory.CreatedBy = new User() { Id = Convert.ToInt32(row["createdby"]) };
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

        public bool Save(AuditLog log, Inventory updInventory, User user)//saving data entry
        {
            this.user = user;
            string query;

            if (updInventory.Id > 0) //for updating existing record
            {
                query = "UPDATE dbjanmos.inventory SET name=@Name,description=@Description,price=@Price,quantity=@Quantity,entrydate=@EntryDate,expiration=@Expiration,day=@Day,availability=@Availability,createdby=@UserId,status=@Status WHERE id=@Id;";
            }
            else //for add new record
            {
                query = "INSERT INTO dbjanmos.inventory(name,description,price,quantity,entrydate,expiration,day,availability,createdby,status) VALUES(@Name,@Description,@Price,@Quantity,@EntryDate,@Expiration,@Day,@Availability,@UserId,@Status);";
            }

            Dictionary<string, string> inventoryParameters = new Dictionary<string, string>()
            {
                {"@Id", updInventory.Id.ToString()},
                {"@Name", updInventory.Name.Id.ToString()},
                {"@Description", updInventory.Description},
                {"@Price", updInventory.Price},
                {"@Quantity", updInventory.Quantity.ToString()},
                {"@EntryDate", updInventory.EntryDate },
                {"@Expiration", updInventory.Expiration},
                {"@Day", updInventory.Day.ToString() },
                {"@Availability", updInventory.Availability.ToString()},
                {"@UserId",updInventory.CreatedBy.Id.ToString()},
                {"@Status", updInventory.Status.ToString()}
            };
            if (updInventory.Id > 0)
            {
                if (upgradeManager.ExecuteQuery(query, inventoryParameters))
                {
                    AuditManager.Log(log.UserId, log.ActionType, log.TableName, log.RecordId, log.OldValue, log.NewValue, log.Description);
                    return true;
                }
                return false;
            }
            else
            {
                int newId = upgradeManager.ExecuteQuery(query, inventoryParameters, true);
                if (newId == 0)
                    return false;
                AuditManager.Log(this.user, ActionType.CREATE, tableName: "Inventory", recordId: newId, description: "The user added new product '" + updInventory.Name + "' in inventory.");
                return true;
            }
        }
    }
}