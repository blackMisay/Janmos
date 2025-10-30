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

namespace App.ManagementModule.AuditLog
{
    public partial class frmAuditLogs : Form
    {
        private readonly User user = new User();
        ManagementModuleRepository repo = new ManagementModuleRepository();
        public frmAuditLogs(User user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void frmAuditLogs_Load(object sender, EventArgs e)
        {
            this.LoadAuditLogs();
        }
        private void LoadAuditLogs()
        {
            dgvAuditLog.DataSource = repo.LoadAuditLog();
        }
    }
}
