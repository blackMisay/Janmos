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

        public DataTable LoadCustomerOrderData()//for record load
        {
            string query = "SELECT customerorder.id AS `ID`, customer.`name` AS `Customer Name`, customerorder.orderdate AS `Date of Order`, customerorder.`status` AS `Status`, customerorder.totalprice AS `Total Price` FROM customerorder JOIN customer ON customerorder.customer = customer.id ORDER BY customerorder.id DESC;";
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }
        public DataTable LoadCustomerOrderData(string searchValue)//for record load while searching
        {
            string query = "SELECT customerorder.id AS `ID`, customer.`name` AS `Customer Name`, customerorder.orderdate AS `Date of Order`, customerorder.`status` AS `Status`, customerorder.totalprice AS `Total Price` FROM customerorder JOIN customer ON customerorder.customer = customer.id WHERE customer.`name` LIKE @val ORDER BY customerorder.id DESC;";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> customerorderParams = new Dictionary<string, string>()
            {
                { "@val", "%" + searchValue + "%" }
            };

            return upgradeManager.Load(query, customerorderParams);
        }
        public DataTable LoadDataList(string query)//load data list
        {
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
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
                    customerOrder.Customer = new Customer() { Id = Convert.ToInt32(row["customer"]) };
                    customerOrder.OrderDate = row["orderdate"].ToString();
                    customerOrder.Status = new StatusType();
                    customerOrder.TotalPrice = decimal.Parse(row["totalprice"].ToString());
                }
                return customerOrder;
            }
            return null;
        }
        public bool Save(CustomerOrder customerOrder)//saving data entry
        {
            string query;

            if (customerOrder.Id > 0) //for updating existing record
            {
                query = "UPDATE dbjanmos.customerorder SET customer=@Customer,status=@Status,orderdate=@OrderDate,totalprice=@TotalPrice WHERE id=@Id;";
            }
            else //for add new record
            {
                query = "INSERT INTO dbjanmos.customerorder(customer,status,orderdate,totalprice) VALUES(@Customer,@Status,@OrderDate,@TotalPrice);";
            }

            Dictionary<string, string> inventoryParameters = new Dictionary<string, string>()
            {
                {"@Id", customerOrder.Id.ToString()},
                {"@Customer", customerOrder.Customer.Id.ToString()},
                {"@Status", customerOrder.Status.ToString()},
                {"@OrderDate", customerOrder.OrderDate},
                {"@TotalPrice", customerOrder.TotalPrice.ToString()}
            };

            upgradeManager = new UpgradeManager();
            if (upgradeManager.ExecuteQuery(query, inventoryParameters))
                return true;
            return false;
        }
    }
}
