namespace App.Paginator
{
    partial class SimplePager
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnFirstPage = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.cmbPageNumber = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnFirstPage
            // 
            this.btnFirstPage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirstPage.Image = global::App.Properties.Resources.firstpage_16px;
            this.btnFirstPage.Location = new System.Drawing.Point(3, 3);
            this.btnFirstPage.Name = "btnFirstPage";
            this.btnFirstPage.Size = new System.Drawing.Size(25, 25);
            this.btnFirstPage.TabIndex = 0;
            this.btnFirstPage.UseVisualStyleBackColor = true;
            this.btnFirstPage.Click += new System.EventHandler(this.SetPage);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrevious.Image = global::App.Properties.Resources.previouspage_16px;
            this.btnPrevious.Location = new System.Drawing.Point(43, 3);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(25, 25);
            this.btnPrevious.TabIndex = 1;
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.SetPage);
            // 
            // btnLastPage
            // 
            this.btnLastPage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLastPage.Image = global::App.Properties.Resources.lastpage_16px;
            this.btnLastPage.Location = new System.Drawing.Point(161, 3);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(25, 25);
            this.btnLastPage.TabIndex = 3;
            this.btnLastPage.UseVisualStyleBackColor = true;
            this.btnLastPage.Click += new System.EventHandler(this.SetPage);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Image = global::App.Properties.Resources.nextpage_16px;
            this.btnNext.Location = new System.Drawing.Point(121, 3);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(25, 25);
            this.btnNext.TabIndex = 2;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.SetPage);
            // 
            // cmbPageNumber
            // 
            this.cmbPageNumber.FormattingEnabled = true;
            this.cmbPageNumber.Location = new System.Drawing.Point(74, 7);
            this.cmbPageNumber.Name = "cmbPageNumber";
            this.cmbPageNumber.Size = new System.Drawing.Size(40, 21);
            this.cmbPageNumber.TabIndex = 4;
            this.cmbPageNumber.SelectedValueChanged += new System.EventHandler(this.cmbPageNumber_SelectedValueChanged);
            // 
            // SimplePager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbPageNumber);
            this.Controls.Add(this.btnLastPage);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnFirstPage);
            this.Name = "SimplePager";
            this.Size = new System.Drawing.Size(190, 31);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFirstPage;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnLastPage;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.ComboBox cmbPageNumber;
    }
}
