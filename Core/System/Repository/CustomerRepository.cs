using Core.System.Data.Model;
using Core.System.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Core.System.Repository
{
    public class CustomerRepository
    {
        UpgradeManager upgradeManager;
        private User user = new User();
        public DataTable LoadCustomerData()
        {
            string query = "SELECT c.id AS `Customer ID`, c.`name` AS `Customer Name`, CONCAT(c.entity, ', ', c.entityname) AS `Entity`, c.mobilenum AS `Mobile Number`, CONCAT(c.phonenum, ' Ext. ', c.extension) AS `Phone Number`, c.primaryemail AS `Email Address`, c.socialnetid AS 'Social Network Id', CONCAT(c.housenum, ', ', b.`name`, ', ', m.`name`, ', ', p.`name`, ', ', r.`name`, ', ', c.postal) AS 'Address', CONCAT(ro.roletitle, ' - ', ui.givenname, ' ', ui.lastname) AS `Created By`, c.createddate AS `Created Date` FROM customer c JOIN region r ON c.regionID = r.id JOIN province p ON c.provinceID = p.id JOIN municipality m ON c.municipalityID = m.id JOIN baranggay b ON c.baranggayID = b.id JOIN `user` u ON c.createdby = u.id JOIN roles ro ON u.roleid = ro.id JOIN userinfo ui ON u.userinfoid = ui.id WHERE c.`status` = 'Active' ORDER BY c.id DESC;";
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }

        public DataTable LoadCustomerData(string searchValue)
        {
            string query = "SELECT c.id AS `Customer ID`, c.`name` AS `Customer Name`, CONCAT(c.entity, ', ', c.entityname) AS `Entity`, c.mobilenum AS `Mobile Number`, CONCAT(c.phonenum, ' Ext. ', c.extension) AS `Phone Number`, c.primaryemail AS `Email Address`, c.socialnetid AS 'Social Network Id', CONCAT(c.housenum, ', ', b.`name`, ', ', m.`name`, ', ', p.`name`, ', ', r.`name`, ', ', c.postal) AS 'Address', CONCAT(ro.roletitle, ' - ', ui.givenname, ' ', ui.lastname) AS `Created By`, c.createddate AS `Created Date` FROM customer c JOIN region r ON c.regionID = r.id JOIN province p ON c.provinceID = p.id JOIN municipality m ON c.municipalityID = m.id JOIN baranggay b ON c.baranggayID = b.id JOIN `user` u ON c.createdby = u.id JOIN roles ro ON u.roleid = ro.id JOIN userinfo ui ON u.userinfoid = ui.id WHERE c.`status` = 'Active' AND(c.`name` LIKE @val OR c.id LIKE @val OR c.entity LIKE @val OR c.entityname LIKE @val OR c.mobilenum LIKE @val OR c.phonenum LIKE @val OR CONCAT(c.housenum, ', ', b.`name`, ', ', m.`name`, ', ', p.`name`, ', ', r.`name`, ', ', c.postal) LIKE @val OR CONCAT(ui.givenname, ' ', ui.lastname) LIKE @val) ORDER BY c.id DESC;";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> customerParams = new Dictionary<string, string>()
            {
                { "@val", searchValue + "%" }
            };

            return upgradeManager.Load(query, customerParams);
        }

        public bool DeleteCustomerData(int customerId)
        {
            string query = "UPDATE customer SET `status` = 'Deleted' WHERE id = @id";
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
                    customer.Entity = (Entity)Enum.Parse(typeof(Entity), row["entity"].ToString());
                    customer.Entityname = row["entityname"].ToString();
                    customer.Mobilenum = row["mobilenum"].ToString();
                    customer.Telenum = row["phonenum"].ToString();
                    customer.Extension = row["extension"].ToString();
                    customer.Email = row["primaryemail"].ToString();
                    customer.Socialnetid = row["socialnetid"].ToString();
                    customer.Region = new Region() { Id = Convert.ToInt32(row["regionID"]) };
                    customer.Province = new Province() { Id = Convert.ToInt32(row["provinceID"]) };
                    customer.Municipality = new Municipality() { Id = Convert.ToInt32(row["municipalityID"]) };
                    customer.Baranggay = new Baranggay() { Id = Convert.ToInt32(row["baranggayID"]) };
                    customer.Housenum = row["housenum"].ToString();
                    customer.Postal = row["postal"].ToString();
                    customer.CreatedBy = new User() { Id = Convert.ToInt32(row["createdby"]) };
                    customer.CreatedDate = row["createddate"].ToString();
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

        public bool Save(AuditLog log, Customer updCustomer, User user)
        {
            this.user = user;
            string query;

            if (updCustomer.Id > 0)
            {
                query = "UPDATE dbjanmos.customer SET name=@Name, entity=@Entity, entityname=@Entityname, mobilenum=@Mobilenum, phonenum=@Telenum, extension=@Extension, primaryemail=@Email, socialnetid=@Socialnetid, regionID=@Region, provinceID=@Province, municipalityID=@Municipality, baranggayID=@Baranggay, housenum=@Housenum, postal=@Postal, createdby=@UserId, createddate=@CreatedDate, status=@Status WHERE id=@Id;";
            }
            else
            {
                query = "INSERT INTO dbjanmos.customer(name, entity, entityname, mobilenum, phonenum, extension, primaryemail, socialnetid, regionID, provinceID, municipalityID, baranggayID, housenum, postal, createdby, createddate, status) VALUES(@Name, @Entity, @Entityname, @Mobilenum, @Telenum, @Extension, @Email, @Socialnetid, @Region, @Province, @Municipality, @Baranggay, @Housenum, @Postal, @UserId, @CreatedDate, @Status);";
            }

            Dictionary<string, string> customerParameters = new Dictionary<string, string>()
            {
                {"@Id", updCustomer.Id.ToString()},
                {"@Name", updCustomer.Name},
                {"@Entity", updCustomer.Entity.ToString()},
                {"@Entityname", updCustomer.Entityname},
                {"@Mobilenum", updCustomer.Mobilenum},
                {"@Telenum", updCustomer.Telenum},
                {"@Extension", updCustomer.Extension},
                {"@Email", updCustomer.Email},
                {"@Socialnetid", updCustomer.Socialnetid},
                {"@Region", updCustomer.Region.Id.ToString()},
                {"@Province", updCustomer.Province.Id.ToString()},
                {"@Municipality", updCustomer.Municipality.Id.ToString()},
                {"@Baranggay", updCustomer.Baranggay.Id.ToString()},
                {"@Housenum", updCustomer.Housenum},
                {"@Postal", updCustomer.Postal},
                {"@UserId", updCustomer.CreatedBy.Id.ToString()},
                {"@CreatedDate", updCustomer.CreatedDate },
                {"@Status", updCustomer.Status.ToString()}
            };

            UpgradeManager upgradeManager = new UpgradeManager();
            if (updCustomer.Id > 0)
            {
                if (upgradeManager.ExecuteQuery(query, customerParameters))
                {
                    AuditManager.Log(log.UserId, log.ActionType, log.TableName, log.RecordId, log.OldValue, log.NewValue, log.Description);
                    return true;
                }
                return false;
            }
            else
            {
                int newId = upgradeManager.ExecuteQuery(query, customerParameters, true);
                if (newId == 0)
                    return false;
                AuditManager.Log(this.user, ActionType.CREATE, tableName: "Customer", recordId: newId, description: "The user added new customer '" + updCustomer.Name + "'.");
                return true;
            }
        }
    }
}