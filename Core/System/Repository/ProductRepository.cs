using System.Collections.Generic;
using Core.System.Data.Model;
using System.Data;
using System;

namespace Core.System.Repository
{
    public class ProductRepository
    {
        UpgradeManager upgradeManager;
        public DataTable LoadProductData()
        {
            string query = "SELECT p.id AS `Product Id`, p.`name` AS `Product Name`, p.`description` AS `Description`, c.`description` AS `Category`, CONCAT('(',mu.symbol, ') ', mu.`name`) AS `Unit Measurement`, CONCAT(p.metricValue, ' ', mu.symbol) AS `Metric Value` FROM product p JOIN category c ON p.category = c.id JOIN metricunit mu ON p.metricUnit = mu.id WHERE p.`status` = 'Active' ORDER BY p.id DESC;";

            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }
        public DataTable LoadProductData(int selectedCategoryId)
        {
            string query = "SELECT p.id AS `Product Id`, p.`name` AS `Product Name`, p.`description` AS `Description`, c.`description` AS `Category`, CONCAT('(',mu.symbol, ') ', mu.`name`) AS `Unit Measurement`, CONCAT(p.metricValue, ' ', mu.symbol) AS `Metric Value` FROM product p JOIN category c ON p.category = c.id JOIN metricunit mu ON p.metricUnit = mu.id WHERE c.id = @CategoryId AND p.`status` = 'Active' ORDER BY p.id DESC;";

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
            string query = "SELECT p.id AS `Product Id`, p.`name` AS `Product Name`, p.`description` AS `Description`, c.`description` AS `Category`, CONCAT('(',mu.symbol, ') ', mu.`name`) AS `Unit Measurement`, CONCAT(p.metricValue, ' ', mu.symbol) AS `Metric Value` FROM product p JOIN category c ON p.category = c.id JOIN metricunit mu ON p.metricUnit = mu.id WHERE p.id LIKE @val AND p.`status` = 'Active' OR p.`name` LIKE @val AND p.`status` = 'Active' OR p.`description` LIKE @val AND p.`status` = 'Active' OR c.`description` LIKE @val AND p.`status` = 'Active' OR mu.`name` LIKE @val AND p.`status` = 'Active' ORDER BY p.id DESC;";
            Dictionary<string, string> productParams = new Dictionary<string, string>()
                {
                    { "@val", "%" + searchValue + "%" }
                };

            return upgradeManager.Load(query, productParams);
        }
        public DataTable LoadProductDataViaSearch(string searchValue, int selectedCategoryId)
        {
            upgradeManager = new UpgradeManager();
            string query = "SELECT p.id AS `Product Id`, p.`name` AS `Product Name`, p.`description` AS `Description`, c.`description` AS `Category`, CONCAT('(',mu.symbol, ') ', mu.`name`) AS `Unit Measurement`, CONCAT(p.metricValue, ' ', mu.symbol) AS `Metric Value` FROM product p JOIN category c ON p.category = c.id JOIN metricunit mu ON p.metricUnit = mu.id WHERE p.id LIKE @val AND c.id = @CategoryId AND p.`status` = 'Active' OR p.`name` LIKE @val AND c.id = @CategoryId AND p.`status` = 'Active' OR p.`description` LIKE @val AND c.id = @CategoryId AND p.`status` = 'Active' OR c.`description` LIKE @val AND c.id = @CategoryId AND p.`status` = 'Active' OR mu.`name` LIKE @val AND c.id = @CategoryId AND p.`status` = 'Active' ORDER BY p.id DESC;";
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

        public bool Save(Product product)
        {
            string query;

            if (product.Id > 0)
            {
                query = "UPDATE dbjanmos.product SET name=@Name, description=@Description, category=@Category, metricUnit=@MetricUnit, metricValue=@MetricValue, status=@Status WHERE id=@Id;";
            }
            else
            {
                query = "INSERT INTO dbjanmos.product(name, description, category, metricUnit, metricValue, status) VALUES(@Name, @Description, @Category, @MetricUnit, @MetricValue, @Status);";
            }

            Dictionary<string, string> productParameters = new Dictionary<string, string>()
            {
                {"@Id", product.Id.ToString()},
                {"@Name", product.Name},
                {"@Description", product.Description},
                {"@Category", product.Category.Id.ToString()},
                {"@MetricValue", product.MetricValue},
                {"@MetricUnit", product.MetricUnit.Id.ToString()},
                {"@Status", (product.Status.ToString())}
            };

            upgradeManager = new UpgradeManager();
            if (upgradeManager.ExecuteQuery(query, productParameters))
                return true;
            return false;
        }
    }
}