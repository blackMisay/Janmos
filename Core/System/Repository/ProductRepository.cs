using System.Collections.Generic;
using Core.System.Data.Model;
using System.Data;
using System;
using System.Windows.Forms;

namespace Core.System.Repository
{
    public class ProductRepository
    {
        UpgradeManager upgradeManager;
        public DataTable LoadProductData(int pageSize)
        {
            string query = "SELECT product.id AS `Product Number`,product.name AS `Product Name`,product.description AS `Description`,category.description AS `Category`, CONCAT(metricunit.`name`, ' (',metricunit.`symbol`,')') AS `Metric Unit`, CONCAT(product.metricValue, ' (', metricunit.symbol, ')') AS `Metric Value` FROM product JOIN category ON product.category = category.id JOIN metricunit ON product.metricUnit = metricunit.id WHERE product.`status` = '0' ORDER BY product.id DESC LIMIT "+pageSize;
            upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }

        public DataTable LoadProductData(string searchValue)
        {
            string query = "SELECT product.id AS `Product Number`,product.name AS `Product Name`,product.description AS `Description`,category.description AS `Category`, CONCAT(metricunit.`name`, ' (',metricunit.`symbol`,')') AS `Metric Unit`, CONCAT(product.metricValue, ' (', metricunit.symbol, ')') AS `Metric Value` FROM product JOIN category ON product.category = category.id JOIN metricunit ON product.metricUnit = metricunit.id WHERE product.name LIKE @val ORDER BY product.id DESC;";
            upgradeManager = new UpgradeManager();

            Dictionary<string, string> productParams = new Dictionary<string, string>()
            { 
                { "@val", "%" + searchValue + "%" }
            };
            
            return upgradeManager.Load(query, productParams);
        }
        public int GetProductCount()
        {
            string query = "SELECT COUNT(*) AS 'totalcount' FROM product WHERE product.`status` = '0'";
            upgradeManager = new UpgradeManager();
            return upgradeManager.GetTotalCount(query);
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
                query = "UPDATE dbjanmos.product SET name=@Name,description=@Description,category=@Category,metricUnit=@MetricUnit,metricValue=@MetricValue WHERE id=@Id;";
            }
            else
            {
                query = "INSERT INTO dbjanmos.product(name,description,category,metricUnit,metricValue) VALUES(@Name,@Description,@Category,@MetricUnit,@MetricValue);";
            }

            Dictionary<string, string> productParameters = new Dictionary<string, string>()
            {
                {"@Id", product.Id.ToString()},
                {"@Name", product.Name},
                {"@Description", product.Description},
                {"@Category", product.Category.Id.ToString()},
                {"@MetricValue", product.MetricValue},
                {"@MetricUnit", product.MetricUnit.Id.ToString()}
            };

            upgradeManager = new UpgradeManager();
            if (upgradeManager.ExecuteQuery(query, productParameters))
                return true;
            return false;
        }
        public string OpenBackup(int buttonValue)
        {
            upgradeManager = new UpgradeManager();
            return this.upgradeManager.OpenBackup(buttonValue);
        }

        public void SaveBackup(int buttonValue, string strPath)
        {
            upgradeManager = new UpgradeManager();
            this.upgradeManager.SaveBackup(buttonValue, strPath);
        }
    }
}