using System.Collections.Generic;
using Core.System.Data.Model;
using System.Data;
using System;
using Core.System.Security;
using Newtonsoft.Json;

namespace Core.System.Repository
{
    public class ProductRepository
    {
        UpgradeManager upgradeManager;
        private User user = new User();
        public DataTable LoadProductData()
        {
            string query = "SELECT p.id AS `Product Id`, p.`name` AS `Product Name`, p.`description` AS `Description`, c.`description` AS `Category`, CONCAT('(',mu.symbol, ') ', mu.`name`) AS `Unit Measurement`, CONCAT(p.metricValue, ' ', mu.symbol) AS `Metric Value`, p.reOrderPoint AS `Re-Order Point`, p.maxStockLevel AS `Max Stock Level`, tt.taxname AS `Tax Type`, CONCAT(r.roletitle, ' - ', ui.givenname, ' ', ui.lastname) AS `Created By`, p.createddate AS `Date Created` FROM product p JOIN category c ON p.category = c.id JOIN metricunit mu ON p.metricUnit = mu.id JOIN taxtype tt ON p.taxtypeid = tt.id JOIN `user` u ON p.createdby = u.id JOIN userinfo ui ON u.userinfoid = ui.id JOIN roles r ON u.roleid = r.id WHERE p.`status` = 'Active' ORDER BY p.id DESC;";

            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }
        public DataTable LoadProductData(int selectedCategoryId)
        {
            string query = "SELECT p.id AS `Product Id`, p.`name` AS `Product Name`, p.`description` AS `Description`, c.`description` AS `Category`, CONCAT('(',mu.symbol, ') ', mu.`name`) AS `Unit Measurement`, CONCAT(p.metricValue, ' ', mu.symbol) AS `Metric Value`, p.reOrderPoint AS `Re-Order Point`, p.maxStockLevel AS `Max Stock Level`, tt.taxname AS `Tax Type`, CONCAT(r.roletitle, ' - ', ui.givenname, ' ', ui.lastname) AS `Created By`, p.createddate AS `Date Created` FROM product p JOIN category c ON p.category = c.id JOIN metricunit mu ON p.metricUnit = mu.id JOIN `user` u ON p.createdby = u.id JOIN taxtype tt ON p.taxtypeid = tt.id JOIN roles r ON u.roleid = r.id JOIN userinfo ui ON u.userinfoid = ui.id WHERE c.id = @CategoryId AND p.`status` = 'Active' ORDER BY p.id DESC;";

            upgradeManager = new UpgradeManager();
            Dictionary<string, string> productParams = new Dictionary<string, string>()
                {
                    { "@CategoryId", selectedCategoryId.ToString() }
                };
            return upgradeManager.Load(query, productParams);
        }
        public DataTable LoadProductDataViaSearch(string searchValue)
        {
            upgradeManager = new UpgradeManager();
            string query = "SELECT p.id AS `Product Id`, p.`name` AS `Product Name`, p.`description` AS `Description`, c.`description` AS `Category`, CONCAT('(',mu.symbol, ') ', mu.`name`) AS `Unit Measurement`, CONCAT(p.metricValue, ' ', mu.symbol) AS `Metric Value`, p.reOrderPoint AS `Re-Order Point`, p.maxStockLevel AS `Max Stock Level`, tt.taxname AS `Tax Type`, CONCAT(r.roletitle, ' - ', ui.givenname, ' ', ui.lastname) AS `Created By`, p.createddate AS `Date Created` FROM product p JOIN category c ON p.category = c.id JOIN metricunit mu ON p.metricUnit = mu.id  JOIN taxtype tt ON p.taxtypeid = tt.id JOIN `user` u ON p.createdby = u.id JOIN roles r ON u.roleid = r.id JOIN userinfo ui ON u.userinfoid = ui.id WHERE p.`status` = 'Active' AND( p.id LIKE @val OR p.`name` LIKE @val OR p.`description` LIKE @val OR c.`description` LIKE @val OR mu.`name` LIKE @val OR tt.taxname LIKE @val OR CONCAT(ui.givenname, ' ', ui.lastname) LIKE @val) ORDER BY p.id DESC;";
            Dictionary<string, string> productParams = new Dictionary<string, string>()
                {
                    { "@val", "%" + searchValue + "%" }
                };

            return upgradeManager.Load(query, productParams);
        }
        public DataTable LoadProductDataViaSearch(string searchValue, int selectedCategoryId)
        {
            upgradeManager = new UpgradeManager();
            string query = "SELECT p.id AS `Product Id`, p.`name` AS `Product Name`, p.`description` AS `Description`, c.`description` AS `Category`, CONCAT('(',mu.symbol, ') ', mu.`name`) AS `Unit Measurement`, CONCAT(p.metricValue, ' ', mu.symbol) AS `Metric Value`, p.reOrderPoint AS `Re-Order Point`, p.maxStockLevel AS `Max Stock Level`, tt.taxname AS `Tax Type`, CONCAT(r.roletitle, ' - ', ui.givenname, ' ', ui.lastname) AS `Created By`, p.createddate AS `Date Created` FROM product p JOIN category c ON p.category = c.id JOIN metricunit mu ON p.metricUnit = mu.id  JOIN taxtype tt ON p.taxtypeid = tt.id JOIN `user` u ON p.createdby = u.id JOIN roles r ON u.roleid = r.id JOIN userinfo ui ON u.userinfoid = ui.id WHERE p.`status` = 'Active' AND c.id = @CategoryId AND( p.id LIKE @val OR p.`name` LIKE @val OR p.`description` LIKE @val OR c.`description` LIKE @val OR mu.`name` LIKE @val OR tt.taxname LIKE @val OR CONCAT(ui.givenname, ' ', ui.lastname) LIKE @val) ORDER BY p.id DESC;";
            Dictionary<string, string> productParams = new Dictionary<string, string>()
                {
                    { "@val", "%" + searchValue + "%"},
                    { "CategoryId", selectedCategoryId.ToString()}
                };

            return upgradeManager.Load(query, productParams);
        }

