namespace AHMCManualStatementApplication
{
    partial class SpecificDatePicker
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
            this.dtPickerSpecificDate = new MetroFramework.Controls.MetroDateTime();
            this.btnDatePickerOK = new MetroFramework.Controls.MetroButton();
            this.btnDatePickerCancel = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(29, 38);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(178, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Please select a date to view:";
            // 
            // dtPickerSpecificDate
            // 
            this.dtPickerSpecificDate.Location = new System.Drawing.Point(29, 81);
            this.dtPickerSpecificDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtPickerSpecificDate.Name = "dtPickerSpecificDate";
            this.dtPickerSpecificDate.Size = new System.Drawing.Size(200, 29);
            this.dtPickerSpecificDate.TabIndex = 1;
            this.dtPickerSpecificDate.ValueChanged += new System.EventHandler(this.dtPickerSpecificDate_ValueChanged);
            this.dtPickerSpecificDate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtPickerSpecificDate_MouseDown);
            // 
            // btnDatePickerOK
            // 
            this.btnDatePickerOK.Location = new System.Drawing.Point(29, 135);
            this.btnDatePickerOK.Name = "btnDatePickerOK";
            this.btnDatePickerOK.Size = new System.Drawing.Size(75, 23);
            this.btnDatePickerOK.TabIndex = 2;
            this.btnDatePickerOK.Text = "OK";
            this.btnDatePickerOK.UseSelectable = true;
            this.btnDatePickerOK.Click += new System.EventHandler(this.btnDatePickerOK_Click);
            // 
            // btnDatePickerCancel
            // 
            this.btnDatePickerCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDatePickerCancel.Location = new System.Drawing.Point(154, 135);
            this.btnDatePickerCancel.Name = "btnDatePickerCancel";
            this.btnDatePickerCancel.Size = new System.Drawing.Size(75, 23);
            this.btnDatePickerCancel.TabIndex = 2;
            this.btnDatePickerCancel.Text = "Cancel";
            this.btnDatePickerCancel.UseSelectable = true;
            this.btnDatePickerCancel.Click += new System.EventHandler(this.btnDatePickerCancel_Click);
            // 
            // SpecificDatePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnDatePickerCancel;
            this.ClientSize = new System.Drawing.Size(254, 195);
            this.Controls.Add(this.btnDatePickerCancel);
            this.Controls.Add(this.btnDatePickerOK);
            this.Controls.Add(this.dtPickerSpecificDate);
            this.Controls.Add(this.metroLabel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpecificDatePicker";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Load += new System.EventHandler(this.SpecificDatePicker_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroDateTime dtPickerSpecificDate;
        private MetroFramework.Controls.MetroButton btnDatePickerOK;
        private MetroFramework.Controls.MetroButton btnDatePickerCancel;
    }
}