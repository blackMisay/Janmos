using App.Product;
using Core.System.Data.Model;
using Core.System.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.UserManagement
{
    public partial class frmUserManagement : Form
    {
        UserManagementRepository userManagement;
        private readonly User user = new User();
        private int selectedUserManagementId = 0;
        private string userName = "";
        public frmUserManagement(User user)
        {
            this.user = user;
            InitializeComponent();
            FieldEnabling();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.selectedUserManagementId > 0)
            {
                if (MessageBox.Show("Do you want to delete the selected user?", "Delete User Record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    userManagement = new UserManagementRepository();
                    userManagement.DeleteUserManagementData(this.selectedUserManagementId);
                    this.selectedUserManagementId = 0;
                    this.LoadUserManagementData();

                    MessageBox.Show("Delete Successfully.", "Delete User Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.", "Delete User Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            FieldEnabling();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.selectedUserManagementId != 0)
            {
                using (frmUserInfoModal uim = new frmUserInfoModal(this.userName, this.selectedUserManagementId))
                {
                    uim.ShowDialog();
                }
                this.LoadUserManagementData();
            }
            else
            {
                MessageBox.Show("Please select a product to update.", "Update product", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.LoadUserManagementData();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            using (frmUserInfoModal uim = new frmUserInfoModal(this.userName))
            {
                uim.ShowDialog();
            }
            this.LoadUserManagementData();
        }

        private void LoadUserManagementData()
        {
            this.selectedUserManagementId = 0;
            userManagement = new UserManagementRepository();
            dgvUserManagement.DataSource = userManagement.LoadUserManagementData();

            this.dgvUserManagement.Columns["ID"].Visible = false;
            FieldEnabling();
        }

        private void frmUserManagement_Load(object sender, EventArgs e)
        {
            LoadUserManagementData();
        }

        private void dgvUserManagement_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUserManagement.RowCount > 0)
            {
                int selectedRowIndex = dgvUserManagement.SelectedCells[0].RowIndex;
                this.selectedUserManagementId = Convert.ToInt32(dgvUserManagement.Rows[selectedRowIndex].Cells[0].Value?.ToString());
            }
            FieldEnabling();
        }
        private void FieldEnabling()
        {
            btnEdit.Enabled = btnDelete.Enabled = false;

            if (this.selectedUserManagementId != 0)
            {
                btnEdit.Enabled = btnDelete.Enabled = true;
            }
            else
            {
                return;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            userManagement = new UserManagementRepository();
            dgvUserManagement.DataSource = userManagement.LoadUserManagementData(txtSearch.Text);
        }

        private void frmUserManagement_Click(object sender, EventArgs e)
        {
            this.selectedUserManagementId = 0;
            FieldEnabling();
        }
    }
}
