using System;
using System.Windows.Forms;
using App.Paginator;
using Core.System.Repository;

namespace App.Product
{
    public partial class frmProduct : Form
    {
        private int selectedProductId = 0;
        private readonly int defaultRowCount = 20;
        private int pageSize;
        public frmProduct()
        {
            InitializeComponent();
            cmbRecordCount.SelectedItem = defaultRowCount.ToString();
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            Paginator.SimplePager paginator = new Paginator.SimplePager();

            int totalPages = CalculateTotalPages();
            paginator.SetPageCount(totalPages);

            LoadProductData(Convert.ToInt32(cmbRecordCount.SelectedItem));
        }

        public void LoadProductData(int pageSize)
        {
            ProductRepository productRepository = new ProductRepository();
            Paginator.SimplePager paginator = new Paginator.SimplePager();

            int totalPages = CalculateTotalPages();
            paginator.SetPageCount(totalPages);

            dgvProduct.DataSource = productRepository.LoadProductData(pageSize);
            this.dgvProduct.Columns["Metric Value"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        private int CalculateTotalPages()
        {
            ProductRepository productRepository = new ProductRepository();
            int totalRecords = productRepository.GetProductCount();
            pageSize = Convert.ToInt32(cmbRecordCount.SelectedItem);
            return (int)Math.Ceiling((double)totalRecords / pageSize);
        }

        public void cmbRecordCount_SelectedValueChanged(object sender, EventArgs e)
        {
            pageSize = int.Parse(cmbRecordCount.SelectedItem.ToString());
            this.LoadProductData(pageSize);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (frmProductModal info = new frmProductModal()) { 
                info.ShowDialog();
            }
            this.LoadProductData(int.Parse(cmbRecordCount.SelectedItem.ToString()));
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedProductId != 0)
            {
                using (frmProductModal info = new frmProductModal(this.selectedProductId))
                {
                    info.ShowDialog();
                }
                this.LoadProductData(int.Parse(cmbRecordCount.SelectedItem.ToString()));
            }
            else
            {
                MessageBox.Show("Please select a product to update.", "Update product", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ProductRepository productRepository = new ProductRepository();
            dgvProduct.DataSource = productRepository.LoadProductData(txtSearch.Text);
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProduct.RowCount > 0)
            {
                int selectedRowIndex = dgvProduct.SelectedCells[0].RowIndex;
                this.selectedProductId = Convert.ToInt32(dgvProduct.Rows[selectedRowIndex].Cells[0].Value?.ToString());
            }
        }
    }
}
