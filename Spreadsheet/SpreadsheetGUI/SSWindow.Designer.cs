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
            this.EditBox = new System.Windows.Forms.TextBox();
            this.CellNameBox = new System.Windows.Forms.TextBox();
            this.ValueBox = new System.Windows.Forms.TextBox();
            this.TheSpreadsheetPanel = new SSGui.SpreadsheetPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // EditBox
            // 
            this.EditBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditBox.Location = new System.Drawing.Point(2, 32);
            this.EditBox.Margin = new System.Windows.Forms.Padding(2);
            this.EditBox.Name = "EditBox";
            this.EditBox.Size = new System.Drawing.Size(1211, 20);
            this.EditBox.TabIndex = 1;
            this.EditBox.TextChanged += new System.EventHandler(this.EditBox_TextChanged);
            this.EditBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EditBox_KeyPress);
            // 
            // CellNameBox
            // 
            this.CellNameBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CellNameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CellNameBox.Location = new System.Drawing.Point(3, 3);
            this.CellNameBox.Name = "CellNameBox";
            this.CellNameBox.ReadOnly = true;
            this.CellNameBox.Size = new System.Drawing.Size(44, 20);
            this.CellNameBox.TabIndex = 2;
            // 
            // ValueBox
            // 
            this.ValueBox.AccessibleName = "";
            this.ValueBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ValueBox.Location = new System.Drawing.Point(53, 3);
            this.ValueBox.Name = "ValueBox";
            this.ValueBox.ReadOnly = true;
            this.ValueBox.Size = new System.Drawing.Size(1153, 20);
            this.ValueBox.TabIndex = 3;
            // 
            // TheSpreadsheetPanel
            // 
            this.TheSpreadsheetPanel.AutoSize = true;
            this.TheSpreadsheetPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TheSpreadsheetPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TheSpreadsheetPanel.Location = new System.Drawing.Point(2, 62);
            this.TheSpreadsheetPanel.Margin = new System.Windows.Forms.Padding(2);
            this.TheSpreadsheetPanel.Name = "TheSpreadsheetPanel";
            this.TheSpreadsheetPanel.Size = new System.Drawing.Size(1211, 714);
            this.TheSpreadsheetPanel.TabIndex = 0;
            this.TheSpreadsheetPanel.SelectionChanged += new SSGui.SelectionChangedHandler(this.TheSpreadsheetPanel_SelectionChanged);
            this.TheSpreadsheetPanel.Load += new System.EventHandler(this.TheSpreadsheetPanel_Load);
            this.TheSpreadsheetPanel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TheSpreadsheetPanel_KeyPress);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.TheSpreadsheetPanel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.EditBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1215, 772);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.CellNameBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.ValueBox, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1209, 24);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // SSWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 772);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SSWindow";
            this.Text = "Spreadsheet";
            this.Load += new System.EventHandler(this.SSWindow_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox EditBox;
        private System.Windows.Forms.TextBox CellNameBox;
        private System.Windows.Forms.TextBox ValueBox;
        private SSGui.SpreadsheetPanel TheSpreadsheetPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}

