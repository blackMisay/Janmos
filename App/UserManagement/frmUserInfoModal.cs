using Core.System.Data.Model;
using Core.System.Repository;
using Core.System.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.UserManagement
{
    public partial class frmUserInfoModal : Form
    {
        UserManagementRepository userManagementRepository;
        UserInfo userInfo;
        User account;
        private readonly int selectedUserId = 0;
        private readonly int UserInfoId = 0;
        private string lastLogin = "";
        private DataTable rolesTable;
        private DataTable filteredRoles;
        private int usercreatorId = 0;
        private string   userRole = "";
        private string userName = "";
        private string userFullname = "";
        private string firstName = "";
        private string lastName = "";
        public frmUserInfoModal(string username)
        {
            InitializeComponent();
            this.userName = username;
            InitializeCreatorData();
            InitializeComponentsData(this.userRole);
        }
        public frmUserInfoModal(string username, int selectedUserId)
        {
            InitializeComponent();
            this.userName = username;
            this.selectedUserId = selectedUserId;
            InitializeCreatorData();
            InitializeSelectedUserData();
        }
        private void InitializeCreatorData()
        {
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            userManagementRepository = new UserManagementRepository();
            if (!string.IsNullOrWhiteSpace(this.userName))
            {
                this.usercreatorId = Convert.ToInt32(userManagementRepository.ExecuteScalar("SELECT u.id FROM `user` u WHERE u.username = @Username;", new Dictionary<string, string>
                {
                    { "@Username", this.userName}
                }));
                this.userRole = (userManagementRepository.ExecuteScalar("SELECT r.roletitle FROM `user` u JOIN roles r ON u.roleid = r.id WHERE u.username = @Username;", new Dictionary<string, string>
                {
                    { "@Username", this.userName}
                })).ToString();
                this.userFullname = (userManagementRepository.ExecuteScalar("SELECT CONCAT(ui.givenname, ' ', ui.lastname) FROM `user` u JOIN userinfo ui ON u.userinfoid = ui.id WHERE u.username = @Username;", new Dictionary<string, string>
                {
                    { "@Username", this.userName}
                })).ToString();
                lblCreatedby.Text = "Created By: " + this.userFullname;
            }
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            if (string.IsNullOrWhiteSpace(this.userRole) || this.usercreatorId == 0 || string.IsNullOrWhiteSpace(this.userFullname))
            {
                MessageBox.Show("Had an issue in your user credential.", "User R");
                this.Dispose();
            }
        }
        private void InitializeSelectedUserData()
        {
            InitializeComponentsData(this.userRole);
            userManagementRepository = new UserManagementRepository();
            gbNewAccount.Enabled = false;

            User account = new User();
            account = userManagementRepository.FetchUserData(this.selectedUserId);

            if (cmbRole.DataSource != null)
            {
                this.cmbRole.SelectedValue = account.RolesId.Id;
            }

            UserInfo userInfo = new UserInfo();
            userInfo = userManagementRepository.FetchUserInfo(Convert.ToInt32(account.UserInfoId.Id));

            this.txtFirstname.Text = userInfo.GivenName;
            this.txtLastname.Text = userInfo.LastName;
            if (Enum.IsDefined(typeof(Gender), userInfo.Gender))
            {
                this.cmbGender.SelectedItem = userInfo.Gender;
            }
            this.txtContact.Text = userInfo.Contact;
            this.txtEmail.Text = userInfo.Email;
            this.txtAddress.Text = userInfo.Address;
            this.txtRoledesc.Text = (userManagementRepository.ExecuteScalar("SELECT r.description FROM roles r WHERE r.id = @RoleId;", new Dictionary<string, string>
            {
                { "@RoleId", account.RolesId.Id.ToString()}
            })).ToString();
        }
                                            
        private void frmUserInfoModal_Load(object sender, EventArgs e)
        {
            LoadUserInfoModal();
        }
        private void LoadUserInfoModal()
        {
            this.ActiveControl = lblFields;
            txtRoledesc.Enabled = false;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            FieldValidate();
        }
        private void FieldValidate()
        {
            if (this.selectedUserId != 0)
            {
                this.lastLogin = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            }
            else
            {
                this.lastLogin = null;
            }

            if (
                txtFirstname.Text == "First Name" || 
                txtLastname.Text == "Last Name" || 
                cmbGender.SelectedIndex == -1 || 
                cmbRole.SelectedIndex == -1 || 
                string.IsNullOrWhiteSpace(txtContact.Text) || 
                string.IsNullOrWhiteSpace(txtEmail.Text) || 
                string.IsNullOrWhiteSpace(txtAddress.Text) || 
                string.IsNullOrWhiteSpace(txtRoledesc.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtReenterpass.Text)
                )
            {
                MessageBox.Show("All fields are required.", "User Info Modal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtPassword.Text != txtReenterpass.Text)
            {
                MessageBox.Show("Enter Password not match.", "User Info Modal",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.ActiveControl = gbNewAccount;
                return;
            }

            this.firstName =  CapitalizeFirstLetter(txtFirstname.Text);
            this.lastName = CapitalizeFirstLetter(txtLastname.Text);

            userInfo = new UserInfo();
            userInfo.Id = this.UserInfoId;
            userInfo.GivenName = this.firstName;
            userInfo.LastName = this.lastName;
            userInfo.Gender = new Gender();
            userInfo.Contact = txtContact.Text;
            userInfo.Address = txtAddress.Text;
            userInfo.Email = txtEmail.Text;
            userInfo.Status = StatusRecord.Type.Active;

            userManagementRepository = new UserManagementRepository();

            int lastInsertedId = userManagementRepository.SaveInfo(userInfo);

            if (lastInsertedId > 0)
            {
                UserAuthentication ua = new UserAuthentication();

                account = new User();
                account.Id = this.selectedUserId;
                account.RolesId = new Roles() { Id = Convert.ToInt32(cmbRole.SelectedValue) };
                account.UserInfoId = new UserInfo() { Id = lastInsertedId };
                account.Username = txtUsername.Text;
                account.Key = ua.GenerateSalt();
                account.Password = ua.HashPassword(txtPassword.Text, account.Key);
                account.Lastlogin = this.lastLogin;
                account.CreatedBy = new User() { Id = this.usercreatorId };
                account.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
                account.Status = StatusRecord.Type.Active;

                if (userManagementRepository.SaveUser(account))
                {
                    MessageBox.Show("Save Sucessfully", "User Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("User Account failed to save.", "User Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("User Info failed to save.", "User Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string CapitalizeFirstLetter(string name)
        {
            if (char.IsUpper(name[0]))
            {
                return name;
            }
            return char.ToUpper(name[0]) + name.Substring(1);
        }
        private void InitializeComponentsData(string currentRole)
        {
            if (this.selectedUserId == 0)
            {
                txtFirstname.Text = "First Name";
                txtFirstname.ForeColor = Color.Gray;
                txtLastname.Text = "Last Name";
                txtLastname.ForeColor = Color.Gray;
                cmbGender.SelectedIndex = -1;
                cmbRole.SelectedIndex = -1;
            }

            userManagementRepository = new UserManagementRepository();
            cmbGender.DataSource = Enum.GetValues(typeof(Gender));
            cmbGender.SelectedIndex = -1;

            rolesTable = userManagementRepository.LoadDataList("SELECT r.id, r.roletitle FROM roles r;");
            filteredRoles = rolesTable.Clone();

            foreach (DataRow row in rolesTable.Rows)
            {
                string roleTitle = row["roletitle"].ToString();

                if (currentRole == "Super Admin" || (currentRole == "Admin" && roleTitle != "Super Admin"))
                {
                    this.filteredRoles.ImportRow(row);
                }
            }

            cmbRole.DataSource = this.filteredRoles;
            cmbRole.ValueMember = "id";
            cmbRole.DisplayMember = "roletitle";
            cmbRole.SelectedIndex = -1;
        }

        private void txtFirstname_Enter(object sender, EventArgs e)
        {
            if (txtFirstname.Text == "First Name")
            {
                txtFirstname.Text = "";
                txtFirstname.ForeColor = Color.Black;
            }
        }

        private void txtFirstname_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstname.Text))
            {
                txtFirstname.Text = "First Name";
                txtFirstname.ForeColor = Color.Gray;
            }
        }

        private void txtLastame_Enter(object sender, EventArgs e)
        {
            if (txtLastname.Text == "Last Name")
            {
                txtLastname.Text = "";
                txtLastname.ForeColor = Color.Black;
            }
        }

        private void txtLastame_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLastname.Text))
            {
                txtLastname.Text = "Last Name";
                txtLastname.ForeColor = Color.Gray;
            }
        }

        private void tmrDatetime_Tick(object sender, EventArgs e)
        {
            lblDatetime.Text = "Date && Time: " + DateTime.Now.ToString();
            tmrDatetime.Start();
        }

        private void txtReenterpass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnNext_Click(sender, e);
            }
        }

        private void txtFirstname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnNext_Click(sender, e);
            }
        }

        private void txtLastame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnNext_Click(sender, e);
            }
        }

        private void txtContact_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnNext_Click(sender, e);
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnNext_Click(sender, e);
            }
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnNext_Click(sender, e);
            }
        }

        private void txtRoledesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnNext_Click(sender, e);
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnNext_Click(sender, e);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnNext_Click(sender, e);
            }
        }

        private void cmbRole_SelectedValueChanged(object sender, EventArgs e)
        {
            userManagementRepository = new UserManagementRepository();
        }
    }
}
