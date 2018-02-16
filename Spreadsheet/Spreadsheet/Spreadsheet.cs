// Written by Christopher Hanson for CS 3500, February 2018

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dependencies;
using Formulas;

namespace SS
{
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
        /// Constructor; makes new empty spreadsheet
        /// </summary>
        public Spreadsheet()
        {
            sheet = new Dictionary<string, Cell>();
            depGraph = new DependencyGraph();
        }

        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            List<string> names = new List<string>();
            foreach (var s in sheet)
            {
                if (s.Value.GetContents() != null)
                {
                    names.Add(s.Value.GetName());
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
            String validCellNamePattern = @"^[a-zA-Z]*[1-9]\d*$";
            if (name == null || !Regex.IsMatch(name, validCellNamePattern))
            {
                throw new InvalidNameException();
            }
            if (sheet.TryGetValue(name, out Cell theCell))
            {
                return theCell.GetContents();
            }
            return "";
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
        public override ISet<string> SetCellContents(string name, double number)
        {
            String validCellNamePattern = @"^[a-zA-Z]*[1-9]\d*$";
            if (name == null || !Regex.IsMatch(name, validCellNamePattern))
            {
                throw new InvalidNameException();
            }

            Cell test = new Cell();
            if (sheet.TryGetValue(name, out test))
            {
                sheet.Remove(name);
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
                
            test.SetContents(number);
            test.SetName(name);
            sheet.Add(name, test);

            HashSet<string> toReturn = new HashSet<string>();
            foreach (string s in GetCellsToRecalculate(name))
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
        public override ISet<string> SetCellContents(string name, string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("");
            }
            String validCellNamePattern = @"^[a-zA-Z]*[1-9]\d*$";
            if (name == null || !Regex.IsMatch(name, validCellNamePattern))
            {
                throw new InvalidNameException();
            }

            Cell test = new Cell();
            if (sheet.TryGetValue(name, out test))
            {
                sheet.Remove(name);
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
            sheet.Add(name, test);

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
        public override ISet<string> SetCellContents(string name, Formula formula)
        {
            String validCellNamePattern = @"^[a-zA-Z]*[1-9]\d*$";
            if (name == null || !Regex.IsMatch(name, validCellNamePattern))
            {
                throw new InvalidNameException();
            }

            HashSet<string> vars = (HashSet<string>)formula.GetVariables();
            foreach (string s in vars) // for each variable in the formula
            {
                if (!Regex.IsMatch(s, validCellNamePattern)) // if not valid cell name,
                {
                    throw new InvalidNameException(); // throw exception
                }
            }

            Cell test = new Cell();
            if (sheet.TryGetValue(name, out test))
            {
                sheet.Remove(name);
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
            test.SetContents(formula);

            test.SetName(name);
            sheet.Add(name, test);

            foreach (string s in vars) { // Add new relevant dependencies
                depGraph.AddDependency(s, name);
            }

            HashSet<string> toReturn = new HashSet<string>();
            foreach (string s in GetCellsToRecalculate(name))
            {
                toReturn.Add(s);
            }

            return toReturn;
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
            if (name == null)
            {
                throw new ArgumentNullException("Argument must not be null.");
            }
            return depGraph.GetDependents(name);
        }

        private struct Cell
        {
            String name;
            Object contents;

            public string GetName()
            {
                return name;
            }

            internal void SetName(string n)
            {
                name = n;
            }

            internal object GetContents()
            {
                return contents;
            }

            internal void SetContents(Object theContents)
            {
                contents = theContents;
            }
        }
    }
}
