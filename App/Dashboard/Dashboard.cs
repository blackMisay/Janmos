using Core.System.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Dashboard
{
    public partial class Dashboard : Form
    {
        private readonly User user = new User();
        public Dashboard(User user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            txtQuantityInHandValue.Text = "0";
            txtQuantityToBeReceivedValue.Text = "0";
        }
    }
}
