namespace AHMCManualStatementApplication
{
    partial class AddNote
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
            if (disposing && (components != null)) {
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
            this.components = new System.ComponentModel.Container();
            this.msmAddNote = new MetroFramework.Components.MetroStyleManager(this.components);
            this.txtEditNoteDate = new MetroFramework.Controls.MetroTextBox();
            this.txtEditAddNote = new MetroFramework.Controls.MetroTextBox();
            this.btnEditAddNote = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.msmAddNote)).BeginInit();
            this.SuspendLayout();
            // 
            // msmAddNote
            // 
            this.msmAddNote.Owner = this;
            this.msmAddNote.Style = MetroFramework.MetroColorStyle.Red;
            // 
            // txtEditNoteDate
            // 
            this.txtEditNoteDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            // 
            // 
            // 
            this.txtEditNoteDate.CustomButton.Image = null;
            this.txtEditNoteDate.CustomButton.Location = new System.Drawing.Point(110, 1);
            this.txtEditNoteDate.CustomButton.Name = "";
            this.txtEditNoteDate.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtEditNoteDate.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtEditNoteDate.CustomButton.TabIndex = 1;
            this.txtEditNoteDate.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtEditNoteDate.CustomButton.UseSelectable = true;
            this.txtEditNoteDate.CustomButton.Visible = false;
            this.txtEditNoteDate.Lines = new string[0];
            this.txtEditNoteDate.Location = new System.Drawing.Point(23, 79);
            this.txtEditNoteDate.MaxLength = 32767;
            this.txtEditNoteDate.Name = "txtEditNoteDate";
            this.txtEditNoteDate.PasswordChar = '\0';
            this.txtEditNoteDate.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtEditNoteDate.SelectedText = "";
            this.txtEditNoteDate.SelectionLength = 0;
            this.txtEditNoteDate.SelectionStart = 0;
            this.txtEditNoteDate.ShortcutsEnabled = true;
            this.txtEditNoteDate.Size = new System.Drawing.Size(132, 23);
            this.txtEditNoteDate.TabIndex = 0;
            this.txtEditNoteDate.UseCustomBackColor = true;
            this.txtEditNoteDate.UseSelectable = true;
            this.txtEditNoteDate.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtEditNoteDate.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtEditAddNote
            // 
            // 
            // 
            // 
            this.txtEditAddNote.CustomButton.Image = null;
            this.txtEditAddNote.CustomButton.Location = new System.Drawing.Point(264, 1);
            this.txtEditAddNote.CustomButton.Name = "";
            this.txtEditAddNote.CustomButton.Size = new System.Drawing.Size(135, 135);
            this.txtEditAddNote.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtEditAddNote.CustomButton.TabIndex = 1;
            this.txtEditAddNote.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtEditAddNote.CustomButton.UseSelectable = true;
            this.txtEditAddNote.CustomButton.Visible = false;
            this.txtEditAddNote.Lines = new string[0];
            this.txtEditAddNote.Location = new System.Drawing.Point(23, 118);
            this.txtEditAddNote.MaxLength = 32767;
            this.txtEditAddNote.Multiline = true;
            this.txtEditAddNote.Name = "txtEditAddNote";
            this.txtEditAddNote.PasswordChar = '\0';
            this.txtEditAddNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEditAddNote.SelectedText = "";
            this.txtEditAddNote.SelectionLength = 0;
            this.txtEditAddNote.SelectionStart = 0;
            this.txtEditAddNote.ShortcutsEnabled = true;
            this.txtEditAddNote.Size = new System.Drawing.Size(400, 137);
            this.txtEditAddNote.TabIndex = 0;
            this.txtEditAddNote.UseSelectable = true;
            this.txtEditAddNote.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtEditAddNote.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnEditAddNote
            // 
            this.btnEditAddNote.Location = new System.Drawing.Point(337, 267);
            this.btnEditAddNote.Name = "btnEditAddNote";
            this.btnEditAddNote.Size = new System.Drawing.Size(86, 23);
            this.btnEditAddNote.TabIndex = 1;
            this.btnEditAddNote.Text = "&Add";
            this.btnEditAddNote.UseSelectable = true;
            // 
            // AddNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 313);
            this.Controls.Add(this.btnEditAddNote);
            this.Controls.Add(this.txtEditAddNote);
            this.Controls.Add(this.txtEditNoteDate);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddNote";
            this.Text = "Add Note";
            ((System.ComponentModel.ISupportInitialize)(this.msmAddNote)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Components.MetroStyleManager msmAddNote;
        private MetroFramework.Controls.MetroButton btnEditAddNote;
        private MetroFramework.Controls.MetroTextBox txtEditAddNote;
        private MetroFramework.Controls.MetroTextBox txtEditNoteDate;
    }
}