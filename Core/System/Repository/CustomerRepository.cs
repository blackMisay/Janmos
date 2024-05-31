using System.Collections.Generic;
using Core.System.Data.Model;
using System.Data;
using System;
using System.Data.SqlClient;

namespace Core.System.Repository
{
    public class CustomerRepository
    {
        UpgradeManager upgradeManager;

        public DataTable LoadCustomerData()
        {
            string query = "SELECT customer.id AS `Customer Number`, customer.`name` AS `Customer Name`, CONCAT(customer.entity, '(', customer.entityname, ')') AS `Entity`, customer.mobilenum AS `Mobile Number`, CONCAT(customer.phonenum, ' Ext. ', customer.extension) AS `Phone Number`, customer.primaryemail AS `Email Address`, customer.socialnetid AS 'Social Network Id', CONCAT(customer.housenum, ', ', baranggay.`name`, ', ', municipality.`name`, ', ', province.`name`, ', ', region.`name`, ', ', customer.postal) AS 'Address' FROM customer JOIN region ON customer.region = region.id JOIN province ON customer.province = province.id JOIN municipality ON customer.municipality = municipality.id JOIN baranggay ON customer.baranggay = baranggay.id WHERE customer.`status` = 'Active' ORDER BY customer.id DESC;";
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }

        public DataTable LoadCustomerData(string searchValue)
        {
            string query = "SELECT customer.id AS `Customer Number`, customer.`name` AS `Customer Name`, CONCAT(customer.entity, '(', customer.entityname, ')') AS `Entity`, customer.mobilenum AS `Mobile Number`, CONCAT(customer.phonenum, ' Ext. ', customer.extension) AS `Phone Number`, customer.primaryemail AS `Email Address`, customer.socialnetid AS 'Social Network Id', CONCAT(customer.housenum, ', ', baranggay.`name`, ', ', municipality.`name`, ', ', province.`name`, ', ', region.`name`, ', ', customer.postal) AS 'Address' FROM customer JOIN region ON customer.region = region.id JOIN province ON customer.province = province.id JOIN municipality ON customer.municipality = municipality.id JOIN baranggay ON customer.baranggay = baranggay.id WHERE customer.name LIKE @val AND customer.`status` = 'Deleted' ORDER BY customer.id DESC;";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> customerParams = new Dictionary<string, string>()
            {
                { "@val", searchValue + "%" }
            };

            return upgradeManager.Load(query, customerParams);
        }

        public bool DeleteCustomerData(int customerId)
        {
            string query = "UPDATE customer SET `status` = '1' WHERE id = @id";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> customerParams = new Dictionary<string, string>()
            {
                {"@id", customerId.ToString() }
            };
            return upgradeManager.ExecuteQuery(query, customerParams);
        }

        public Customer FetchCustomerData(int customerId)
        {
            DataTable dt = new DataTable();
            this.upgradeManager = new UpgradeManager();
            dt = upgradeManager.Load("SELECT * FROM dbjanmos.customer WHERE customer.id=" + customerId);

            if (dt.Rows.Count > 0)
            {
                Customer customer = new Customer();
                foreach (DataRow row in dt.Rows)
                {
                    customer.Name = row["name"].ToString();
                    customer.Entity = new Entity();
                    customer.Entityname = row["entityname"].ToString();
                    customer.Mobilenum = row["mobilenum"].ToString();
                    customer.Telenum = row["phonenum"].ToString();
                    customer.Extension = row["extension"].ToString();
                    customer.Email = row["primaryemail"].ToString();
                    customer.Socialnetid = row["socialnetid"].ToString();
                    customer.Region = new Region() { Id = Convert.ToInt32(row["region"]) };
                    customer.Province = new Province() { Id = Convert.ToInt32(row["province"]) };
                    customer.Municipality = new Municipality() { Id = Convert.ToInt32(row["municipality"]) };
                    customer.Baranggay = new Baranggay() { Id = Convert.ToInt32(row["baranggay"]) };
                    customer.Housenum = row["housenum"].ToString();
                    customer.Postal = row["postal"].ToString();
                }
                return customer;
            }
            return null;
        }

        public DataTable LoadDataList(string query)
        {
            this.upgradeManager = new UpgradeManager();
            return this.upgradeManager.Load(query);
        }

        public bool Save(Customer customer)
        {
            string query;

            if (customer.Id > 0)
            {
                query = "UPDATE dbjanmos.customer SET name=@Name, entity=@Entity, entityname=@Entityname, mobilenum=@Mobilenum, phonenum=@Telenum, extension=@Extension, primaryemail=@Email, socialnetid=@Socialnetid, region=@Region, province=@Province, municipality=@Municipality, baranggay=@Baranggay, housenum=@Housenum, postal=@Postal, status=@Status WHERE id=@Id;";
            }
            else
            {
                query = "INSERT INTO dbjanmos.customer(name, entity, entityname, mobilenum, phonenum, extension, primaryemail, socialnetid, region, province, municipality, baranggay, housenum, postal, status) VALUES(@Name, @Entity, @Entityname, @Mobilenum, @Telenum, @Extension, @Email, @Socialnetid, @Region, @Province, @Municipality, @Baranggay, @Housenum, @Postal, @Status);";
            }

            Dictionary<string, string> customerParameters = new Dictionary<string, string>()
            {
                {"@Id", customer.Id.ToString()},
                {"@Name", customer.Name},
                {"@Entity", customer.Entity.ToString()},
                {"@Entityname", customer.Entityname},
                {"@Mobilenum", customer.Mobilenum},
                {"@Telenum", customer.Telenum},
                {"@Extension", customer.Extension},
                {"@Email", customer.Email},
                {"@Socialnetid", customer.Socialnetid},
                {"@Region", customer.Region.Id.ToString()},
                {"@Province", customer.Province.Id.ToString()},
                {"@Municipality", customer.Municipality.Id.ToString()},
                {"@Baranggay", customer.Baranggay.Id.ToString()},
                {"@Housenum", customer.Housenum},
                {"@Postal", customer.Postal},
                {"@Status", customer.Status.ToString()}
            };

            UpgradeManager upgradeManager = new UpgradeManager();
            if (upgradeManager.ExecuteQuery(query, customerParameters))
                return true;
            return false;
        }
    }
}