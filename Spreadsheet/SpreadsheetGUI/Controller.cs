using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SSGui;
using SS;
using System.Windows.Forms;
using Formulas;
using System.IO;

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
        private string currentValue;
        private string currentContent;
        private ISet<string> cellsToChange;

        /// <summary>
        /// Begins controlling window.
        /// </summary>
        public Controller(ISSWindowView window)
        {
            this.window = window;
            this.model = new Spreadsheet();
            currentName = "A1";
            window.ChangeCurrentEvent += ChangeCurrent;
            window.ChangeCellContentEvent += ChangeCellContent;
            window.RetrieveEditBoxValueEvent += RetrieveEditBoxValue;
            window.UpdateRelevantEvent += UpdateRelevantCells;
            window.MoveLeftEvent += MoveLeft;
            window.MoveRightEvent += MoveRight;
            window.MoveUpEvent += MoveUp;
            window.MoveDownEvent += MoveDown;
            window.NewSpreadsheetEvent += NewSpreadsheet;
            window.OpenSpreadsheetEvent += OpenSpreadsheet;
            window.SaveSpreadsheetEvent += SaveSpreadsheet;
        }

        /// <summary>
        /// Takes a speadsheet panel as a parameter and sets the currently selected cell.
        /// </summary>
        /// <param name="sender"></param>
        private void ChangeCurrent(SpreadsheetPanel sender)
        {
            currentPanel = sender;
            sender.GetSelection(out col, out row);
            currentName = (alphabet[col] + (row+1).ToString());
            sender.GetValue(col, row, out currentValue);
            object desiredContent = model.GetCellContents(currentName);
            if (desiredContent is Formula)
            {
                currentContent = "=" + desiredContent.ToString();
            }
            else
                currentContent = desiredContent.ToString();

            window.CellNameBoxVal = currentName;
            window.CellValueBoxVal = currentValue;
        }

        /// <summary>
        /// changes a currently selected cells value.
        /// </summary>
        /// <param name="content"></param>
        private void ChangeCellContent(string content)
        {
            cellsToChange = model.SetContentsOfCell(currentName, content); // Set contents in spreadsheet
            // Obtain value from cells (calced by above), conv to string, set it to display
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

        private void RetrieveEditBoxValue(TextBox t)
        {
            t.Text = currentContent;
        }

        /// <summary>
        /// Updates dependees of a currently selected cell.
        /// </summary>
        private void UpdateRelevantCells()
        {
            foreach (string s in cellsToChange)
            {
                int thisCol = s[0] - 65;
                int thisRow;
                int.TryParse(s.Substring(1), out thisRow);
                thisRow--;
                

                // Get the value in the cell and convert it to appropriate string
                object valueTemp = model.GetCellValue(s);
                String value;
                if (valueTemp is string)
                {
                    value = (string)valueTemp;
                }
                else if (valueTemp is double)
                {
                    value = valueTemp.ToString();
                }
                else if (valueTemp is FormulaError)
                    value = "Formula Error";
                else
                    value = "";

                currentPanel.SetValue(thisCol, thisRow, value);
            }
        }

        private void MoveLeft(SpreadsheetPanel sender)
        {
            if(sender.SetSelection(col - 1, row))
            {
                col -= 1;
            }
        }
        private void MoveRight(SpreadsheetPanel sender)
        {
            if (sender.SetSelection(col + 1, row))
            {
                col += 1;
            }
        }
        private void MoveUp(SpreadsheetPanel sender)
        {
            if (sender.SetSelection(col, row - 1))
            {
                row -= 1;
            }
        }
        private void MoveDown(SpreadsheetPanel sender)
        {
            if(sender.SetSelection(col, row + 1))
            {
                row += 1;
            }
        }

        private void NewSpreadsheet()
        {
            window.OpenNew();
        }

        private void OpenSpreadsheet(string filename)
        {
            SSWindow newSpreadsheet = new SSWindow();
            newSpreadsheet.OpenSS();
        }


        private void SaveSpreadsheet(string filename)
        {
            StreamWriter f = new StreamWriter(filename);
            StringWriter sw = new StringWriter();
            model.Save(sw);
            f.Write(sw);
            f.Close();
        }
    }
}
