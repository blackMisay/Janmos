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

        private void btnSave_Click(object sender, EventArgs e)
        {
            FieldValidate();
        }
        public void FieldValidate()
        {
            bool validated = true;

            if (string.IsNullOrEmpty(txtProductName.Text) || string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                lblRequiredName.Visible = true;
                validated = false;
            }
            else
            {
                lblRequiredName.Visible = false;
            }

            if (string.IsNullOrEmpty(txtDescription.Text) || string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                lblRequiredDescription.Visible = true;
                validated = false;
            }
            else
            {
                lblRequiredDescription.Visible = false;
            }

            if (string.IsNullOrEmpty(txtMetricValue.Text) || string.IsNullOrWhiteSpace(txtMetricValue.Text))
            {
                lblRequiredMetricValue.Visible = true;
                validated = false;
            }
            else
            {
                lblRequiredMetricValue.Visible = false;
            }

            if (cmbCategory.SelectedIndex == -1)
            {
                lblRequiredCategory.Visible = true;
                validated = false;
            }
            else
            {
                lblRequiredCategory.Visible = false;
            }

            if (cmbMetricUnit.SelectedIndex == -1)
            {
                lblRequiredMetricUnit.Visible = true;
                validated = false;
            }
            else
            {
                lblRequiredMetricUnit.Visible = false;
            }

            if (!validated)
                return;

            productController = new ProductRepository();
            Core.System.Data.Model.Product product = new Core.System.Data.Model.Product();
            product.Id = this.Id;
            product.Name = this.txtProductName.Text;
            product.Description = this.txtDescription.Text;
            product.Category = new Category() { Id = Convert.ToInt32(cmbCategory.SelectedValue) };
            product.MetricUnit = new MetricUnit() { Id = Convert.ToInt32(cmbMetricUnit.SelectedValue) };
            product.MetricValue = this.txtMetricValue.Text;
            if (productController.Save(product))
            {
                MessageBox.Show("Record saved Successfully", "Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Unable to save the product record. Please try again later or contact support for assistance.\r\n", "Product", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponentsData()
        {
            productController = new ProductRepository();
            cmbCategory.DataSource = productController.LoadDataList("SELECT id, CONCAT(`description`,' (',`name`,')') AS `name` FROM dbjanmos.category;");
            cmbCategory.ValueMember = "id";
            cmbCategory.DisplayMember = "name";
            cmbCategory.SelectedIndex = -1;

            cmbMetricUnit.DataSource = productController.LoadDataList("SELECT id, CONCAT(`name`,' (',`symbol`,')') AS `name` FROM dbjanmos.metricunit;");
            cmbMetricUnit.ValueMember = "id";
            cmbMetricUnit.DisplayMember = "name";
            cmbMetricUnit.SelectedIndex = -1;
        }

        private void InitializeSelectedProductData()
        {
            InitializeComponentsData();

            productController = new ProductRepository();
            
            Core.System.Data.Model.Product product = new Core.System.Data.Model.Product();
            product = productController.FetchProductData(this.Id);

            this.txtProductName.Text = product.Name;
            this.txtDescription.Text = product.Description;
            this.cmbCategory.SelectedValue = product.Category.Id;
            this.cmbMetricUnit.SelectedValue = product.MetricUnit.Id;
            this.txtMetricValue.Text = product.MetricValue;
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close this form without saving the product?", "Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void lblResetFields_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Pressing the 'Clear all fields' will clear all values in fields and dropdown menus. Are you sure do you want to proceed?", "Clear all fields", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                txtProductName.Clear();
                txtDescription.Clear();
                cmbCategory.SelectedIndex = -1;
                cmbMetricUnit.SelectedIndex = -1;
                txtMetricValue.Clear();
            }
        }
    }
}
