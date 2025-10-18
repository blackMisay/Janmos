using System;
using Core.System.Data.Model;
using Core.System.Repository;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Xml.Schema;
using Core.System.Security;
using System.Data.Odbc;

namespace App.Product
{
    public partial class frmProductModal : Form
    {
        private readonly User user = new User();
        private static Core.System.Data.Model.Product oldProduct = new Core.System.Data.Model.Product();
        private Core.System.Data.Model.Product updProduct = new Core.System.Data.Model.Product();

        private readonly int Id = 0;
        private int count = 0;
        private int newCount = 0;
        ProductRepository productRepository;

        public frmProductModal(User user)
        {
            this.user = user;
            InitializeComponent();
            InitializeComponentsData();
        }

        public frmProductModal(int productId, User user)
        {
            this.user = user;
            InitializeComponent();
            this.Id = productId;
            InitializeSelectedProductData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FieldValidate();
        }
        private void FieldValidate()
        {
            bool validated = true;

            bool ValidateTextBox(TextBox txt, Label lbl)
            {
                bool invalid = string.IsNullOrWhiteSpace(txt.Text);
                lbl.Visible = invalid;
                return !invalid;
            }
            bool ValidateComboBox(ComboBox cmb, Label lbl)
            {
                bool invalid = cmb.SelectedIndex == -1;
                lbl.Visible = invalid;
                return !invalid;
            }

            validated &= ValidateTextBox(txtProductName, lblRequiredName);
            validated &= ValidateTextBox(txtMetricValue, lblRequiredMetricValue);
            validated &= ValidateTextBox(txtReOrderPoint, lblRequiredReOrderPoint);
            validated &= ValidateTextBox(txtMaxStockLevel, lblRequiredMaxStockLevel);

            validated &= ValidateComboBox(cmbCategory, lblRequiredCategory);
            validated &= ValidateComboBox(cmbMetricUnit, lblRequiredMetricUnit);
            validated &= ValidateComboBox(cmbTaxType, lblRequiredTaxType);

            if (!validated)
                return;

            productRepository = new ProductRepository();
            if (this.Id != 0) //update
            {
                if (MessageBox.Show("Do you want to save the product data?", "Save Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Core.System.Data.Model.Product updProduct = SaveProduct();
                    var (oldJson, newJson, desc) = AuditHelper.GetDifferences(oldProduct, updProduct);
                    AuditLog log = new AuditLog
                    {
                        UserId = new User() { Id = Convert.ToInt32(this.user.Id)},
                        ActionType = "UPDATE",
                        TableName = "Product",
                        RecordId = updProduct.Id,
                        OldValue = oldJson,
                        NewValue = newJson,
                        Description = desc
                    };
                    if (productRepository.Save(log, SaveProduct(), this.user))
                    {
                        MessageBox.Show("Record saved Successfully", "Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Unable to save the product record. Please try again later or contact support for assistance.\r\n", "Product", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else //insert
            {
                if (MessageBox.Show("Do you want to save the product data?", "Save Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (productRepository.Save(null, SaveProduct(), this.user))
                    {
                        MessageBox.Show("Record saved Successfully", "Product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Unable to save the product record. Please try again later or contact support for assistance.\r\n", "Product", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private Core.System.Data.Model.Product SaveProduct()
        {
            updProduct.Id = this.Id;
            updProduct.Name = this.txtProductName.Text;
            updProduct.Description = this.txtDescription.Text;
            updProduct.Category = new Category() { Id = Convert.ToInt32(cmbCategory.SelectedValue) };
            updProduct.MetricUnit = new MetricUnit() { Id = Convert.ToInt32(cmbMetricUnit.SelectedValue) };
            updProduct.MetricValue = this.txtMetricValue.Text;
            updProduct.ReOrderPoint = Convert.ToInt32(this.txtReOrderPoint.Text);
            updProduct.MaxStockLevel = Convert.ToInt32(this.txtMaxStockLevel.Text);
            updProduct.TaxTypeId = new TaxType() { Id = Convert.ToInt32(cmbTaxType.SelectedValue) };
            updProduct.CreatedBy = new User() { Id = Convert.ToInt32(this.user.Id) };
            updProduct.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            updProduct.Status = StatusRecord.Type.Active;

            return updProduct;
        }

        private void InitializeComponentsData()
        {
            productRepository = new ProductRepository();
            cmbCategory.DataSource = productRepository.LoadDataList("SELECT id, CONCAT(`description`,' (',`name`,')') AS `name` FROM dbjanmos.category;");
            cmbCategory.ValueMember = "id";
            cmbCategory.DisplayMember = "name";
            cmbCategory.SelectedIndex = -1;

            cmbMetricUnit.DataSource = productRepository.LoadDataList("SELECT id, CONCAT(`name`,' (',`symbol`,')') AS `name` FROM dbjanmos.metricunit;");
            cmbMetricUnit.ValueMember = "id";
            cmbMetricUnit.DisplayMember = "name";
            cmbMetricUnit.SelectedIndex = -1;

            DataTable dt = productRepository.LoadDataList("SELECT taxtype.id AS `Id`, taxtype.taxname AS `Name` FROM taxtype;");
            cmbTaxType.ValueMember = "Id";
            cmbTaxType.DisplayMember = "Name";
            cmbTaxType.DataSource = dt;

            this.count = cmbTaxType.Items.Count;
            DataRow newRow = dt.NewRow();
            this.newCount = this.count + 1;
            newRow["Id"] = newCount;
            newRow["Name"] = "--Add Tax Type--";
            dt.Rows.InsertAt(newRow, newCount);

            cmbTaxType.SelectedIndex = -1;
        }

        private void InitializeSelectedProductData()
        {
            InitializeComponentsData();

            productRepository = new ProductRepository();
            
            updProduct = productRepository.FetchProductData(this.Id);
            oldProduct = updProduct;

            this.txtProductName.Text = updProduct.Name;
            this.txtDescription.Text = updProduct.Description;
            this.cmbCategory.SelectedValue = updProduct.Category.Id;
            this.cmbMetricUnit.SelectedValue = updProduct.MetricUnit.Id;
            this.txtMetricValue.Text = updProduct.MetricValue;
            this.txtReOrderPoint.Text = updProduct.ReOrderPoint.ToString();
            this.txtMaxStockLevel.Text = updProduct.MaxStockLevel.ToString();
            this.cmbTaxType.SelectedValue = updProduct.TaxTypeId.Id;
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
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
                txtReOrderPoint.Clear();
                txtMaxStockLevel.Clear();
            }
        }

        private void cmbTaxType_SelectedValueChanged(object sender, EventArgs e)
        {
            int selectedvalue = Convert.ToInt32(cmbTaxType.SelectedValue);
            if (cmbTaxType.SelectedIndex >= 0)
            {
                if (selectedvalue == this.newCount)
                {
                    using (frmTaxTypeModal ttm = new frmTaxTypeModal(this.user))
                    {
                        ttm.ShowDialog();
                        LoadProductModalData();
                    }
                }
            }
        }

        private void frmProductModal_Load(object sender, EventArgs e)
        {
            this.LoadProductModalData();
        }
        private void LoadProductModalData()
        {
            InitializeComponentsData();
        }
    }
}
