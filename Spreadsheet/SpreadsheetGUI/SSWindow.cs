using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SSGui;

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
            }
        }

        private void TheSpreadsheetPanel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Left)
            {
                if (MoveLeftEvent != null)
                {
                    MoveLeftEvent((SpreadsheetPanel)sender);
                }
            }
            else if (e.KeyChar == (char)Keys.Right)
            {
                if (MoveRightEvent != null)
                {
                    MoveRightEvent((SpreadsheetPanel)sender);
                }
            }
            else if (e.KeyChar == (char)Keys.Up)
            {
                if (MoveUpEvent != null)
                {
                    MoveUpEvent((SpreadsheetPanel)sender);
                }
            }
            else if (e.KeyChar == (char)Keys.Down)
            {
                if (MoveDownEvent != null)
                {
                    MoveDownEvent((SpreadsheetPanel)sender);
                }
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
    }
}
