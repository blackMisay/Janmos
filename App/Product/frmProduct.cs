using System;
using System.Windows.Forms;
using Core.System.Repository;

namespace App.Product
{
    public partial class frmProduct : Form
    {
        private int selectedProductId = 0;
        private readonly int defaultRowCount = 20;
        public frmProduct()
        {
            InitializeComponent();
            cmbRecordCount.SelectedItem = defaultRowCount.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (frmProductModal info = new frmProductModal()) { 
                info.ShowDialog();
            }
            this.LoadProductData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedProductId != 0)
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
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            this.LoadProductData();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProduct.RowCount > 0)
            {
                int selectedRowIndex = dgvProduct.SelectedCells[0].RowIndex;
                this.selectedProductId = Convert.ToInt32(dgvProduct.Rows[selectedRowIndex].Cells[0].Value?.ToString());
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ProductRepository productRepository = new ProductRepository();
            dgvProduct.DataSource = productRepository.LoadProductData(txtSearch.Text);
        }

        private void LoadProductData()
        {
            ProductRepository productRepository = new ProductRepository();
            dgvProduct.DataSource = productRepository.LoadProductData();
            this.dgvProduct.Columns["Metric Value"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
