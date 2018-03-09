using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public event Action<string> SaveSpreadsheetEvent;
        public event Action NewSpreadsheetEvent;
        public event Action<string> OpenSpreadsheetEvent;
        public event Action<string, SSWindow> UpdateTitleTextEvent;
        public event Action UpdateAllNonEmptyEvent;

        public SSWindow()
        {
            InitializeComponent();
        }

        //TEST
        public SSWindow(SSWindow oldWindow)
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

        public void OpenSS(string filename)
        {
            SSWindowApplicationContext.GetContext().RunNew(filename);
           }

        public string TitleTextVal
        {
            set { this.Text = value; }
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
                if (UpdateTitleTextEvent != null)
                {
                    UpdateTitleTextEvent("", this);
                }
                if (UpdateTitleTextEvent != null)
                {
                    UpdateTitleTextEvent("", this);
                }
                if (UpdateRelevantEvent != null)
                {
                    UpdateRelevantEvent();
                }
                ChangeCurrentEvent(TheSpreadsheetPanel);
            }
        }

        /// <summary>
        /// The 'new' button is clicked from file menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NewSpreadsheetEvent != null)
            {
                NewSpreadsheetEvent();
            }
        }
        /// <summary>
        /// The 'open' button is clicked from file menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// The 'Save To' button is clicked from file menu. Spreadsheet will be saved as
        /// and XML file with location and name as specified in the save dialog.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = saveDialog.ShowDialog();
            if (result == DialogResult.Yes || result == DialogResult.OK)
            {
                if (SaveSpreadsheetEvent != null)
                {
                    SaveSpreadsheetEvent(saveDialog.FileName);
                }
                if (UpdateTitleTextEvent != null)
                {
                    UpdateTitleTextEvent(saveDialog.FileName, this);
                }
            }
        }

        /// <summary>
        /// The 'Save' button is clicked from file menu. Spreadsheet will be saved to
        /// previous filepath is one has been selected, or will open save dialog if the
        /// sheet has not been saved since its inception.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveRegularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.Text.Contains(".")) { // If title text is a filepath, meaning sheet has been saved
                string filepath;
                if (this.Text.EndsWith("*")) // If the path defined in title ends with '*',
                {
                    filepath = this.Text.Remove(this.Text.Length-1); // Remove it from filepath
                }
                else
                {
                    filepath = this.Text; // Otherwise, set the title to filepath
                }
                if (SaveSpreadsheetEvent != null)
                {
                    SaveSpreadsheetEvent(filepath);
                }
                if (UpdateTitleTextEvent != null)
                {
                    UpdateTitleTextEvent(filepath, this);
                }
            }
            else // If sheet has not been saved, act as if 'Save To' was pressed (bring up save dialog)
            {
                saveToolStripMenuItem_Click(sender, e);
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void TheSpreadsheetPanel_KeyDown(object sender, KeyEventArgs e)
        {
            
                if (e.KeyCode == Keys.Left) // Left arrow pressed
                {
                    if (MoveLeftEvent != null)
                    {
                        MoveLeftEvent((SpreadsheetPanel) sender);
                        TheSpreadsheetPanel_SelectionChanged(TheSpreadsheetPanel);
                    }
                }
                else if (e.KeyCode == Keys.Right) // Right arrow pressed
                {
                    if (MoveRightEvent != null)
                    {
                        MoveRightEvent((SpreadsheetPanel) sender);
                        TheSpreadsheetPanel_SelectionChanged(TheSpreadsheetPanel);
                    }
                }
                else if (e.KeyCode == Keys.Up) // Up arrow pressed
                {
                    if (MoveUpEvent != null)
                    {
                        MoveUpEvent((SpreadsheetPanel) sender);
                        TheSpreadsheetPanel_SelectionChanged(TheSpreadsheetPanel);
                    }
                }
                else if (e.KeyCode == Keys.Down) // Down arrow pressed
                {
                    if (MoveDownEvent != null)
                    {
                        MoveDownEvent((SpreadsheetPanel) sender);
                        TheSpreadsheetPanel_SelectionChanged(TheSpreadsheetPanel);
                    }
                }

            
        }

        private void EditBox_KeyDown(object sender, KeyEventArgs e)
        {   // If the key down was an arrow key
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

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Help Instructions: \n" +
                            "\n" +
                            "Select any cell with the mouse or arrow keys. \n" +
                            "\n" +
                            "Cells may contain any string or number, or a  \n" +
                            "combination of both. \n" +
                            "\n" +
                            "A valid formula may be entered in any cell    \n" +
                            "using the '=' sign to designate it a formula. \n" +
                            "\n" +
                            "A valid formula is any valid mathimatical  ex.\n" +
                            "limited to these characters: (,),+,-,*,/.     \n" +
                            "normal math rules apply (no dividing by zero) \n" +
                            "\n" +
                            "Any other cell's value may be used by using   \n" +
                            "that cells column and row name in this format:\n" +
                            "\n" +
                            "                   A1, X22, B34, etc...       \n" +
                            "\n" +
                            "This spreadsheet may be saved using the File  \n" +
                            "Menu on the top left; you may also use the    \n" +
                            "menu to open a saved worksheet or open a fresh\n" +
                            "page.\n" +
                            "                   Thank You!");
        }

        private void SSWindow_Shown(object sender, EventArgs e)
        {
            if (UpdateAllNonEmptyEvent != null)
            {
                UpdateAllNonEmptyEvent();
            }
        }
    }
}
