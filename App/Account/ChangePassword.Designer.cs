namespace App.Account
{
    partial class ChangePassword
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.txtReenterPassword = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnEPShowHide = new System.Windows.Forms.Button();
            this.btnRPShowHide = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmPassword.Location = new System.Drawing.Point(59, 9);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(142, 21);
            this.lblConfirmPassword.TabIndex = 0;
            this.lblConfirmPassword.Text = "Confirm Password";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewPassword.Location = new System.Drawing.Point(12, 45);
            this.txtNewPassword.Multiline = true;
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(200, 30);
            this.txtNewPassword.TabIndex = 1;
            this.txtNewPassword.Tag = "";
            this.txtNewPassword.Text = "Enter Password";
            this.txtNewPassword.Enter += new System.EventHandler(this.txtNewPassword_Enter);
            this.txtNewPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewPassword_KeyDown);
            this.txtNewPassword.Leave += new System.EventHandler(this.txtNewPassword_Leave);
            // 
            // txtReenterPassword
            // 
            this.txtReenterPassword.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReenterPassword.Location = new System.Drawing.Point(12, 81);
            this.txtReenterPassword.Multiline = true;
            this.txtReenterPassword.Name = "txtReenterPassword";
            this.txtReenterPassword.Size = new System.Drawing.Size(200, 30);
            this.txtReenterPassword.TabIndex = 2;
            this.txtReenterPassword.Tag = "";
            this.txtReenterPassword.Text = "Reenter Password";
            this.txtReenterPassword.Enter += new System.EventHandler(this.txtReenterPassword_Enter);
            this.txtReenterPassword.Leave += new System.EventHandler(this.txtReenterPassword_Leave);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(169, 117);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 30);
            this.btnConfirm.TabIndex = 3;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnEPShowHide
            // 
            this.btnEPShowHide.Location = new System.Drawing.Point(212, 44);
            this.btnEPShowHide.Name = "btnEPShowHide";
            this.btnEPShowHide.Size = new System.Drawing.Size(32, 32);
            this.btnEPShowHide.TabIndex = 4;
            this.btnEPShowHide.Text = "EP";
            this.btnEPShowHide.UseVisualStyleBackColor = true;
            this.btnEPShowHide.Click += new System.EventHandler(this.btnEPShowHide_Click);
            // 
            // btnRPShowHide
            // 
            this.btnRPShowHide.Location = new System.Drawing.Point(212, 80);
            this.btnRPShowHide.Name = "btnRPShowHide";
            this.btnRPShowHide.Size = new System.Drawing.Size(32, 32);
            this.btnRPShowHide.TabIndex = 5;
            this.btnRPShowHide.Text = "RP";
            this.btnRPShowHide.UseVisualStyleBackColor = true;
            this.btnRPShowHide.Click += new System.EventHandler(this.btnRPShowHide_Click);
            // 
            // ChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 156);
            this.ControlBox = false;
            this.Controls.Add(this.btnRPShowHide);
            this.Controls.Add(this.btnEPShowHide);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtReenterPassword);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.lblConfirmPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChangePassword";
            this.Load += new System.EventHandler(this.ChangePassword_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.TextBox txtReenterPassword;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnEPShowHide;
        private System.Windows.Forms.Button btnRPShowHide;
    }
}