using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Repository
{
    public class SupplierOrderRepository
    {
        UpgradeManager upgradeManager;
        public DataTable LoadSupplierOrderData()//for loading data
        {
            string query = "SELECT supplierorder.id AS `Supplier Order Id`, supplier.`name` AS `Supplier Name`, supplierorder.orderdate AS `Order Date`, supplierorder.orderstatus AS `Order Status`, supplierorder.totalprice AS `Total Price` FROM supplierorder JOIN supplier ON supplierorder.supplier = supplier.`name` WHERE supplierorder.`status` = 'Active' ORDER BY supplierorder.id DESC;";
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }
        public DataTable LoadSupplierOrderData(string searchValue)//for loading data while searching
        {
            string query = "SELECT supplierorder.id AS `Supplier Order Id`, supplier.`name` AS `Supplier Name`, supplierorder.orderdate AS `Order Date`, supplierorder.orderstatus AS `Order Status`, supplierorder.totalprice AS `Total Price` FROM supplierorder JOIN supplier ON supplierorder.supplier = supplier.`name` WHERE supplierorder.id LIKE @val AND supplierorder.`status` = 'Active' OR supplier.`name` LIKE @val AND supplierorder.`status` = 'Active' OR supplierorder.orderdate LIKE @val AND supplierorder.`status` = 'Active' OR supplierorder.orderstatus LIKE @val AND supplierorder.`status` = 'Active' ORDER BY supplierorder.id DESC;";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> customerorderParams = new Dictionary<string, string>()
            {
                { "@val", "%" + searchValue + "%" }
            };

            return upgradeManager.Load(query, customerorderParams);
        }
        public bool DeleteSupplierOrderData(int supplierOrderId)
        {
            string query = "UPDATE customerorder SET `status` = 'Deleted' WHERE id = @id";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> supplierOrderParams = new Dictionary<string, string>()
            {
                { "@id", supplierOrderId.ToString() }
            };
            return upgradeManager.ExecuteQuery(query, supplierOrderParams);
        }
    }
}
