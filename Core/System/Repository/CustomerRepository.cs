using System.Collections.Generic;
using Core.System.Data.Model;
using System.Data;
using System;

namespace Core.System.Repository
{
    public class CustomerRepository
    {
        UpgradeManager upgradeManager;
        public DataTable LoadCustomerData()
        {
            string query = "SELECT customer.id AS `Customer Number`,customer.name AS `Customer Name`,customer.entity AS 'Entity', customer.entityname AS 'Entity Name', customer.mobilenum AS `Mobile Number`, customer.phonenum AS 'Phone Number', customer.extension AS 'Extension', customer.primaryemail AS `Email Address`, customer.socialnetid AS 'Social Network Id', customer.region AS 'Region', customer.municipality AS 'Municipality', customer.baranggay AS 'Baranggay', customer.housenum AS 'House Number', customer.postal AS 'Postal Code' FROM customer ORDER BY customer.id DESC;";
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }

        public DataTable LoadCustomerData(string searchValue)
        {
            string query = "SELECT customer.id AS `Customer Number`,customer.name AS `Customer Name`,customer.entity AS 'Entity', customer.entityname AS 'Entity Name', customer.mobilenum AS `Mobile Number`, customer.phonenum AS 'Phone Number', customer.extension AS 'Extension', customer.primaryemail AS `Email Address`, customer.socialnetid AS 'Social Network Id', customer.region AS 'Region', customer.municipality AS 'Municipality', customer.baranggay AS 'Baranggay', customer.housenum AS 'House Number', customer.postal AS 'Postal Code' WHERE customer.name LIKE @val ORDER BY customer.id DESC;";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> customerParams = new Dictionary<string, string>()
            {
                { "@val", searchValue + "%" }
            };

            return upgradeManager.Load(query, customerParams);
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
                    customer.Entity = row["entity"].ToString();
                    customer.EntityName = row["entityname"].ToString();
                    customer.MobileNum = row["mobilenum"].ToString();
                    customer.TeleNum = row["phonenum"].ToString();
                    customer.Extension = row["extension"].ToString();
                    customer.Email = row["primaryemail"].ToString();
                    customer.SocialNetId = row["socialnetid"].ToString();
                    customer.Region = row["region"].ToString();
                    customer.Municipality = row["municipality"].ToString();
                    customer.Baranggay = row["baranggay"].ToString();
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
                query = "UPDATE dbjanmos.customer SET name=@Name,entity=@Entity,entityname=@Entityname,mobilenum=@Mobilenum,phonenum=@Telenum,extension=@Extension,primaryemail=@Email,socialnetid=@Socialnetid,region=@Region,municipality=@Municipality,baranggay=@Baranggay,housenum=@Housenum,postal=@Postal WHERE id=@Id;";
            }
            else
            {
                query = "INSERT INTO dbjanmos.customer(name,entity,entityname,mobilenum,phonenum,extension,primaryemail,socialnetid,region,municipality,baranggay,housenum,postal) VALUES(@Name,@Entity,@Entityname,@Mobilenum,@Telenum,@Extension,@Email,@Socialnetid,@Region,@Municipality,@Baranggay,@Housenum,@Postal);";
            }

            Dictionary<string, string> customerParameters = new Dictionary<string, string>()
            {
                {"@Id", customer.Id.ToString()},
                {"@Name", customer.Name},
                {"@Entity", customer.Entity},
                {"@Entityname", customer.EntityName},
                {"@Mobilenum", customer.MobileNum},
                {"@Telenum", customer.TeleNum},
                {"@Extension", customer.Extension},
                {"@Email", customer.Email},
                {"@Socialnetid", customer.SocialNetId},
                {"@Region", customer.Region},
                {"@Municipality", customer.Municipality},
                {"@Baranggay", customer.Baranggay},
                {"@Housenum", customer.Housenum},
                {"@Postal", customer.Postal}
            };

            UpgradeManager upgradeManager = new UpgradeManager();
            if (upgradeManager.ExecuteQuery(query, customerParameters))
                return true;
            return false;
        }
    }
}