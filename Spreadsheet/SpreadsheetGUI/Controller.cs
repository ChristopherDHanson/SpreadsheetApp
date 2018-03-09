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
using System.Text.RegularExpressions;

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
        private string theFilename;

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
            window.UpdateTitleTextEvent += UpdateTitleText;
            window.UpdateAllNonEmptyEvent += UpdateAllNonEmpty;
        }

        /// <summary>
        /// Begins controlling window. This constructor takes in a filename
        /// </summary>
        public Controller(ISSWindowView window, string filename)
        {
            this.window = window;
            StreamReader sr = new StreamReader(filename);
            Regex r = new Regex(".*");
            this.model = new Spreadsheet(sr, r);
            currentName = "A1";
            theFilename = filename;
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
            window.UpdateTitleTextEvent += UpdateTitleText;
            window.UpdateAllNonEmptyEvent += UpdateAllNonEmpty;
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
            String value;
            try
            {
                cellsToChange = model.SetContentsOfCell(currentName, content); // Set contents in spreadsheet
                //cellsToChange.Remove(currentName);
                // Obtain value from cells (calced by above), conv to string, set it to display
                object valueTemp = model.GetCellValue(currentName);
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

                currentPanel.SetValue(col, row, value);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                value = "";
                cellsToChange = new HashSet<string>();
            }
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

        /// <summary>
        /// Move selection to the left
        /// </summary>
        private void MoveLeft(SpreadsheetPanel sender)
        {
            if(sender.SetSelection(col - 1, row))
            {
                col -= 1;
            }
        }
        /// <summary>
        /// Move selection to the right
        /// </summary>
        private void MoveRight(SpreadsheetPanel sender)
        {
            if (sender.SetSelection(col + 1, row))
            {
                col += 1;
            }
        }
        /// <summary>
        /// Move selection up
        /// </summary>
        private void MoveUp(SpreadsheetPanel sender)
        {
            if (sender.SetSelection(col, row - 1))
            {
                row -= 1;
            }
        }
        /// <summary>
        /// Move selection down
        /// </summary>
        private void MoveDown(SpreadsheetPanel sender)
        {
            if(sender.SetSelection(col, row + 1))
            {
                row += 1;
            }
        }

        /// <summary>
        /// Opens blank spreadsheet in new window, using method in
        /// SSWindow.
        /// </summary>
        private void NewSpreadsheet()
        {
            window.OpenNew();
        }

        /// <summary>
        /// Open new spreadsheet, getting info from path specified in
        /// 'filename'
        /// </summary>
        /// <param name="filename"></param>
        private void OpenSpreadsheet(string filename)
        {
            SSWindow newSpreadsheet = new SSWindow();
            newSpreadsheet.OpenSS(filename);
        }

        /// <summary>
        /// Save spreadsheet to filepath specified by 'filename'
        /// </summary>
        /// <param name="filename"></param>
        private void SaveSpreadsheet(string filename)
        {
            StreamWriter f = new StreamWriter(filename);
            StringWriter sw = new StringWriter();
            model.Save(sw);
            f.Write(sw);
            f.Close();
            theFilename = filename;
        }

        /// <summary>
        /// Changes the title of window to specified string, or adds asterisk
        /// is spreadsheet has been changed.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="window"></param>
        private void UpdateTitleText(string filename, SSWindow window)
        {
            if (model.Changed)
            {
                if (!window.Text.EndsWith("*")) {
                    window.Text = window.Text + "*";
                }
            }
            else
            {
                window.Text = filename;
            }
        }

        private void UpdateAllNonEmpty()
        {
            ISet<string> set = new HashSet<string>();
            foreach(string s in model.GetNamesOfAllNonemptyCells())
            {
                set.Add(s);
            }
            cellsToChange = set;
            UpdateRelevantCells();
        }
    }
}
