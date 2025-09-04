using Core.System.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Repository
{
    public class CustomerOrderDetailsRepository
    {
        UpgradeManager upgradeManager;

        // LOAD CUSTOMER ORDER DETAILS DATA VIA ORDER ID
        public DataTable LoadCustomerOrderDetailsData(string customerOrderNumber)//for record load
        {
            string query = "SELECT product.`name` AS `Product Name`, customerorderdetails.quantity AS `Quantity`, customerorderdetails.unitprice AS `Unit Price`, customerorderdetails.totalamount AS `Total Amount` FROM customerorderdetails JOIN inventory ON customerorderdetails.inventoryid = inventory.id JOIN product ON inventory.`name` = product.id WHERE customerorderdetails.ordernumber = '" + customerOrderNumber+"';";
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }
        public DataTable LoadCustomerOrderDetailsData(string customerOrderNumber, string searchValue)
        {
            string query = "SELECT product.`name` AS `Product Name`, customerorderdetails.quantity AS `Quantity`, customerorderdetails.unitprice AS `Unit Price`, customerorderdetails.totalamount AS `Total Amount` FROM customerorderdetails JOIN inventory ON customerorderdetails.inventoryid = inventory.id JOIN product ON inventory.`name` = product.id WHERE customerorderdetails.ordernumber = '" + customerOrderNumber + "' AND product.`name` LIKE @val;";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> inventoryParams = new Dictionary<string, string>()
            {
                { "@val", "%" + searchValue + "%" }
            };
            return upgradeManager.Load(query, inventoryParams);
        }
        public int VerifyCustomerOrderDetailsIfExisted(string ordernumber, int inventoryId)
        {
            string query = "SELECT COUNT(*) FROM customerorderdetails WHERE customerorderdetails.ordernumber = @OrderNumber AND customerorderdetails.inventoryid = @InventoryId";
            upgradeManager = new UpgradeManager();
            Dictionary<string, string> verifyCustomerOrderParams = new Dictionary<string, string>()
            {
                { "@OrderNumber", ordernumber},
                { "@InventoryId",  inventoryId.ToString()},
            };
            int result = Convert.ToInt32(upgradeManager.ExecuteScalar(query, verifyCustomerOrderParams));
            return result;
        }

        public List<CustomerOrderDetails> GetCustomerOrder(string customerOrderNumber)
        {
            string query = "SELECT inventory.id AS `Inventory Id`, product.`name` AS `Product Name`, customerorderdetails.quantity AS `Quantity`, customerorderdetails.unitprice AS `Unit Price`, customerorderdetails.totalamount AS `Total Amount` FROM customerorderdetails JOIN inventory ON customerorderdetails.inventoryid = inventory.id JOIN product ON inventory.`name` = product.id WHERE customerorderdetails.ordernumber = @OrderNumber ORDER BY customerorderdetails.ordernumber DESC;";
            Dictionary<string, string> getCustomerOrderParams = new Dictionary<string, string>()
            {
                { "@OrderNumber", customerOrderNumber},
            };
            upgradeManager = new UpgradeManager();
            return upgradeManager.GetCustomerOrderProduct(query, getCustomerOrderParams);
        }

        public DataTable LoadDataList(string query)//load data list
        {
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }

        public bool Delete(CustomerOrderDetails customerOrderDetails)
        {
            string query = "UPDATE customerorderdetails SET customerorderdetails.`status` = @Status WHERE customerorderdetails.ordernumber = @OrderNumber AND customerorderdetails.inventoryid = @InventoryId;";

            Dictionary<string, string> deleteParams = new Dictionary<string, string>()
            {
                { "@OrderNumber",  customerOrderDetails.CustomerOrderNumber.OrderNumber.ToString()},
                { "@InventoryId", customerOrderDetails.InventoryID.Id.ToString() },
                { "@Status", customerOrderDetails.Status.ToString()},
            };

            upgradeManager = new UpgradeManager();
            if (upgradeManager.ExecuteQuery(query, deleteParams))
                return true;
            return false;
        }

        public bool Save(CustomerOrderDetails customerOrderDetails)//saving data entry
        {
            string query;

            string checkQuery = "SELECT COUNT(*) FROM dbjanmos.customerorderdetails WHERE customerorderdetails.ordernumber = @OrderNumber AND customerorderdetails.inventoryid = @InventoryId;";
            
            Dictionary<string, string> checkParams = new Dictionary<string, string>()
            {
                { "@OrderNumber", customerOrderDetails.CustomerOrderNumber.OrderNumber},
                { "@InventoryId", customerOrderDetails.InventoryID.Id.ToString() }
            };

            upgradeManager = new UpgradeManager();
            int recordExists = Convert.ToInt32(upgradeManager.ExecuteScalar(checkQuery, checkParams));

            if (recordExists > 0) //for updating existing record
            {
                query = "UPDATE dbjanmos.customerorderdetails SET quantity=@Quantity,unitprice=@UnitPrice,totalamount=@TotalAmount WHERE ordernumber=@OrderNumber AND inventoryid=@InventoryId;";
            }
            else //for add new record
            {
                query = "INSERT INTO dbjanmos.customerorderdetails(ordernumber,inventoryid,quantity,unitprice,totalamount) VALUES(@OrderNumber,@InventoryId,@Quantity,@UnitPrice,@TotalAmount);";
            }

            Dictionary<string, string> customerOrderDetailsParameters = new Dictionary<string, string>()
            {
                {"@OrderNumber", customerOrderDetails.CustomerOrderNumber.OrderNumber},
                {"@InventoryId", customerOrderDetails.InventoryID.Id.ToString()},
                {"@Quantity", customerOrderDetails.Quantity.ToString()},
                {"@UnitPrice", customerOrderDetails.UnitPrice.ToString()},
                {"@TotalAmount", customerOrderDetails.TotalAmount.ToString()}
            };

            upgradeManager = new UpgradeManager();
            if (upgradeManager.ExecuteQuery(query, customerOrderDetailsParameters))
                return true;
            return false;
        }
    }
}
