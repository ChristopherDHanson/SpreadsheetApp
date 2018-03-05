using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSGui;
using SS;

namespace SpreadsheetGUI
{
    class Controller
    {
        private Spreadsheet model;
        private ISSWindowView window;

        /// <summary>
        /// Begins controlling window.
        /// </summary>
        public Controller(ISSWindowView window)
        {
            this.window = window;
            this.model = new Spreadsheet();
            //window.FileChosenEvent += HandleFileChosen;
            //window.CloseEvent += HandleClose;
            //window.NewEvent += HandleNew;
            //window.CountEvent += HandleCount;
            window.SelectionChangedEvent += HandleSelectionChanged;
        }

        private void HandleSelectionChanged(SpreadsheetPanel sender)
        {
            sender.GetSelection(out int column, out int row);
            sender.SetValue(column, row, "ok");
        }


    }
}
