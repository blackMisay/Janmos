using System;
using Core.System.Data.Model;
using Core.System.Repository;
using System.Windows.Forms;

namespace App.Product
{
    public partial class frmProductModal : Form
    {
        private readonly int Id = 0;
        ProductRepository productController;

        public frmProductModal()
        {
            InitializeComponent();
            InitializeComponentsData();
        }

        public frmProductModal(int productId)
        {
            InitializeComponent();
            this.Id = productId;
            InitializeSelectedProductData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Core.System.Data.Model.Product product = new Core.System.Data.Model.Product();
            product.Id = this.Id;
            product.Name = this.txtProductName.Text;
            product.Description = this.txtDescription.Text;
            product.Category = new Category() { Id = Convert.ToInt32(cmbCategory.SelectedValue) };
            product.MetricUnit = new MetricUnit() { Id = Convert.ToInt32(cmbUnit.SelectedValue) };
            product.MetricValue = this.txtValue.Text;

            productController = new ProductRepository();
            if (productController.Save(product))
            {
                MessageBox.Show("Save Successfully", "Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Product failed to save", "Product", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponentsData()
        {
            productController = new ProductRepository();
            cmbCategory.DataSource = productController.LoadDataList("SELECT id, CONCAT(`name`,'-',`description`) AS `name` FROM dbjanmos.category;");
            cmbCategory.ValueMember = "id";
            cmbCategory.DisplayMember = "name";

            cmbUnit.DataSource = productController.LoadDataList("SELECT id, CONCAT(`name`,' (',`symbol`,')') AS `name` FROM dbjanmos.metricunit;");
            cmbUnit.ValueMember = "id";
            cmbUnit.DisplayMember = "name";
        }

        private void InitializeSelectedProductData()
        {
            InitializeComponentsData();

            productController = new ProductRepository();
            
            Core.System.Data.Model.Product product = new Core.System.Data.Model.Product();
            product = productController.FetchProductData(this.Id);

            this.txtProductName.Text = product.Name;
            this.txtDescription.Text = product.Description;
            cmbCategory.SelectedValue = product.Category.Id;
            cmbUnit.SelectedValue = product.MetricUnit.Id;
            this.txtValue.Text = product.MetricValue;
        }
    }
}
