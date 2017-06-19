namespace AHMCManualStatementApplication
{
    partial class SearchByAccount
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
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.txtSearchAccountNumber = new MetroFramework.Controls.MetroTextBox();
            this.btnOK = new MetroFramework.Controls.MetroButton();
            this.btnCancel = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(24, 37);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(167, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Enter an account number:";
            // 
            // txtSearchAccountNumber
            // 
            // 
            // 
            // 
            this.txtSearchAccountNumber.CustomButton.Image = null;
            this.txtSearchAccountNumber.CustomButton.Location = new System.Drawing.Point(239, 1);
            this.txtSearchAccountNumber.CustomButton.Name = "";
            this.txtSearchAccountNumber.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtSearchAccountNumber.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSearchAccountNumber.CustomButton.TabIndex = 1;
            this.txtSearchAccountNumber.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSearchAccountNumber.CustomButton.UseSelectable = true;
            this.txtSearchAccountNumber.CustomButton.Visible = false;
            this.txtSearchAccountNumber.Lines = new string[0];
            this.txtSearchAccountNumber.Location = new System.Drawing.Point(24, 72);
            this.txtSearchAccountNumber.MaxLength = 32767;
            this.txtSearchAccountNumber.Name = "txtSearchAccountNumber";
            this.txtSearchAccountNumber.PasswordChar = '\0';
            this.txtSearchAccountNumber.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSearchAccountNumber.SelectedText = "";
            this.txtSearchAccountNumber.SelectionLength = 0;
            this.txtSearchAccountNumber.SelectionStart = 0;
            this.txtSearchAccountNumber.ShortcutsEnabled = true;
            this.txtSearchAccountNumber.Size = new System.Drawing.Size(261, 23);
            this.txtSearchAccountNumber.TabIndex = 1;
            this.txtSearchAccountNumber.UseSelectable = true;
            this.txtSearchAccountNumber.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSearchAccountNumber.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(61, 126);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseSelectable = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(172, 126);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseSelectable = true;
            // 
            // SearchByAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 182);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtSearchAccountNumber);
            this.Controls.Add(this.metroLabel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchByAccount";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox txtSearchAccountNumber;
        private MetroFramework.Controls.MetroButton btnOK;
        private MetroFramework.Controls.MetroButton btnCancel;
    }
}