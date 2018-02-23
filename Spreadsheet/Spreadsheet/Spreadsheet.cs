// Written by Christopher Hanson for CS 3500, February 2018

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dependencies;
using Formulas;

namespace SS
{
    /// <summary>
    /// A Spreadsheet is a system of related cells. A Spreadsheet keeps track of
    /// Cell names and their contents, and also uses a DependencyGraph to store
    /// relationships between cells. Further explanation is in AbstractSpreadsheet
    /// class
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {
        /// <summary>
        /// Dictionary stores association b/w cell names and Cells
        /// </summary>
        private Dictionary<string, Cell> sheet;
        /// <summary>
        /// DependencyGraph stores relationships b/w Cells
        /// </summary>
        private DependencyGraph depGraph;
        /// <summary>
        /// IsValid; Defines valid cell names
        /// </summary>
        private String validCellNamePattern = @"^[a-zA-Z]*[1-9]\d*$"; // One or more letters followed by nonzero, more digits
        /// <summary>
        /// Regex used to define valid cell beyond class definition; specified by parameter constructor
        /// </summary>
        private Regex IsValid;

        // ADDED FOR PS6
        /// <summary>
        /// True if this spreadsheet has been modified since it was created or saved
        /// (whichever happened most recently); false otherwise.
        /// </summary>
        public override bool Changed { get; protected set; }

        /// <summary>
        /// Creates an empty Spreadsheet whose IsValid regular expression accepts every string.
        /// </summary>
        public Spreadsheet()
        {
            sheet = new Dictionary<string, Cell>();
            depGraph = new DependencyGraph();
            IsValid = new Regex(@"(.* )?");
            Changed = false;
        }

        /// <summary>
        /// Creates an empty Spreadsheet whose IsValid regular expression is provided as the parameter
        /// </summary>
        public Spreadsheet(Regex isValid)
        {
            sheet = new Dictionary<string, Cell>();
            depGraph = new DependencyGraph();
            IsValid = isValid;
            Changed = false;
        }

        /// <summary>
        /// Creates a Spreadsheet that is a duplicate of the spreadsheet saved in source.
        ///
        /// See the AbstractSpreadsheet.Save method and Spreadsheet.xsd for the file format 
        /// specification.  
        ///
        /// If there's a problem reading source, throws an IOException.
        ///
        /// Else if the contents of source are not consistent with the schema in Spreadsheet.xsd, 
        /// throws a SpreadsheetReadException.  
        ///
        /// Else if the IsValid string contained in source is not a valid C# regular expression, throws
        /// a SpreadsheetReadException.  (If the exception is not thrown, this regex is referred to
        /// below as oldIsValid.)
        ///
        /// Else if there is a duplicate cell name in the source, throws a SpreadsheetReadException.
        /// (Two cell names are duplicates if they are identical after being converted to upper case.)
        ///
        /// Else if there is an invalid cell name or an invalid formula in the source, throws a 
        /// SpreadsheetReadException.  (Use oldIsValid in place of IsValid in the definition of 
        /// cell name validity.)
        ///
        /// Else if there is an invalid cell name or an invalid formula in the source, throws a
        /// SpreadsheetVersionException.  (Use newIsValid in place of IsValid in the definition of
        /// cell name validity.)
        ///
        /// Else if there's a formula that causes a circular dependency, throws a SpreadsheetReadException. 
        ///
        /// Else, create a Spreadsheet that is a duplicate of the one encoded in source except that
        /// the new Spreadsheet's IsValid regular expression should be newIsValid.
        /// </summary>
        public Spreadsheet(TextReader source, Regex newIsValid)
        {
            Changed = false;
        }

        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            List<string> names = new List<string>();
            foreach (var s in sheet) // For each key/value pair in sheet
            {
                if (s.Value.GetContents() != null) // If is technically unnecessary in this implementation,
                {                                   // but could have use under slightly different circ.
                    names.Add(s.Value.GetName()); // Add the name to List
                }
            }
            return names;
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.  The return
        /// value should be either a string, a double, or a Formula.
        /// </summary>
        public override object GetCellContents(string name)
        {
            if (name == null || !Regex.IsMatch(name, validCellNamePattern))
            {
                throw new InvalidNameException();
            }
            if (sheet.TryGetValue(name, out Cell theCell)) // If the Dictionary contains name/Cell
            {
                return theCell.GetContents(); // Return the Cell's contents
            }
            return ""; // Otherwise cell is empty, so return empty string
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes number.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<string> SetCellContents(string name, double number)
        {
            if (name == null || !Regex.IsMatch(name, validCellNamePattern))
            {
                throw new InvalidNameException();
            }

            Cell test = new Cell(); // Create the new cell that will be added
            if (sheet.TryGetValue(name, out test)) // If the Cell 'name' already has contents
            {
                sheet.Remove(name); // Remove the pair from sheet
            }
            Object oldContents = test.GetContents(); // Get the contents already in the cell
            if (oldContents is Formula) // If old contents are formula, remove relevant dependencies
            {
                Formula oldFormula = (Formula)oldContents;
                foreach (var v in oldFormula.GetVariables()) // All variables in the cell are dependees of the cell
                {
                    depGraph.RemoveDependency(v, name);
                }
            }

            test.SetContents(number);
            test.SetName(name);
            sheet.Add(name, test);

            HashSet<string> toReturn = new HashSet<string>();
            foreach (string s in GetCellsToRecalculate(name)) // Each directly or indirectly dependent cell
            {
                toReturn.Add(s);
            }
            return toReturn;
        }

        /// <summary>
        /// If text is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes text.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<string> SetCellContents(string name, string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("");
            }
            if (name == null || !Regex.IsMatch(name, validCellNamePattern))
            {
                throw new InvalidNameException();
            }

            Cell test = new Cell(); // Create new cell to add
            if (sheet.TryGetValue(name, out test)) // If cell already has contents
            {
                sheet.Remove(name); // Remove the contents
            }
            Object oldContents = test.GetContents();
            if (oldContents is Formula) // If old contents are formula, remove relevant dependencies
            {
                Formula oldFormula = (Formula)oldContents;
                foreach (var v in oldFormula.GetVariables())
                {
                    depGraph.RemoveDependency(v, name);
                }
            }

            test.SetContents(text);
            test.SetName(name);
            if (text != "") // Only add the Cell back into the sheet if the contents are not empty str
            {
                sheet.Add(name, test);
            }

            HashSet<string> toReturn = new HashSet<string>();
            foreach (string s in GetCellsToRecalculate(name))
            {
                toReturn.Add(s);
            }
            return toReturn;
        }

