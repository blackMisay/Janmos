using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Repository
{
    public class CustomerBranchRepository
    {
        UpgradeManager upgradeManager; //For Manager Connection

        //CUSTOMER BRANCH RECORD LOAD
        public DataTable LoadCustomerBranchData()
        {
            string query = "SELECT customer.id AS `Customer ID`, customer.`name` AS `Customer Name`, CONCAT(customer.entity, '(', customer.entityname, ')') AS `Entity`, customer.mobilenum AS `Mobile Number`, CONCAT(customer.phonenum, ' Ext. ', customer.extension) AS `Phone Number`, customer.primaryemail AS `Email`, customer.socialnetid AS `Social Net ID`, CONCAT(customer.housenum, ', ', baranggay.`name`, ', ', municipality.`name`, ', ', province.`name`, ', ', region.`name`, ', ', customer.postal) AS `Address` FROM customer JOIN region ON customer.regionID = region.id JOIN province ON customer.provinceID = province.id JOIN municipality ON customer.municipalityID = municipality.id JOIN baranggay ON customer.baranggayID = baranggay.id WHERE customer.`status` = 'Active' ORDER BY customer.id DESC;";
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }

        //CUSTOMER BRANCH RECORD LOAD WHILE SEARCHING
        public DataTable LoadCustomerBranchData(string searchValue)
        {
            string query = "SELECT customer.id AS `Customer ID`, customer.`name` AS `Customer Name`, CONCAT(customer.entity, '(', customer.entityname, ')') AS `Entity`, customer.mobilenum AS `Mobile Number`, CONCAT(customer.phonenum, ' Ext. ', customer.extension) AS `Phone Number`, customer.primaryemail AS `Email`, customer.socialnetid AS `Social Net ID`, CONCAT(customer.housenum, ', ', baranggay.`name`, ', ', municipality.`name`, ', ', province.`name`, ', ', region.`name`, ', ', customer.postal) AS `Address` FROM customer JOIN region ON customer.regionID = region.id JOIN province ON customer.provinceID = province.id JOIN municipality ON customer.municipalityID = municipality.id JOIN baranggay ON customer.baranggayID = baranggay.id WHERE customer.`name` LIKE @val AND customer.`status` = 'Active' ORDER BY customer.id DESC;";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> inventoryParams = new Dictionary<string, string>()
            {
                { "@val", "%" + searchValue + "%" }
            };

            return upgradeManager.Load(query, inventoryParams);
        }
    }
}
