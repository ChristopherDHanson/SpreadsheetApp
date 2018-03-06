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
        private string currentName;
        private int col;
        private int row;
        private SpreadsheetPanel currentPanel;
        private char[] alphabet = {'A','B','C','D','E','F','G','H','I','J','K','L',
            'M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};

        /// <summary>
        /// Begins controlling window.
        /// </summary>
        public Controller(ISSWindowView window)
        {
            this.window = window;
            this.model = new Spreadsheet();
            currentName = "A1";
            //window.FileChosenEvent += HandleFileChosen;
            //window.CloseEvent += HandleClose;
            //window.NewEvent += HandleNew;
            //window.CountEvent += HandleCount;
            window.ChangeCurrentEvent += ChangeCurrent;
            window.ChangeCellContentEvent += ChangeCellContent;
        }

        private void ChangeCurrent(SpreadsheetPanel sender)
        {
            currentPanel = sender;
            sender.GetSelection(out col, out row);
            currentName = (alphabet[col] + (row+1).ToString());
        }

        private void ChangeCellContent(string content)
        {
            model.SetContentsOfCell(currentName, content);
            object valueTemp = model.GetCellValue(currentName);
            String value;
            if (valueTemp is string)
            {
                value = (string)valueTemp;
            } else if (valueTemp is double)
            {
                value = valueTemp.ToString();
            }
            else if (valueTemp is FormulaError)
                value = "Formula Error";
            else
                value = "";
            currentPanel.SetValue(col, row, value);
        }
    }
}
