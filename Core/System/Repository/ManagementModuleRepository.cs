using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Repository
{
    public class ManagementModuleRepository
    {
        UpgradeManager manager;
        public DataTable LoadSalesChart(DateTime start, DateTime end)
        {
            string query = "SELECT co.orderdate AS `OrderDate`, SUM(co.totalprice) AS `DailySales` FROM customerorder co WHERE co.orderdate BETWEEN @StartDate AND @EndDate GROUP BY co.orderdate ORDER BY co.orderdate;";
            manager = new UpgradeManager();
            Dictionary<string, string> salesChartParams = new Dictionary<string, string>()
            {
                {"@StartDate", start.ToString("yyyy-MM-dd")},
                {"@EndDate",  end.ToString("yyyy-MM-dd")}
            };
            return manager.Load(query, salesChartParams);
        }
        public DataTable LoadFluctuationInventoryChart(DateTime start, DateTime end)
        {
            string query = "SELECT DATE(im.date_entry) AS `Date`, SUM(CASE WHEN im.transaction_type = 'IN' THEN im.quantity ELSE - im.quantity END) AS NetMovement FROM inventory_movement im WHERE im.date_entry BETWEEN @StartDate AND @EndDate GROUP BY DATE(im.date_entry) ORDER BY `Date`;";
            manager = new UpgradeManager();
            Dictionary<string, string> inventoryChartParams = new Dictionary<string, string>()
            {
                {"@StartDate", start.ToString("yyyy-MM-dd")},
                {"@EndDate",  end.ToString("yyyy-MM-dd")}
            };
            return manager.Load(query, inventoryChartParams);
        }
    }
}
