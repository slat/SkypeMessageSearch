namespace SkypeMessageSearch
{
    partial class MainView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this._grid = new System.Windows.Forms.DataGridView();
            this._dateFrom = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._dateTo = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this._checkMessageToggle = new System.Windows.Forms.CheckBox();
            this._txtSearch = new SkypeMessageSearch.SearchTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._labelFooter = new System.Windows.Forms.ToolStripStatusLabel();
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this._btnContactClear = new System.Windows.Forms.Button();
            this._comboContact = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // _grid
            // 
            this._grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._grid.Location = new System.Drawing.Point(12, 74);
            this._grid.Name = "_grid";
            this._grid.Size = new System.Drawing.Size(997, 350);
            this._grid.TabIndex = 4;
            // 
            // _dateFrom
            // 
            this._dateFrom.Checked = false;
            this._dateFrom.Location = new System.Drawing.Point(6, 22);
            this._dateFrom.Name = "_dateFrom";
            this._dateFrom.ShowCheckBox = true;
            this._dateFrom.Size = new System.Drawing.Size(210, 23);
            this._dateFrom.TabIndex = 0;
            this._dateFrom.ValueChanged += new System.EventHandler(this._dateFrom_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._dateFrom);
            this.groupBox1.Location = new System.Drawing.Point(553, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 56);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Minimum Date Filter";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this._dateTo);
            this.groupBox2.Location = new System.Drawing.Point(784, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(225, 56);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Maximum Date Filter";
            // 
            // _dateTo
            // 
            this._dateTo.Checked = false;
            this._dateTo.Location = new System.Drawing.Point(6, 22);
            this._dateTo.Name = "_dateTo";
            this._dateTo.ShowCheckBox = true;
            this._dateTo.Size = new System.Drawing.Size(210, 23);
            this._dateTo.TabIndex = 0;
            this._dateTo.ValueChanged += new System.EventHandler(this._dateTo_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this._checkMessageToggle);
            this.groupBox3.Controls.Add(this._txtSearch);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(304, 56);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Message Filter";
            // 
            // _checkMessageToggle
            // 
            this._checkMessageToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._checkMessageToggle.Appearance = System.Windows.Forms.Appearance.Button;
            this._checkMessageToggle.Checked = true;
            this._checkMessageToggle.CheckState = System.Windows.Forms.CheckState.Checked;
            this._checkMessageToggle.Location = new System.Drawing.Point(260, 20);
            this._checkMessageToggle.Name = "_checkMessageToggle";
            this._checkMessageToggle.Size = new System.Drawing.Size(38, 25);
            this._checkMessageToggle.TabIndex = 1;
            this._checkMessageToggle.Text = "And";
            this._checkMessageToggle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._checkMessageToggle.UseVisualStyleBackColor = true;
            this._checkMessageToggle.CheckedChanged += new System.EventHandler(this._checkMessageToggle_CheckedChanged);
            // 
            // _txtSearch
            // 
            this._txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._txtSearch.Location = new System.Drawing.Point(6, 22);
            this._txtSearch.Name = "_txtSearch";
            this._txtSearch.Placeholder = "Search Message";
            this._txtSearch.Size = new System.Drawing.Size(248, 23);
            this._txtSearch.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._labelFooter});
            this.statusStrip1.Location = new System.Drawing.Point(0, 427);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1021, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // _labelFooter
            // 
            this._labelFooter.Name = "_labelFooter";
            this._labelFooter.Size = new System.Drawing.Size(78, 17);
            this._labelFooter.Text = "Select a Profile";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this._btnContactClear);
            this.groupBox4.Controls.Add(this._comboContact);
            this.groupBox4.Location = new System.Drawing.Point(322, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(225, 56);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Contact Filter";
            // 
            // _btnContactClear
            // 
            this._btnContactClear.Image = global::SkypeMessageSearch.Properties.Resources.clear;
            this._btnContactClear.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._btnContactClear.Location = new System.Drawing.Point(193, 23);
            this._btnContactClear.Name = "_btnContactClear";
            this._btnContactClear.Size = new System.Drawing.Size(26, 22);
            this._btnContactClear.TabIndex = 1;
            this._btnContactClear.UseVisualStyleBackColor = true;
            this._btnContactClear.Click += new System.EventHandler(this._btnContactClear_Click);
            // 
            // _comboContact
            // 
            this._comboContact.FormattingEnabled = true;
            this._comboContact.Location = new System.Drawing.Point(7, 23);
            this._comboContact.Name = "_comboContact";
            this._comboContact.Size = new System.Drawing.Size(180, 23);
            this._comboContact.TabIndex = 0;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 449);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._grid);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainView";
            this.Text = "Skype Message Search";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainView_Load);
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView _grid;
        private System.Windows.Forms.DateTimePicker _dateFrom;
        private SearchTextBox _txtSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker _dateTo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _labelFooter;
        private System.Windows.Forms.ToolTip _toolTip;
        private System.Windows.Forms.CheckBox _checkMessageToggle;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox _comboContact;
        private System.Windows.Forms.Button _btnContactClear;
    }
}

