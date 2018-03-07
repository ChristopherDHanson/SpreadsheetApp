namespace SpreadsheetGUI
{
    partial class SSWindow
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
            this.TheSpreadsheetPanel = new SSGui.SpreadsheetPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CellNameBox = new System.Windows.Forms.TextBox();
            this.EditBox = new System.Windows.Forms.TextBox();
            this.CellValueBox = new System.Windows.Forms.TextBox();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TheSpreadsheetPanel
            // 
            this.TheSpreadsheetPanel.AutoSize = true;
            this.TheSpreadsheetPanel.Location = new System.Drawing.Point(12, 98);
            this.TheSpreadsheetPanel.Name = "TheSpreadsheetPanel";
            this.TheSpreadsheetPanel.Size = new System.Drawing.Size(771, 488);
            this.TheSpreadsheetPanel.TabIndex = 0;
            this.TheSpreadsheetPanel.SelectionChanged += new SSGui.SelectionChangedHandler(this.TheSpreadsheetPanel_SelectionChanged);
            this.TheSpreadsheetPanel.Load += new System.EventHandler(this.TheSpreadsheetPanel_Load);
            this.TheSpreadsheetPanel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TheSpreadsheetPanel_KeyPress);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(788, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.openToolStripMenuItem.Text = "Open ..";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // CellNameBox
            // 
            this.CellNameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CellNameBox.Location = new System.Drawing.Point(13, 31);
            this.CellNameBox.Name = "CellNameBox";
            this.CellNameBox.ReadOnly = true;
            this.CellNameBox.Size = new System.Drawing.Size(41, 22);
            this.CellNameBox.TabIndex = 2;
            // 
            // EditBox
            // 
            this.EditBox.Location = new System.Drawing.Point(12, 58);
            this.EditBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EditBox.Name = "EditBox";
            this.EditBox.Size = new System.Drawing.Size(352, 22);
            this.EditBox.TabIndex = 1;
            this.EditBox.TextChanged += new System.EventHandler(this.EditBox_TextChanged);
            this.EditBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EditBox_KeyPress);
            // 
            // CellValueBox
            // 
            this.CellValueBox.Location = new System.Drawing.Point(61, 31);
            this.CellValueBox.Name = "CellValueBox";
            this.CellValueBox.ReadOnly = true;
            this.CellValueBox.Size = new System.Drawing.Size(303, 22);
            this.CellValueBox.TabIndex = 3;
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "fileDialog";
            // 
            // SSWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 590);
            this.Controls.Add(this.CellValueBox);
            this.Controls.Add(this.EditBox);
            this.Controls.Add(this.CellNameBox);
            this.Controls.Add(this.TheSpreadsheetPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SSWindow";
            this.Text = "Spreadsheet";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SSGui.SpreadsheetPanel TheSpreadsheetPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.TextBox CellNameBox;
        private System.Windows.Forms.TextBox EditBox;
        private System.Windows.Forms.TextBox CellValueBox;
        private System.Windows.Forms.OpenFileDialog fileDialog;
    }
}

