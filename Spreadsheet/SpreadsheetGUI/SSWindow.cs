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
        public event Action SaveEvent;
        public event Action LoadEvent;
        public event Action NewEvent;
        public event Action OpenEvent;
        public event Action CloseEvent;
        public event Action<SpreadsheetPanel> SelectUp;
        public event Action<SpreadsheetPanel> SelectDown;
        public event Action<SpreadsheetPanel> SelectRight;
        public event Action<SpreadsheetPanel> SelectLeft;


        public SSWindow()
        {
            InitializeComponent();
        }

        public void DoClose()
        {
            Close();
        }

        public string CellNameBoxVal
        {
            set { CellNameBox.Text = value; }

        }

        public string CellValueBoxVal
        {
            set { ValueBox.Text = value; }
        }

        public void OpenNew()
        {
            SSWindowApplicationContext.GetContext().RunNew();
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

        private void EditBox_Enter(object sender, EventArgs e)
        {

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
            if (e.KeyChar == (char)Keys.NumPad8)
            {
                SelectUp((SpreadsheetPanel) sender);
            }
            else if (e.KeyChar == (char)Keys.NumPad2)
            {
                SelectDown((SpreadsheetPanel) sender);
            }
            else if (e.KeyChar == (char)Keys.NumPad6)
            {
                SelectRight((SpreadsheetPanel) sender);
            }
            else if (e.KeyChar == (char)Keys.NumPad4)
            {
                SelectLeft((SpreadsheetPanel) sender);
            }
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SSWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
