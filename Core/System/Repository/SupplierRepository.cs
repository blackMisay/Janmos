using Core.System.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.System.Repository
{
    public class SupplierRepository
    {
        UpgradeManager upgradeManager;

        public DataTable LoadSupplierData()
        {
            string query = "SELECT supplier.id AS `Id`, supplier.`name` AS `Supplier Name`, suppliercategory.`name` AS `Category`, supplier.contactperson AS `Contact Person`, supplier.socialnetid AS `Social Network Id`, supplier.mobilenum AS `Mobile Number`, supplier.phonenum AS `Phone Number`, supplier.extension AS `Extension`, supplier.email AS `Email`, CONCAT(region.`name`, '-', province.`name`, '-', municipality.`name`, '-', baranggay.`name`) AS `Address`, supplier.housenum AS `House Number`, supplier.postal AS `Postal Code` FROM supplier JOIN suppliercategory ON supplier.category = suppliercategory.id JOIN region ON supplier.region = region.id JOIN province ON supplier.province = province.id JOIN municipality ON supplier.municipality = municipality.id JOIN baranggay ON supplier.baranggay = baranggay.id WHERE supplier.`status` = '1' ORDER BY supplier.id DESC;";
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }

        public DataTable LoadSupplierData(string searchValue)
        {
            string query = "SELECT supplier.id AS `Id`, supplier.`name` AS `Supplier Name`, suppliercategory.`name` AS `Category`, supplier.contactperson AS `Contact Person`, supplier.socialnetid AS `Social Network Id`, supplier.mobilenum AS `Mobile Number`, supplier.phonenum AS `Phone Number`, supplier.extension AS `Extension`, supplier.email AS `Email`, CONCAT(region.`name`, '-', province.`name`, '-', municipality.`name`, '-', baranggay.`name`) AS `Address`, supplier.housenum AS `House Number`, supplier.postal AS `Postal Code` FROM supplier JOIN suppliercategory ON supplier.category = suppliercategory.id JOIN region ON supplier.region = region.id JOIN province ON supplier.province = province.id JOIN municipality ON supplier.municipality = municipality.id JOIN baranggay ON supplier.baranggay = baranggay.id WHERE supplier.`name` LIKE @val AND supplier.`status` = '1' ORDER BY supplier.id DESC;";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> supplierParams = new Dictionary<string, string>()
            {
                { "@val", "%" + searchValue + "%" }
            };

            return upgradeManager.Load(query, supplierParams);
        }

        public bool DeleteSupplierData(int supplierId)
        {
            string query = "UPDATE supplier SET `status` = '0' WHERE id = @id";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> supplierParams = new Dictionary<string, string>()
            {
                {"@id", supplierId.ToString() }
            };
            return upgradeManager.ExecuteQuery(query, supplierParams);
        }

        public Supplier FetchSupplierData(int supplierId)
        {
            DataTable dt = new DataTable();
            this.upgradeManager = new UpgradeManager();
            dt = upgradeManager.Load("SELECT * FROM dbjanmos.supplier WHERE supplier.id=" + supplierId);

            if (dt.Rows.Count > 0)
            {
                Supplier supplier = new Supplier();
                foreach (DataRow row in dt.Rows)
                {
                    supplier.Name = row["name"].ToString();
                    supplier.Category = new SupplierCategory() { Id = Convert.ToInt32(row["category"]) };
                    supplier.Contactperson = row["contactperson"].ToString();
                    supplier.Socialnetid = row["socialnetid"].ToString();
                    supplier.Mobilenum = row["mobilenum"].ToString();
                    supplier.Phonenum = row["phonenum"].ToString();
                    supplier.Extension = row["extension"].ToString();
                    supplier.Email = row["email"].ToString();
                    supplier.Region = new Region() { Id = Convert.ToInt32(row["region"]) };
                    supplier.Province = new Province() { Id = Convert.ToInt32(row["province"])};
                    supplier.Municipality = new Municipality() { Id = Convert.ToInt32(row["municipality"]) };
                    supplier.Baranggay = new Baranggay() { Id = Convert.ToInt32(row["baranggay"]) };
                    supplier.Housenum = row["housenum"].ToString();
                    supplier.Postal = row["postal"].ToString();
                }
                return supplier;
            }
            return null;
        }

        public DataTable LoadDataList(string query)
        {
            this.upgradeManager = new UpgradeManager();
            return this.upgradeManager.Load(query);
        }

        public bool Save(Supplier supplier)
        {
            string query;

            if (supplier.Id > 0)
            {
                query = "UPDATE dbjanmos.supplier SET name=@Name, category=@Category, contactperson=@Contactperson, socialnetid=@Socialnetid, mobilenum=@Mobilenum, phonenum=@Phonenum, extension=@Extension, email=@Email, region=@Region, province=@Province, municipality=@Municipality, baranggay=@Baranggay, housenum=@Housenum, postal=@Postal WHERE id=@Id;";
            }
            else
            {
                query = "INSERT INTO dbjanmos.supplier(name, category, contactperson, socialnetid, mobilenum, phonenum, extension, email, region, province, municipality, baranggay, housenum, postal) VALUES(@Name, @Category, @ContactPerson, @SocialNetId, @Mobilenum, @Phonenum, @Extension, @Email, @Region, @Province, @Municipality, @Baranggay, @Housenum, @Postal);";
            }

            Dictionary<string, string> supplierParameters = new Dictionary<string, string>()
            {
                {"@Id", supplier.Id.ToString()},
                {"@Name", supplier.Name},
                {"@Category", supplier.Category.Id.ToString()},
                {"@Contactperson", supplier.Contactperson},
                {"@Socialnetid", supplier.Socialnetid},
                {"@Mobilenum", supplier.Mobilenum},
                {"@Phonenum", supplier.Phonenum},
                {"@Extension", supplier.Extension},
                {"@Email", supplier.Email},
                {"@Region", supplier.Region.Id.ToString()},
                {"@Province", supplier.Province.Id.ToString()},
                {"@Municipality", supplier.Municipality.Id.ToString()},
                {"@Baranggay", supplier.Baranggay.Id.ToString()},
                {"@Housenum", supplier.Housenum},
                {"@Postal", supplier.Postal},
            };

            UpgradeManager upgradeManager = new UpgradeManager();
            if (upgradeManager.ExecuteQuery(query, supplierParameters))
                return true;
            return false;
        }
    }
}
