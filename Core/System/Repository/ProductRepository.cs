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
            string query = "SELECT product.id AS `Product Number`, product.name AS `Product Name`, product.description AS `Description`, category.description AS `Category`, CONCAT(metricunit.`name`, ' (',metricunit.`symbol`,')') AS `Metric Unit`, CONCAT(product.metricValue, ' (', metricunit.symbol, ')') AS `Metric Value`, product.status AS 'Status' FROM product JOIN category ON product.category = category.id JOIN metricunit ON product.metricUnit = metricunit.id WHERE status = '1' ORDER BY product.id DESC;";
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }

        public DataTable LoadProductData(string searchValue)
        {
            string query = "SELECT product.id AS `Product Number`, product.name AS `Product Name`, product.description AS `Description`, category.description AS `Category`, CONCAT(metricunit.`name`, ' (',metricunit.`symbol`,')') AS `Metric Unit`, CONCAT(product.metricValue, ' (', metricunit.symbol, ')') AS `Metric Value`, product.status AS 'Status' FROM product JOIN category ON product.category = category.id JOIN metricunit ON product.metricUnit = metricunit.id WHERE product.name LIKE @val ORDER BY product.id DESC;";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> productParams = new Dictionary<string, string>()
            { 
                { "@val", "%" + searchValue + "%" }
            };
            
            return upgradeManager.Load(query, productParams);
        }

        public bool DeleteProductData(int productId)
        {
            string query = "UPDATE product SET `status` = '0' WHERE id = @id";
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
            this.upgradeManager = new UpgradeManager();
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
                    product.Status = new Product.status();
                }
                return product;
            }
            return null;
        }

        public DataTable LoadDataList(string query)
        {
            this.upgradeManager = new UpgradeManager();
            return this.upgradeManager.Load(query);
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
                {"@Status", product.Status.ToString()}
            };

            UpgradeManager upgradeManager = new UpgradeManager();
            if (upgradeManager.ExecuteQuery(query, productParameters))
                return true;
            return false;
        }
    }
}