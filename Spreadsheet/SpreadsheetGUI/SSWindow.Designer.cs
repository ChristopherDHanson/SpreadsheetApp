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
            this.TheSpreadsheetPanel = new SSGui.SpreadsheetPanel();
            this.SuspendLayout();
            // 
            // EditBox
            // 
            this.EditBox.Location = new System.Drawing.Point(9, 33);
            this.EditBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.EditBox.Name = "EditBox";
            this.EditBox.Size = new System.Drawing.Size(265, 20);
            this.EditBox.TabIndex = 1;
            this.EditBox.TextChanged += new System.EventHandler(this.EditBox_TextChanged);
            // 
            // TheSpreadsheetPanel
            // 
            this.TheSpreadsheetPanel.Location = new System.Drawing.Point(9, 56);
            this.TheSpreadsheetPanel.Margin = new System.Windows.Forms.Padding(2);
            this.TheSpreadsheetPanel.Name = "TheSpreadsheetPanel";
            this.TheSpreadsheetPanel.Size = new System.Drawing.Size(560, 286);
            this.TheSpreadsheetPanel.TabIndex = 0;
            this.TheSpreadsheetPanel.SelectionChanged += new SSGui.SelectionChangedHandler(this.TheSpreadsheetPanel_SelectionChanged);
            this.TheSpreadsheetPanel.Load += new System.EventHandler(this.TheSpreadsheetPanel_Load);
            // 
            // SSWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 306);
            this.Controls.Add(this.EditBox);
            this.Controls.Add(this.TheSpreadsheetPanel);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SSWindow";
            this.Text = "Spreadsheet";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SSGui.SpreadsheetPanel TheSpreadsheetPanel;
        private System.Windows.Forms.TextBox EditBox;
    }
}

