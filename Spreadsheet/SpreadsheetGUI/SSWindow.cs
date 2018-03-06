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
        public event Action SaveEvent;
        public event Action DirectionPressEvent;
        public event Action LoadEvent;
        public event Action NewEvent;
        public event Action OpenEvent;
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

        private void TheSpreadsheetPanel_SelectionChanged(SpreadsheetPanel sender)
        {

            if (ChangeCellContentEvent != null)
            {
                ChangeCellContentEvent(EditBox.Text);
            }

            if (ChangeCurrentEvent != null)
            {
                ChangeCurrentEvent(sender);
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
    }
}
