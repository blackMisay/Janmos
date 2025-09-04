using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Core.System.Repository;

namespace App.Product
{
    public partial class frmProduct : Form
    {
        ProductRepository productRepository;
        private int selectedProductId = 0;
        private readonly int defaultRowCount = 20;
        private int selectedCategoryId;
        public frmProduct()
        {
            InitializeComponent();
            cmbRecordCount.SelectedItem = defaultRowCount.ToString();
            InitializeComponentsData();
            
        }
        private void InitializeComponentsData()
        {
            productRepository = new ProductRepository();

            cmbCategory.DataSource = productRepository.LoadDataList("SELECT c.id, c.`description` AS `name` FROM category c;");
            cmbCategory.ValueMember = "id";
            cmbCategory.DisplayMember = "name";
            cmbCategory.SelectedIndex = -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (frmProductModal info = new frmProductModal())
            {
                info.ShowDialog();
            }
            FieldEnabling();
            this.LoadProductData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.selectedProductId != 0)
            {
                using (frmProductModal info = new frmProductModal(this.selectedProductId))
                {
                    info.ShowDialog();
                }
                this.LoadProductData();
            }
            else
            {
                MessageBox.Show("Please select a product to update.", "Update product", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            FieldEnabling();
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            this.LoadProductData();
            FieldEnabling();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProduct.RowCount > 0)
            {
                int selectedRowIndex = dgvProduct.SelectedCells[0].RowIndex;
                this.selectedProductId = Convert.ToInt32(dgvProduct.Rows[selectedRowIndex].Cells[0].Value?.ToString());
            }
            FieldEnabling();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            productRepository = new ProductRepository();
            if (cmbCategory.SelectedValue != null)
            {
                dgvProduct.DataSource = productRepository.LoadProductDataViaSearch(txtSearch.Text, this.selectedCategoryId);
            }
            else
            {
                dgvProduct.DataSource = productRepository.LoadProductDataViaSearch(txtSearch.Text);
            }
        }

        private void LoadProductData()
        {
            productRepository = new ProductRepository();
            if (cmbCategory.SelectedValue != null)
            {
                dgvProduct.DataSource = productRepository.LoadProductData(this.selectedCategoryId);
            }
            else
            {
                dgvProduct.DataSource = productRepository.LoadProductData();
            }

            this.dgvProduct.Columns["Product Id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvProduct.Columns["Metric Value"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.selectedProductId > 0)
            {
                if (MessageBox.Show("Do you want to delete the selected product?", "Delete Product", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    productRepository = new ProductRepository();
                    productRepository.DeleteProductData(this.selectedProductId);
                    this.selectedProductId = 0;
                    this.LoadProductData();

                    MessageBox.Show("Delete Successfully.", "Delete product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a product to delete.", "Delete product", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            FieldEnabling();
        }

        private void frmProduct_Click(object sender, EventArgs e)
        {
            this.selectedProductId = 0;
            FieldEnabling();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            this.selectedProductId = 0;
            FieldEnabling();
        }
        private void FieldEnabling()
        {
            btnEdit.Enabled = btnDelete.Enabled = false;

            if (this.selectedProductId != 0)
            {
                btnEdit.Enabled = btnDelete.Enabled = true;
            }
            else
            {
                return;
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategory.Items.Count > 0)
            {
                if (cmbCategory.SelectedValue != null)
                {
                    this.selectedCategoryId = Convert.ToInt32(cmbCategory.SelectedValue);
                    LoadProductData();
                }
            }
        }
    }
}
