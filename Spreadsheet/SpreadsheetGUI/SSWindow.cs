using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SS;
using SSGui;

/// <summary>
/// GUI for the Spreadsheet application. Handles events and sends them to the controller. 
/// Writen by Chris Hanson, and Bryce Hansen 3/7/18
/// </summary>
namespace SpreadsheetGUI
{
    public partial class SSWindow : Form, ISSWindowView
    {
        public event Action<SpreadsheetPanel> ChangeCurrentEvent;
        public event Action<string> ChangeCellContentEvent;
        public event Action<TextBox> RetrieveEditBoxValueEvent;
        public event Action UpdateRelevantEvent;
        public event Action<SpreadsheetPanel> MoveLeftEvent;
        public event Action<SpreadsheetPanel> MoveRightEvent;
        public event Action<SpreadsheetPanel> MoveUpEvent;
        public event Action<SpreadsheetPanel> MoveDownEvent;
        public event Action SaveSpreadsheetEvent;
        public event Action LoadEvent;
        public event Action NewSpreadsheetEvent;
        public event Action<string> OpenSpreadsheetEvent;
        public event Action CloseEvent;

        public SSWindow()
        {
            InitializeComponent();
        }

        public void DoClose()
        {
            Close();
        }

        public void OpenNew()
        {
            SSWindowApplicationContext.GetContext().RunNew();
        }

        public string CellNameBoxVal
        {
            set { CellNameBox.Text = value; }
        }

        public string CellValueBoxVal
        {
            set { CellValueBox.Text = value; }
        }

        /// <summary>
        /// Sends the event to the controller that sets the values of the cell previous to the one selected.
        /// </summary>
        /// <param name="sender"></param>
        private void TheSpreadsheetPanel_SelectionChanged(SpreadsheetPanel sender)
        {
            if (ChangeCellContentEvent != null)
            {
                ChangeCellContentEvent(EditBox.Text);
            }
            if (UpdateRelevantEvent != null)
            {
                UpdateRelevantEvent();
            }
            if (ChangeCurrentEvent != null)
            {
                ChangeCurrentEvent(sender);
            }
            if (RetrieveEditBoxValueEvent != null)
            {
                RetrieveEditBoxValueEvent(EditBox);
            }
        }

        /// <summary>
        /// Sends the text of the editor box to be changed to the controller.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditBox_TextChanged(object sender, EventArgs e)
        {
            if (ChangeCellContentEvent != null && !EditBox.Text.StartsWith("="))
            {
                ChangeCellContentEvent(EditBox.Text);
            }
        }

        private void TheSpreadsheetPanel_Load(object sender, EventArgs e)
        {
            if (ChangeCurrentEvent != null)
            {
                ChangeCurrentEvent((SpreadsheetPanel) sender);
            }
        }
        

        /// <summary>
        /// Allows use of the "Enter" key to submit a value to be updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditBox_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Return)
            {
                if (ChangeCellContentEvent != null)
                {
                    ChangeCellContentEvent(EditBox.Text);
                }
                if (UpdateRelevantEvent != null)
                {
                    UpdateRelevantEvent();
                }
                ChangeCurrentEvent(TheSpreadsheetPanel);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NewSpreadsheetEvent != null)
            {
                NewSpreadsheetEvent();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.Yes || result == DialogResult.OK)
            {
                if (OpenSpreadsheetEvent != null)
                {
                    OpenSpreadsheetEvent(fileDialog.FileName);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveSpreadsheetEvent != null)
            {
                SaveSpreadsheetEvent();
            }
        }

        private void CellNameBox_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void TheSpreadsheetPanel_KeyDown(object sender, KeyEventArgs e)
        {
            
                if (e.KeyCode == Keys.Left)
                {
                    if (MoveLeftEvent != null)
                    {
                        MoveLeftEvent((SpreadsheetPanel) sender);
                        TheSpreadsheetPanel_SelectionChanged(TheSpreadsheetPanel);
                    }
                }
                else if (e.KeyCode == Keys.Right)
                {
                    if (MoveRightEvent != null)
                    {
                        MoveRightEvent((SpreadsheetPanel) sender);
                        TheSpreadsheetPanel_SelectionChanged(TheSpreadsheetPanel);
                    }
                }
                else if (e.KeyCode == Keys.Up)
                {
                    if (MoveUpEvent != null)
                    {
                        MoveUpEvent((SpreadsheetPanel) sender);
                        TheSpreadsheetPanel_SelectionChanged(TheSpreadsheetPanel);
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    if (MoveDownEvent != null)
                    {
                        MoveDownEvent((SpreadsheetPanel) sender);
                        TheSpreadsheetPanel_SelectionChanged(TheSpreadsheetPanel);
                    }
                }

            
        }

        private void EditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                TheSpreadsheetPanel_KeyDown(TheSpreadsheetPanel, e);

                if (ChangeCellContentEvent != null)
                {
                    ChangeCellContentEvent(EditBox.Text);
                }

                if (UpdateRelevantEvent != null)
                {
                    UpdateRelevantEvent();
                }

                ChangeCurrentEvent(TheSpreadsheetPanel);
            }

        }

        private void SSWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
