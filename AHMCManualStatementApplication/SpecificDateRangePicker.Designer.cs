namespace AHMCManualStatementApplication
{
    partial class SpecificDateRangePicker
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
            this.btnDateCancelPickerCancel = new MetroFramework.Controls.MetroButton();
            this.btnDateRangePickerOK = new MetroFramework.Controls.MetroButton();
            this.dtPickerFromDate = new MetroFramework.Controls.MetroDateTime();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.dtPickerToDate = new MetroFramework.Controls.MetroDateTime();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // btnDateCancelPickerCancel
            // 
            this.btnDateCancelPickerCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDateCancelPickerCancel.Location = new System.Drawing.Point(260, 140);
            this.btnDateCancelPickerCancel.Name = "btnDateCancelPickerCancel";
            this.btnDateCancelPickerCancel.Size = new System.Drawing.Size(75, 23);
            this.btnDateCancelPickerCancel.TabIndex = 3;
            this.btnDateCancelPickerCancel.Text = "Cancel";
            this.btnDateCancelPickerCancel.UseSelectable = true;
            this.btnDateCancelPickerCancel.Click += new System.EventHandler(this.btnDateCancelPickerCancel_Click);
            // 
            // btnDateRangePickerOK
            // 
            this.btnDateRangePickerOK.Location = new System.Drawing.Point(148, 140);
            this.btnDateRangePickerOK.Name = "btnDateRangePickerOK";
            this.btnDateRangePickerOK.Size = new System.Drawing.Size(75, 23);
            this.btnDateRangePickerOK.TabIndex = 2;
            this.btnDateRangePickerOK.Text = "OK";
            this.btnDateRangePickerOK.UseSelectable = true;
            this.btnDateRangePickerOK.Click += new System.EventHandler(this.btnDateRangePickerOK_Click);
            // 
            // dtPickerFromDate
            // 
            this.dtPickerFromDate.Location = new System.Drawing.Point(23, 82);
            this.dtPickerFromDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtPickerFromDate.Name = "dtPickerFromDate";
            this.dtPickerFromDate.Size = new System.Drawing.Size(200, 29);
            this.dtPickerFromDate.Style = MetroFramework.MetroColorStyle.Red;
            this.dtPickerFromDate.TabIndex = 0;
            this.dtPickerFromDate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtPickerFromDate_MouseDown);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(23, 38);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(217, 19);
            this.metroLabel1.TabIndex = 3;
            this.metroLabel1.Text = "Please select a date range to view:";
            // 
            // dtPickerToDate
            // 
            this.dtPickerToDate.Location = new System.Drawing.Point(260, 82);
            this.dtPickerToDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtPickerToDate.Name = "dtPickerToDate";
            this.dtPickerToDate.Size = new System.Drawing.Size(200, 29);
            this.dtPickerToDate.Style = MetroFramework.MetroColorStyle.Red;
            this.dtPickerToDate.TabIndex = 1;
            this.dtPickerToDate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtPickerToDate_MouseDown);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(230, 92);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(22, 19);
            this.metroLabel2.TabIndex = 3;
            this.metroLabel2.Text = "to";
            // 
            // SpecificDateRangePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 186);
            this.Controls.Add(this.btnDateCancelPickerCancel);
            this.Controls.Add(this.btnDateRangePickerOK);
            this.Controls.Add(this.dtPickerToDate);
            this.Controls.Add(this.dtPickerFromDate);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpecificDateRangePicker";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Load += new System.EventHandler(this.SpecificDateRangePicker_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton btnDateCancelPickerCancel;
        private MetroFramework.Controls.MetroButton btnDateRangePickerOK;
        private MetroFramework.Controls.MetroDateTime dtPickerFromDate;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroDateTime dtPickerToDate;
        private MetroFramework.Controls.MetroLabel metroLabel2;
    }
}