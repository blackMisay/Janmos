using Core.System.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Repository
{
    public class CustomerOrderDetailsRepository
    {
        UpgradeManager upgradeManager;

        public DataTable LoadCustomerOrderDetailsData(int customerOrderID)//for record load
        {
            string query = "SELECT product.`name` AS `Product Name`, customerorderdetails.quantity AS `Quantity`, customerorderdetails.unitprice AS `Unit Price`, customerorderdetails.totalprice AS `Total Price` FROM customerorderdetails JOIN customerorder ON customerorderdetails.customerorder = customerorder.id JOIN product ON customerorderdetails.product = product.id WHERE customerorder.id = "+customerOrderID+" ORDER BY customerorderdetails.id DESC;";
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }
        public DataTable LoadDataList(string query)//load data list
        {
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }

        public int ExecuteScalar(string query)
        {
            upgradeManager = new UpgradeManager();
            return upgradeManager.ExecuteScalar(query);
        }

        public CustomerOrderDetails FetchCustomerOrderDetailsData(int customerOrderID)//fetching data from database to form
        {
            List<CustomerOrderDetails> customerOrderList;
            DataTable dt = new DataTable();
            upgradeManager = new UpgradeManager();
            dt = upgradeManager.Load("SELECT * FROM dbjanmos.customerorderdetails JOIN customerorder ON customerorderdetails.customerorder = customerorder.id WHERE customerorder.id=" + customerOrderID);

            if (dt.Rows.Count > 0)
            {
                CustomerOrderDetails customerOrderDetails = new CustomerOrderDetails();
                foreach (DataRow row in dt.Rows)
                {

                    customerOrderDetails.CustomerOrderID = new CustomerOrder() { Id = Convert.ToInt32(row["customerorder"]) };
                    customerOrderDetails.ProductID = new Product() { Id = Convert.ToInt32(row["product"]) };
                    customerOrderDetails.Quantity = Convert.ToInt32(row["quantity"]);
                    customerOrderDetails.UnitPrice = Convert.ToInt32(row["unitprice"]);
                    customerOrderDetails.TotalPrice = Convert.ToInt32(row["totalprice"]);

                    //customerOrderList.Add(customerOrderDetails);
                }
                return customerOrderDetails;
            }
            return null;
        }

        public bool Save(CustomerOrderDetails customerOrderDetails)//saving data entry
        {
            string query;

            if (customerOrderDetails.Id > 0) //for updating existing record
            {
                query = "UPDATE dbjanmos.customerorderdetails SET customerorder=@CustomerOrder,product=@Product,quantity=@Quantity,unitprice=@UnitPrice,totalprice=@TotalPrice WHERE id=@Id;";
            }
            else //for add new record
            {
                query = "INSERT INTO dbjanmos.customerorderdetails(customerorder,product,quantity,unitprice,totalprice) VALUES(@CustomerOrder,@Product,@Quantity,@UnitPrice,@TotalPrice);";
            }

            Dictionary<string, string> customerOrderDetailsParameters = new Dictionary<string, string>()
            {
                {"@Id", customerOrderDetails.Id.ToString()},
                {"@CustomerOrder", customerOrderDetails.CustomerOrderID.Id.ToString()},
                {"@Product", customerOrderDetails.ProductID.Id.ToString()},
                {"@Quantity", customerOrderDetails.Quantity.ToString()},
                {"@UnitPrice", customerOrderDetails.UnitPrice.ToString()},
                {"@TotalPrice", customerOrderDetails.TotalPrice.ToString()}
            };

            upgradeManager = new UpgradeManager();
            if (upgradeManager.ExecuteQuery(query, customerOrderDetailsParameters))
                return true;
            return false;
        }

        public int InsertAndGetId(CustomerOrderDetails customerOrderDetails)
        {
            string query = "INSERT INTO dbjanmos.customerorderdetails(customerorder,product,quantity,unitprice,totalprice) VALUES(@CustomerOrder,@Product,@Quantity,@UnitPrice,@TotalPrice);";
            Dictionary<string, string> customerOrderDetailsData = new Dictionary<string, string>
            {
                { "@CustomerOrder", customerOrderDetails.CustomerOrderID.Id.ToString() },
                { "@Product", customerOrderDetails.ProductID.Id.ToString() },
                { "@Quantity", customerOrderDetails.Quantity.ToString() },
                { "@UnitPrice", customerOrderDetails.UnitPrice.ToString() },
                { "@TotalPrice", customerOrderDetails.TotalPrice.ToString() }
            };
            return upgradeManager.InsertAndGetId(query, customerOrderDetailsData);
        }
    }
}
