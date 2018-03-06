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
        private char[] alphabet = {'a','b','c','d','e','f','g','h','i','j','k','l',
            'm','n','o','p','q','r','s','t','u','v','w','x','y','z'};

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
            window.ChangeCurrentEvent += ChangeCurrent;
            window.ChangeCellContentEvent += ChangeCellContent;
        }

        private void ChangeCurrent(SpreadsheetPanel sender)
        {
            currentPanel = sender;
            sender.GetSelection(out col, out row);
            sender.SetValue(col, row, "ok");
            currentName = (alphabet[col] + row.ToString());
            
        }

        private void ChangeCellContent(string content)
        {
            model.SetContentsOfCell(currentName, content);
            object valueTemp = model.GetCellValue(currentName);
            String value;
            if (valueTemp is string)
            {
                value = (string)valueTemp;
            }
            else if (valueTemp is FormulaError)
                value = "Formula Error";
            else
                value = "";
            currentPanel.SetValue(col, row, value);
        }
    }
}