        /// <summary>
        /// Requires that all of the variables in formula are valid cell names.
        /// 
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException.
        /// 
        /// Otherwise, the contents of the named cell becomes formula.  The method returns a
        /// Set consisting of name plus the names of all other cells whose value depends,
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<string> SetCellContents(string name, Formula formula)
        {
            if (name == null || !Regex.IsMatch(name, validCellNamePattern))
            {
                throw new InvalidNameException();
            }

            HashSet<string> vars = (HashSet<string>)formula.GetVariables(); // Extract variables from formula
            foreach (string s in vars) // for each variable in the formula
            {
                if (!Regex.IsMatch(s, validCellNamePattern)) // if not valid cell name,
                {
                    throw new InvalidNameException(); // throw exception
                }
            }

            Cell test = new Cell();
            if (sheet.TryGetValue(name, out test)) // If cell has contents
            {
                sheet.Remove(name); // Remove contents
            }
            Object oldContents = test.GetContents();
            if (oldContents is Formula) // If old contents are formula, remove relevant dependencies
            {
                Formula oldFormula = (Formula)oldContents;
                foreach (var v in oldFormula.GetVariables())
                {
                    depGraph.RemoveDependency(v, name);
                }
            }

            try
            {
                test.SetContents(formula);
                test.SetName(name);
                sheet.Add(name, test); // Add the new Cell

                foreach (string s in vars)
                { // Add new relevant dependencies
                    depGraph.AddDependency(s, name);
                }

                HashSet<string> toReturn = new HashSet<string>();
                foreach (string s in GetCellsToRecalculate(name))
                {
                    toReturn.Add(s);
                }

                return toReturn;
            }
            catch (CircularException e)
            {
                sheet.Remove(name);
                test.SetContents(oldContents);
                sheet.Add(name, test);
                throw e;
            }
        }

