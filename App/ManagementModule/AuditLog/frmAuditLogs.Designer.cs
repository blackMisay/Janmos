namespace App.ManagementModule.AuditLog
{
    partial class frmAuditLogs
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
            this.dgvAuditLog = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtpSaleStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpSaleEndDate = new System.Windows.Forms.DateTimePicker();
            this.label27 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditLog)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvAuditLog
            // 
            this.dgvAuditLog.AllowUserToAddRows = false;
            this.dgvAuditLog.AllowUserToDeleteRows = false;
            this.dgvAuditLog.AllowUserToResizeColumns = false;
            this.dgvAuditLog.AllowUserToResizeRows = false;
            this.dgvAuditLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAuditLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAuditLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAuditLog.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvAuditLog.Location = new System.Drawing.Point(0, 0);
            this.dgvAuditLog.Name = "dgvAuditLog";
            this.dgvAuditLog.RowHeadersVisible = false;
            this.dgvAuditLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAuditLog.Size = new System.Drawing.Size(738, 330);
            this.dgvAuditLog.TabIndex = 66;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dgvAuditLog);
            this.panel1.Location = new System.Drawing.Point(23, 112);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(738, 330);
            this.panel1.TabIndex = 67;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.dtpSaleStartDate);
            this.panel2.Controls.Add(this.dtpSaleEndDate);
            this.panel2.Controls.Add(this.label27);
            this.panel2.Controls.Add(this.txtSearch);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label32);
            this.panel2.Location = new System.Drawing.Point(23, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(738, 78);
            this.panel2.TabIndex = 68;
            // 
            // dtpSaleStartDate
            // 
            this.dtpSaleStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpSaleStartDate.Location = new System.Drawing.Point(120, 51);
            this.dtpSaleStartDate.Name = "dtpSaleStartDate";
            this.dtpSaleStartDate.Size = new System.Drawing.Size(130, 20);
            this.dtpSaleStartDate.TabIndex = 78;
            this.dtpSaleStartDate.Value = new System.DateTime(2025, 6, 27, 12, 40, 50, 0);
            // 
            // dtpSaleEndDate
            // 
            this.dtpSaleEndDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpSaleEndDate.Location = new System.Drawing.Point(283, 51);
            this.dtpSaleEndDate.Name = "dtpSaleEndDate";
            this.dtpSaleEndDate.Size = new System.Drawing.Size(130, 20);
            this.dtpSaleEndDate.TabIndex = 77;
            this.dtpSaleEndDate.Value = new System.DateTime(2025, 6, 27, 12, 40, 50, 0);
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(19, 51);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(81, 17);
            this.label27.TabIndex = 76;
            this.label27.Text = "Date Range:";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(465, 50);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(275, 30);
            this.txtSearch.TabIndex = 75;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(461, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 74;
            this.label2.Text = "Search:";
            // 
            // label32
            // 
            this.label32.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(2, -1);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(101, 21);
            this.label32.TabIndex = 73;
            this.label32.Text = "AUDIT LOGS";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(256, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 17);
            this.label1.TabIndex = 79;
            this.label1.Text = "to";
            // 
            // frmAuditLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAuditLogs";
            this.Padding = new System.Windows.Forms.Padding(20, 25, 20, 0);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AuditLogs";
            this.Load += new System.EventHandler(this.frmAuditLogs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditLog)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvAuditLog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dtpSaleStartDate;
        private System.Windows.Forms.DateTimePicker dtpSaleEndDate;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label1;
    }
}