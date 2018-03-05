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
        public event Action<SpreadsheetPanel> SelectionChangedEvent;
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

        private void TheSpreadsheetPanel_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void TheSpreadsheetPanel_SelectionChanged(SpreadsheetPanel sender, EventArgs e)
        {
            //sender.GetSelection(out int column, out int row);
            //sender.SetValue(column, row, "ok");
            if (SelectionChangedEvent != null)
            {
                SelectionChangedEvent(sender);
            }
        }
    }
}
