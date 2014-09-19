namespace SkypeMessageSearch
{
    partial class ProfileSelectView
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
            this.panel1 = new System.Windows.Forms.Panel();
            this._btnOk = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this._linkCancel = new System.Windows.Forms.LinkLabel();
            this._comboProfile = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._btnOk);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this._linkCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 74);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(357, 48);
            this.panel1.TabIndex = 9;
            // 
            // _btnOk
            // 
            this._btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnOk.Location = new System.Drawing.Point(195, 10);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(82, 32);
            this._btnOk.TabIndex = 0;
            this._btnOk.Text = "&OK";
            this._btnOk.Click += new System.EventHandler(this._btnOk_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(283, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 22);
            this.label3.TabIndex = 1;
            this.label3.Text = "or";
            // 
            // _linkCancel
            // 
            this._linkCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._linkCancel.Location = new System.Drawing.Point(310, 16);
            this._linkCancel.Name = "_linkCancel";
            this._linkCancel.Size = new System.Drawing.Size(33, 22);
            this._linkCancel.TabIndex = 2;
            this._linkCancel.TabStop = true;
            this._linkCancel.Text = "Exit";
            this._linkCancel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this._linkCancel_LinkClicked);
            // 
            // _comboProfile
            // 
            this._comboProfile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._comboProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboProfile.FormattingEnabled = true;
            this._comboProfile.Location = new System.Drawing.Point(101, 43);
            this._comboProfile.Name = "_comboProfile";
            this._comboProfile.Size = new System.Drawing.Size(241, 23);
            this._comboProfile.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "Displaying Profiles stored in %AppData%\\Skype";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "Select Profile";
            // 
            // ProfileSelectView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 122);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._comboProfile);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProfileSelectView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Skype Message Search | Profile Select";
            this.Load += new System.EventHandler(this.ProfileSelectView_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button _btnOk;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel _linkCancel;
        private System.Windows.Forms.ComboBox _comboProfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}