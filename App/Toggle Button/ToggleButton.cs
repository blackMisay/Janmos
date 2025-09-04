using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Toggle_Button
{
    public class ToggleButton : CheckBox
    {
        public ToggleButton()
        {
            this.Appearance = Appearance.Button;
            this.AutoSize = false;
            this.Width = 50;
            this.Height = 25;
            this.FlatStyle = FlatStyle.Flat;
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.Text = "OFF";
            this.BackColor = Color.LightGray;
            this.ForeColor = Color.Black;
            this.CheckedChanged += ToggleButton_CheckedChanged;
        }

        private void ToggleButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Checked)
            {
                this.Text = "ON";
                this.BackColor = Color.DodgerBlue;
                this.ForeColor = Color.White;
            }
            else
            {
                this.Text = "OFF";
                this.BackColor = Color.LightGray;
                this.ForeColor = Color.Black;
            }
        }
    }

}