        public List<KeyValuePair<int, string>> PopulateCombobox(string query)
        {
            upgradeManager = new UpgradeManager();
            return upgradeManager.Populate(query);
        }

        public bool DeleteProductData(int productId)
        {
            string query = "UPDATE product SET `status` = 'Deleted' WHERE id = @id";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> productParams = new Dictionary<string, string>()
            {
                { "@id", productId.ToString() }
            };
            return upgradeManager.ExecuteQuery(query, productParams);
        }

        public Product FetchProductData(int productId)
        {
            DataTable dt = new DataTable();
            upgradeManager = new UpgradeManager();
            dt = upgradeManager.Load("SELECT * FROM dbjanmos.product WHERE product.id=" + productId);

            if (dt.Rows.Count > 0 )
            {
                Product product = new Product();
                foreach (DataRow row in dt.Rows)
                {
                    product.Name = row["name"].ToString();
                    product.Description = row["description"].ToString();
                    product.Category = new Category() { Id = Convert.ToInt32(row["category"]) };
                    product.MetricUnit = new MetricUnit() { Id = Convert.ToInt32(row["metricUnit"]) };
                    product.MetricValue = row["metricValue"].ToString();
                    product.ReOrderPoint = Convert.ToInt32(row["reOrderPoint"]);
                    product.MaxStockLevel = Convert.ToInt32(row["maxStockLevel"]);
                    product.TaxTypeId = new TaxType() { Id = Convert.ToInt32(row["taxtypeid"]) };
                    product.CreatedBy = new User() { Id = Convert.ToInt32(row["createdby"]) };
                    product.CreatedDate = row["createddate"].ToString();
                }
                return product;
            }
            return null;
        }

        public DataTable LoadDataList(string query)
        {
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }

        public bool Save(AuditLog log, Product updProduct, User user)
        {
            this.user = user;
            string query;

            if (updProduct.Id > 0)
            {
                query = "UPDATE dbjanmos.product SET name=@Name, description=@Description, category=@Category, metricUnit=@MetricUnit, metricValue=@MetricValue, reOrderPoint=@ReOrderPoint, maxStockLevel=@MaxStockLevel, taxtypeid=@TaxTypeId, status=@Status WHERE id=@Id;";
            }
            else
            {
                query = "INSERT INTO dbjanmos.product(name, description, category, metricUnit, metricValue, reOrderPoint, maxStockLevel, taxtypeid, createdby, createddate, status) VALUES(@Name, @Description, @Category, @MetricUnit, @MetricValue, @ReOrderPoint, @MaxStockLevel, @TaxTypeId, @UserId, @CreatedDate, @Status);";
            }

            Dictionary<string, string> productParameters = new Dictionary<string, string>()
            {
                {"@Id", updProduct.Id.ToString()},
                {"@Name", updProduct.Name},
                {"@Description", updProduct.Description},
                {"@Category", updProduct.Category.Id.ToString()},
                {"@MetricValue", updProduct.MetricValue},
                {"@MetricUnit", updProduct.MetricUnit.Id.ToString()},
                {"@ReOrderPoint", updProduct.ReOrderPoint.ToString() },
                {"@MaxStockLevel", updProduct.MaxStockLevel.ToString()},
                {"@TaxTypeId", updProduct.TaxTypeId.Id.ToString()},
                {"@UserId", updProduct.CreatedBy.Id.ToString()},
                {"@CreatedDate",  updProduct.CreatedDate},
                {"@Status", (updProduct.Status.ToString())}
            };

            upgradeManager = new UpgradeManager();
            if (updProduct.Id > 0)
            {
                if (upgradeManager.ExecuteQuery(query, productParameters))
                {
                    AuditManager.Log(log.UserId, log.ActionType, log.TableName, log.RecordId, log.OldValue, log.NewValue, log.Description);
                    return true;
                }
                return false;   
            }
            else
            {
                int newId = upgradeManager.ExecuteQuery(query, productParameters, true);
                if (newId == 0)
                    return false;
                AuditManager.Log(this.user, ActionType.CREATE, tableName: "Product", recordId: newId, description: "The user added new product '" + updProduct.Name + "'.");
                return true;
            }
        }
    }
}