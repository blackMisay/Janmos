using System.Collections.Generic;
using Core.System.Data.Model;
using System.Data;

namespace Core.System.Repository
{
    public class ProductRepository
    {
        public DataTable LoadProductData()
        {
            string query = "SELECT product.id,product.name,product.description,category.description FROM product JOIN category ON product.category = category.id;";
            UpgradeManager upgradeManager = new UpgradeManager();
            return upgradeManager.Load(query);
        }
        public bool Save(Product product)
        {
            string query;
            
            if (product.Id > 0)
            {
                query = "UPDATE dbjanmos.product SET name=@Name,description=@Description,category=@Category WHERE Id=@Id);";
            }
            else
            {
                query = "INSERT INTO dbjanmos.product(name,description,category) VALUES(@Name,@Description,@Category);";
            }
            
            Dictionary<string, string> productParameters = new Dictionary<string, string>()
            {
                {"@Id", product.Id.ToString()},
                {"@Name", product.Name},
                {"@Description", product.Description},
                {"@Category", product.Category.Id.ToString()}
            };

            UpgradeManager upgradeManager = new UpgradeManager();
            if (upgradeManager.ExecuteQuery(query, productParameters))
                return true;
            return false;
        }
    }
}