        /// <summary>
        /// If name is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name isn't a valid cell name, throws an InvalidNameException.
        /// 
        /// Otherwise, returns an enumeration, without duplicates, of the names of all cells whose
        /// values depend directly on the value of the named cell.  In other words, returns
        /// an enumeration, without duplicates, of the names of all cells that contain
        /// formulas containing name.
        /// 
        /// For example, suppose that
        /// A1 contains 3
        /// B1 contains the formula A1 * A1
        /// C1 contains the formula B1 + A1
        /// D1 contains the formula B1 - C1
        /// The direct dependents of A1 are B1 and C1
        /// </summary>
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            return depGraph.GetDependents(name);
        }

        // PS6
        /// <summary>
        /// Writes the contents of this spreadsheet to dest using an XML format.
        /// The XML elements should be structured as follows:
        ///
        /// <spreadsheet IsValid="IsValid regex goes here">
        ///   <cell name="cell name goes here" contents="cell contents go here"></cell>
        ///   <cell name="cell name goes here" contents="cell contents go here"></cell>
        ///   <cell name="cell name goes here" contents="cell contents go here"></cell>
        /// </spreadsheet>
        ///
        /// The value of the IsValid attribute should be IsValid.ToString()
        /// 
        /// There should be one cell element for each non-empty cell in the spreadsheet.
        /// If the cell contains a string, the string (without surrounding double quotes) should be written as the contents.
        /// If the cell contains a double d, d.ToString() should be written as the contents.
        /// If the cell contains a Formula f, f.ToString() with "=" prepended should be written as the contents.
        ///
        /// If there are any problems writing to dest, the method should throw an IOException.
        /// </summary>
        public override void Save(TextWriter dest)
        {
            dest.WriteLine("<spreadsheet IsValid=\""+IsValid.ToString()+"\">");
            foreach (var s in sheet)
            {
                String contentsToSave;
                Object theContents = s.Value.GetContents();
                if (theContents is double)
                {
                    contentsToSave = theContents.ToString();
                }
                else if (theContents is Formula)
                {
                    contentsToSave = ((Formula)theContents).ToString();
                }
                else
                {
                    contentsToSave = (string)theContents;
                }
                dest.WriteLine("<cell name=\""+s+"\" contents=\""+contentsToSave+"\"></cell>");
            }
            dest.WriteLine("</spreadsheet>");
            Changed = false;
        }

        // ADDED FOR PS6
        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        ///
        /// Otherwise, returns the value (as opposed to the contents) of the named cell.  The return
        /// value should be either a string, a double, or a FormulaError.
        /// </summary>
        public override object GetCellValue(string name)
        {
            if (name == null || !IsValid.IsMatch(name))
            {
                throw new InvalidNameException();
            }
            return sheet[name].GetValue();
        }

        // ADDED FOR PS6
        /// <summary>
        /// If content is null, throws an ArgumentNullException.
        ///
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        ///
        /// Otherwise, if content parses as a double, the contents of the named
        /// cell becomes that double.
        ///
        /// Otherwise, if content begins with the character '=', an attempt is made
        /// to parse the remainder of content into a Formula f using the Formula
        /// constructor with s => s.ToUpper() as the normalizer and a validator that
        /// checks that s is a valid cell name as defined in the AbstractSpreadsheet
        /// class comment.  There are then three possibilities:
        ///
        ///   (1) If the remainder of content cannot be parsed into a Formula, a
        ///       Formulas.FormulaFormatException is thrown.
        ///
        ///   (2) Otherwise, if changing the contents of the named cell to be f
        ///       would cause a circular dependency, a CircularException is thrown.
        ///
        ///   (3) Otherwise, the contents of the named cell becomes f.
        ///
        /// Otherwise, the contents of the named cell becomes content.
        ///
        /// If an exception is not thrown, the method returns a set consisting of
        /// name plus the names of all other cells whose value depends, directly
        /// or indirectly, on the named cell.
        ///
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<string> SetContentsOfCell(string name, string content)
        {
            if (content == null)
            {
                throw new ArgumentNullException();
            }
            ISet<string> toReturn;
            if (double.TryParse(content, out double result))
            {
                sheet[name].SetValue(result);
                toReturn = SetCellContents(name, result);
            }
            else if (content[0].Equals('='))
            {
                Formula contentFormula = new Formula(content.Substring(1), s => s.ToUpper(), s => Regex.IsMatch(s, validCellNamePattern));
                toReturn = SetCellContents(name, contentFormula);
                Lookup lookup = CellDoubleLookup;
                foreach(string s in GetCellsToRecalculate(name))
                {
                    sheet[s].SetValue(((Formula)sheet[s].GetContents()).Evaluate(lookup));
                }
            }
            else
            {
                sheet[name].SetValue(content);
                toReturn = SetCellContents(name, content);
            }

            Changed = true;
            return toReturn;
        }

        /// <summary>
        /// To be used as a Lookup, will return double associated with string in
        /// the sheet, or UndefinedVariablException otherwises
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private double CellDoubleLookup(string s)
        {
            if (!(GetCellValue(s) is double)) {
                throw new UndefinedVariableException("s");
            }
            return (double)GetCellValue(s);
        }

        /// <summary>
        /// Cell struct has a name and contents
        /// </summary>
        private struct Cell
        {
            /// <summary>
            /// Name of the cell
            /// </summary>
            private String name;
            /// <summary>
            /// Contents of the cell may be Formula, double, or string
            /// Thus, Object type is used
            /// </summary>
            private Object contents;
            /// <summary>
            /// Value of cell may be double, string, or FormulaError
            /// </summary>
            private Object value;

            /// <summary>
            /// Gets name
            /// </summary>
            /// <returns></returns>
            public string GetName()
            {
                return name;
            }

            /// <summary>
            /// Sets name
            /// </summary>
            /// <param name="n"></param>
            internal void SetName(string n)
            {
                name = n;
            }

            /// <summary>
            /// Gets contents
            /// </summary>
            /// <returns></returns>
            internal object GetContents()
            {
                return contents;
            }

            /// <summary>
            /// Sets contents
            /// </summary>
            /// <param name="theContents"></param>
            internal void SetContents(Object theContents)
            {
                contents = theContents;
            }

            /// <summary>
            /// Set value
            /// </summary>
            /// <param name="theValue"></param>
            internal void SetValue(Object theValue)
            {
                value = theValue;
            }

            /// <summary>
            /// Gets value
            /// </summary>
            /// <returns></returns>
            internal object GetValue()
            {
                return value;
            }
        }
    }
}
