using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Repository
{
    public class ReportRepository
    {
        UpgradeManager manager;

        public DataTable LoadSalesData() //Formload via selected Sales Report
        {
            string q = "SELECT s.id AS `Id`, p.`name` AS `Product Name`, c.`name` AS `Customer Name`, s.saledate AS `Sale Date`, s.quantity AS `Quantity`, s.totalprice AS `Total Price` FROM sales s JOIN product p ON s.product = p.id JOIN customer c ON s.customer = c.id ORDER BY s.id DESC;";
            manager = new UpgradeManager();
            return manager.Load(q);
        }
        public DataTable LoadSalesData(string selectedDateTimeFrom, string selectedDateTimeTo) //Formlaod via selected datetimepicker in Sales Report
        {
            string q = "SELECT s.id AS `Id`, p.`name` AS `Product Name`, c.`name` AS `Customer Name`, s.saledate AS `Sale Date`, s.quantity AS `Quantity`, s.totalprice AS `Total Price` FROM sales s JOIN product p ON s.product = p.id JOIN customer c ON s.customer = c.id WHERE s.saledate BETWEEN @DateFrom and @DateTo ORDER BY s.id DESC;";
            manager = new UpgradeManager();
            Dictionary<string, string> salesParams = new Dictionary<string, string>()
            {
                {"@DateFrom", selectedDateTimeFrom },
                {"@DateTo", selectedDateTimeTo }
            };
            return manager.Load(q, salesParams);
        }
        public DataTable LoadInventoryData()
        {
            string q = "SELECT i.id AS `Inventory Id`, p.`name` AS `Product Name`, i.`description` AS `Description`, i.price AS `Price`, i.quantity AS `Quantity`, i.entrydate AS `Entry Date`, i.expiration AS `Expiration Date`, i.availability AS `Availability`, i.`status` AS `Status` FROM inventory i JOIN product p ON i.`name` = p.id ORDER BY i.id DESC;";
            manager = new UpgradeManager();
            return manager.Load(q);
        }
        public DataTable LoadInventoryData(string selectedDateTimeFrom, string selectedDateTimeTo, string selectedFilter)
        {
            string q = "";
            if (selectedFilter == "Entry Date")
            {
                q = "SELECT i.id AS `Inventory Id`, p.`name` AS `Product Name`, i.`description` AS `Description`, i.price AS `Price`, i.quantity AS `Quantity`, i.entrydate AS `Entry Date`, i.expiration AS `Expiration Date`, i.availability AS `Availability`, i.`status` AS `Status` FROM inventory i JOIN product p ON i.`name` = p.id WHERE i.entrydate BETWEEN @DateFrom and @DateTo ORDER BY i.id DESC;";
            }
            else if (selectedFilter == "Expiration Date")
            {
                q = "SELECT i.id AS `Inventory Id`, p.`name` AS `Product Name`, i.`description` AS `Description`, i.price AS `Price`, i.quantity AS `Quantity`, i.entrydate AS `Entry Date`, i.expiration AS `Expiration Date`, i.availability AS `Availability`, i.`status` AS `Status` FROM inventory i JOIN product p ON i.`name` = p.id WHERE i.expiration BETWEEN @DateFrom and @DateTo ORDER BY i.id DESC;";
            }
            manager = new UpgradeManager();
            Dictionary<string, string> inventoryParams = new Dictionary<string, string>()
            {
                {"@DateFrom", selectedDateTimeFrom },
                {"@DateTo", selectedDateTimeTo }
            };
            return manager.Load(q, inventoryParams);
        }
    }
}
