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
    public class CustomerOrderRepository
    {
        UpgradeManager upgradeManager;

        public DataTable LoadCustomerOrderData()//for loading data
        {
            string query = "SELECT co.id AS `Customer Order Id`, co.ordernumber AS `Customer Order Number`, c.`name` AS `Customer`, co.orderdate AS `Order Date`, `cos`.orderstatus AS `Order Status`, co.totalprice AS `Total Price` FROM customerorder co JOIN customer c ON co.customer = c.id JOIN customerorderstatus `cos` ON co.orderstatus = `cos`.id WHERE co.`status` = 'Active' ORDER BY co.id DESC;";
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }
        /*public DataTable LoadCustomerOrderData(string searchValue)//for loading data while searching
        {
            string query = "SELECT co.id AS `Customer Order Id`, co.ordernumber AS `Customer Order Number`, c.`name` AS `Customer`, co.orderdate AS `Order Date`, `cos`.orderstatus AS `Order Status`, co.totalprice AS `Total Price` FROM customerorder co JOIN customer c ON co.customer = c.id JOIN customerorderstatus `cos` ON co.orderstatus = `cos`.id WHERE c.`name` LIKE @val AND co.`status` = 'Active' OR co.ordernumber LIKE @val AND co.`status` = 'Active' OR co.id LIKE @val AND co.`status` = 'Active' OR co.orderdate LIKE @val AND co.`status` = 'Active' OR `cos`.orderstatus LIKE @val AND co.`status` = 'Active' ORDER BY co.id DESC;";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> customerorderParams = new Dictionary<string, string>()
            {
                { "@val", "%" + searchValue + "%" }
            };

            return upgradeManager.Load(query, customerorderParams);
        }*/
        public DataTable LoadCustomerOrderData(string searchValue, string selectedOrderStatus)//for loading data while searching
        {
            string query = "SELECT co.id AS `Customer Order Id`, co.ordernumber AS `Customer Order Number`, c.`name` AS `Customer`, co.orderdate AS `Order Date`, `cos`.orderstatus AS `Order Status`, co.totalprice AS `Total Price` FROM customerorder co JOIN customer c ON co.customer = c.id JOIN customerorderstatus `cos` ON co.orderstatus = `cos`.id WHERE co.id LIKE @val AND `cos`.orderstatus = @OrderStatus AND co.`status` = 'Active' OR co.id LIKE @val AND co.`status` = 'Active' OR co.ordernumber LIKE @val AND `cos`.orderstatus = @OrderStatus AND co.`status` = 'Active' OR co.ordernumber LIKE @val AND co.`status` = 'Active' OR c.`name` LIKE @val AND `cos`.orderstatus = @OrderStatus AND co.`status` = 'Active' OR c.`name` LIKE @val AND co.`status` = 'Active' OR co.orderdate LIKE @val AND `cos`.orderstatus = @OrderStatus AND co.`status` = 'Active' OR co.orderdate LIKE @val AND co.`status` = 'Active' OR `cos`.orderstatus LIKE @val AND `cos`.orderstatus = @OrderStatus AND co.`status` = 'Active' OR `cos`.orderstatus LIKE @val AND co.`status` = 'Active' OR `cos`.orderstatus = @OrderStatus AND co.`status` = 'Active' ORDER BY co.id DESC;";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> customerorderParams = new Dictionary<string, string>()
            {
                { "@val", "%" + searchValue + "%" },
                { "@OrderStatus", selectedOrderStatus }
            };

            return upgradeManager.Load(query, customerorderParams);
        }
        public DataTable LoadCustomerOrderData(string searchValue)
        {
            return this.LoadCustomerOrderData(searchValue, null);
        }
        public DataTable LoadCustomerOrderDataViaOrderStatus(string selectedOrderStatus)
        {
            return this.LoadCustomerOrderData(null, selectedOrderStatus);
        }
        /*public DataTable LoadCustomerOrderDataViaOrderStatus(string selectedOrderStatus)
        {
            string query = "SELECT co.id AS `Customer Order Id`, co.ordernumber AS `Customer Order Number`, c.`name` AS `Customer`, co.orderdate AS `Order Date`, `cos`.orderstatus AS `Order Status`, co.totalprice AS `Total Price` FROM customerorder co JOIN customer c ON co.customer = c.id JOIN customerorderstatus `cos` ON co.orderstatus = `cos`.id WHERE `cos`.orderstatus = @OrderStatus AND co.`status` = 'Active' ORDER BY co.id DESC;";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> customerorderparams = new Dictionary<string, string>()
            {
                { "@OrderStatus", selectedOrderStatus}
            };

            return upgradeManager.Load(query, customerorderparams);
        }
        public DataTable LoadCustomerOrderDataViaOrderStatusAndSearchBox(string selectedOrderStatus, string searchValue)
        {
            string query = "SELECT co.id AS `Customer Order Id`, co.ordernumber AS `Customer Order Number`, c.`name` AS `Customer`, co.orderdate AS `Order Date`, `cos`.orderstatus AS `Order Status`, co.totalprice AS `Total Price` FROM customerorder co JOIN customer c ON co.customer = c.id JOIN customerorderstatus `cos` ON co.orderstatus = `cos`.id WHERE c.`name` LIKE @val AND `cos`.orderstatus = @OrderStatus AND co.`status` = 'Active' OR co.ordernumber LIKE @val AND `cos`.orderstatus = @OrderStatus AND co.`status` = 'Active' OR co.id LIKE @val AND `cos`.orderstatus = @OrderStatus AND co.`status` = 'Active' OR co.orderdate LIKE @val AND `cos`.orderstatus = @OrderStatus AND co.`status` = 'Active' ORDER BY co.id DESC;";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> customerorderparams = new Dictionary<string, string>()
            {
                { "@val", "%" + searchValue + "%"},
                { "@OrderStatus", selectedOrderStatus}
            };

            return upgradeManager.Load(query, customerorderparams);
        }*/

        public int VerifyCustomerOrder(string orderNumber)
        {
            int result;
            string query = "SELECT COUNT(*) FROM customerorder WHERE customerorder.ordernumber = @OrderNumber";
            upgradeManager = new UpgradeManager();
            Dictionary<string, string> verifyCustomerOrderParams = new Dictionary<string, string>()
            {
                { "@Ordernumber",  orderNumber},
            };
            result = Convert.ToInt32(upgradeManager.ExecuteScalar(query, verifyCustomerOrderParams));
            return result;
        }

        public string GetStringValue(string query, Dictionary<string, string> parameters)
        {
            upgradeManager = new UpgradeManager();
            return (upgradeManager.ExecuteScalar(query, parameters)).ToString();
        }

        //GETTING THE LAST INSERTED ORDER NUMBER VALUE FROM DATABASE USING SQL QUERY
        public string GetLastInsertedOrderNumber(string query)
        {
            upgradeManager = new UpgradeManager();
            return (upgradeManager.ExecuteScalar(query) != null && long.TryParse(upgradeManager.ExecuteScalar(query).ToString(), out long parsedResult)? parsedResult : 0).ToString();
        }

        public bool DeleteCustomerOrderData(int customerOrderId)
        {
            string query = "UPDATE customerorder SET `status` = 'Deleted' WHERE id = @id";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> customerOrderParams = new Dictionary<string, string>()
            {
                { "@id", customerOrderId.ToString() }
            };
            return upgradeManager.ExecuteQuery(query, customerOrderParams);
        }

        public bool DeleteSelectedItemData(string orderNumber, int inventoryId)
        {
            string query = "DELETE FROM customerorderdetails WHERE customerorderdetails.ordernumber = @OrderNumber AND customerorderdetails.inventoryid = @InventoryId";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> customerOrderParams = new Dictionary<string, string>()
            {
                { "@OrderNumber", orderNumber },
                { "@InventoryId", inventoryId.ToString() }
            };
            return upgradeManager.ExecuteQuery(query, customerOrderParams);
        }

        public CustomerOrder FetchCustomerOrderData(int customerorderId)//fetching data from database to form
        {
            DataTable dt = new DataTable();
            upgradeManager = new UpgradeManager();
            dt = upgradeManager.Load("SELECT * FROM dbjanmos.customerorder WHERE customerorder.id=" + customerorderId);

            if (dt.Rows.Count > 0)
            {
                CustomerOrder customerOrder = new CustomerOrder();
                foreach (DataRow row in dt.Rows)
                {
                    customerOrder.OrderNumber = row["ordernumber"].ToString();
                    customerOrder.Customer = new Customer() { Id = Convert.ToInt32(row["customer"]) };
                    customerOrder.OrderDate = row["orderdate"].ToString();
                    customerOrder.OrderStatus = new CustomerOrderStatus() { Id = Convert.ToInt32(row["orderstatus"])};
                    customerOrder.TotalPrice = row["totalprice"].ToString();
                }
                return customerOrder;
            }
            return null;
        }

        public bool Save(CustomerOrder customerOrder)//for saving data entry
        {
            string query;

            if (customerOrder.Id > 0) //for updating existing record
            {
                query = "UPDATE dbjanmos.customerorder SET ordernumber=@OrderNumber,customer=@Customer,orderstatus=@OrderStatus,orderdate=@OrderDate,totalprice=@TotalPrice,status=@Status WHERE id=@Id;";
            }
            else //for add new record
            {
                query = "INSERT INTO dbjanmos.customerorder(ordernumber,customer,orderdate,orderstatus,totalprice,status) VALUES(@OrderNumber,@Customer,@OrderDate,@OrderStatus,@TotalPrice,@Status);";
            }

            Dictionary<string, string> customerOrderParameters = new Dictionary<string, string>()
            {
                {"@Id", customerOrder.Id.ToString()},
                {"@OrderNumber", customerOrder.OrderNumber},
                {"@Customer", customerOrder.Customer.Id.ToString()},
                {"@OrderDate", customerOrder.OrderDate},
                {"@OrderStatus", customerOrder.OrderStatus.Id.ToString()},
                {"@TotalPrice", customerOrder.TotalPrice},
                {"@Status", customerOrder.Status.ToString()}
            };

            upgradeManager = new UpgradeManager();
            if (upgradeManager.ExecuteQuery(query, customerOrderParameters))
                return true;
            return false;
        }
        public DataTable LoadDataList(string query)
        {
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }
    }
}
