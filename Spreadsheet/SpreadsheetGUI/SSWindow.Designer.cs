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
            this.EditBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TheSpreadsheetPanel
            // 
            this.TheSpreadsheetPanel.Location = new System.Drawing.Point(12, 69);
            this.TheSpreadsheetPanel.Name = "TheSpreadsheetPanel";
            this.TheSpreadsheetPanel.Size = new System.Drawing.Size(747, 352);
            this.TheSpreadsheetPanel.TabIndex = 0;
            this.TheSpreadsheetPanel.SelectionChanged += new SSGui.SelectionChangedHandler(this.TheSpreadsheetPanel_SelectionChanged);
            // 
            // EditBox
            // 
            this.EditBox.Location = new System.Drawing.Point(12, 41);
            this.EditBox.Name = "EditBox";
            this.EditBox.Size = new System.Drawing.Size(352, 22);
            this.EditBox.TabIndex = 1;
            this.EditBox.TextChanged += new System.EventHandler(this.EditBox_TextChanged);
            // 
            // SSWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 376);
            this.Controls.Add(this.EditBox);
            this.Controls.Add(this.TheSpreadsheetPanel);
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

