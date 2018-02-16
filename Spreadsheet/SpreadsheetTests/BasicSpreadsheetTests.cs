using System;
using System.Collections.Generic;
using System.Linq;
using Formulas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SS;

namespace SpreadsheetTests
{
    [TestClass]
    public class BasicSpreadsheetTests
    {
        /// <summary>
        /// Construct new ss, check for fatal errors
        /// </summary>
        [TestMethod]
        public void SSBasicConstructorTest()
        {
            Spreadsheet ss = new Spreadsheet();
        }

        /// <summary>
        /// Get cell contents on empty cell
        /// </summary>
        [TestMethod]
        public void SSBasicGetOnEmpty()
        {
            Spreadsheet ss = new Spreadsheet();
            Object result = ss.GetCellContents("A6");
            Assert.AreEqual("", result);
        }

        /// <summary>
        /// Set cell contents to a double, get contents
        /// </summary>
        [TestMethod]
        public void SSBasicSetAndGetNumber()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents("a1", 0.98);
            Object result = ss.GetCellContents("a1");
            Assert.AreEqual(0.98, result);
        }

        /// <summary>
        /// Set cell contents to a string, get contents
        /// </summary>
        [TestMethod]
        public void SSBasicSetAndGetString()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents("a1", "Cook's");
            Object result = ss.GetCellContents("a1");
            Assert.AreEqual("Cook's", result);
        }

        /// <summary>
        /// Set cell contents to a Formula, get contents
        /// </summary>
        [TestMethod]
        public void SSBasicSetAndGetFormula()
        {
            Spreadsheet ss = new Spreadsheet();
            Formula f = new Formula("A2-2");
            ss.SetCellContents("a1", f);
            Object result = ss.GetCellContents("a1");
            Assert.AreEqual(f, result);
        }

        /// <summary>
        /// Set cell contents to a Formula with invalid cell name vars, get contents
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SSBasicSetFormulaWithInvalidVars()
        {
            Spreadsheet ss = new Spreadsheet();
            Formula f = new Formula("X-2");
            ss.SetCellContents("a1", f);
        }

        /// <summary>
        /// Set cell contents, then set same cell contents again (number); check for result
        /// </summary>
        [TestMethod]
        public void SSBasicSetAndOverrideDouble()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents("a1", 0.1);
            ss.SetCellContents("a1", 0.9);
            Object result = ss.GetCellContents("a1");
            Assert.AreEqual(0.9, result);
        }

        /// <summary>
        /// Set cell contents, then set same cell contents again (string); check for result
        /// </summary>
        [TestMethod]
        public void SSBasicSetAndOverrideString()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents("a1", "Lake");
            ss.SetCellContents("a1", "Leelanau");
            Object result = ss.GetCellContents("a1");
            Assert.AreEqual("Leelanau", result);
        }

        /// <summary>
        /// Set cell contents, then set same cell contents again (formula); check for result
        /// </summary>
        [TestMethod]
        public void SSBasicSetAndOverrideFormula()
        {
            Spreadsheet ss = new Spreadsheet();
            Formula f = new Formula("A1-4");
            Formula g = new Formula("A2-4");
            ss.SetCellContents("a1", f);
            ss.SetCellContents("a1", g);
            Object result = ss.GetCellContents("a1");
            Assert.AreEqual(g, result);
        }

        /// <summary>
        /// Set cell contents to formula, then set same cell contents to double; check for result
        /// </summary>
        [TestMethod]
        public void SSBasicSetAndOverrideFormulaWithDouble()
        {
            Spreadsheet ss = new Spreadsheet();
            Formula f = new Formula("A1-4");
            ss.SetCellContents("a1", f);
            ss.SetCellContents("a1", 5.10);
            Object result = ss.GetCellContents("a1");
            Assert.AreEqual(5.10, result);
        }

        /// <summary>
        /// Set cell contents to formula, then set same cell contents to string; check for result
        /// </summary>
        [TestMethod]
        public void SSBasicSetAndOverrideFormulaWithString()
        {
            Spreadsheet ss = new Spreadsheet();
            Formula f = new Formula("A1-4");
            ss.SetCellContents("a1", f);
            ss.SetCellContents("a1", "Grand Haven");
            Object result = ss.GetCellContents("a1");
            Assert.AreEqual("Grand Haven", result);
        }

        /// <summary>
        /// Get names of all nonempty, when all are empty
        /// </summary>
        [TestMethod]
        public void SSAllNonEmptyOnAllEmpty()
        {
            Spreadsheet ss = new Spreadsheet();
            IEnumerable<string> result = ss.GetNamesOfAllNonemptyCells();
            Assert.AreEqual(0, result.Count());
        }

        /// <summary>
        /// Get names of all nonempty, with three cells nonempty; test correct number
        /// </summary>
        [TestMethod]
        public void SSGetAllNonEmptyThreeFilledTestNum()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents("a1", "apple");
            ss.SetCellContents("a2", 3.14159);
            ss.SetCellContents("a3", "<3");
            IEnumerable<string> result = ss.GetNamesOfAllNonemptyCells();
            Assert.AreEqual(3, result.Count());
        }

        /// <summary>
        /// Get names of all nonempty, with three cells nonempty; test correct names
        /// </summary>
        [TestMethod]
        public void SSGetAllNonEmptyThreeFilledNames()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents("a1", "apple");
            ss.SetCellContents("a2", 3.14159);
            ss.SetCellContents("a3", "<3");
            IEnumerable<string> result = ss.GetNamesOfAllNonemptyCells();
            String[] expected = { "a1", "a2", "a3" };
            foreach (string s in result)
            {
                if (!expected.Contains<string>(s))
                    Assert.Fail();
            }
        }

        /// <summary>
        /// Get direct dependents of a cell with two
        /// </summary>
        [TestMethod]
        public void SSChangeCellThatIsDependee()
        {
            Spreadsheet ss = new Spreadsheet();
            Formula f = new Formula("A1-4");
            Formula g = new Formula("A1-4");
            ss.SetCellContents("a1", f);
            ss.SetCellContents("a2", g);
            ss.SetCellContents("A1", 0.87);
            Object result = ss.GetCellContents("A1");
            Assert.AreEqual(0.87, result);
        }

        /// <summary>
        /// Create circular dependency, expect  CircularException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void SSCircularExceptionTest()
        {
            Spreadsheet ss = new Spreadsheet();
            Formula f = new Formula("A1-4");
            Formula g = new Formula("a1-4");
            Formula h = new Formula("a2+3");
            ss.SetCellContents("a1", f);
            ss.SetCellContents("a2", g);
            ss.SetCellContents("A1", h);
        }

        // NULL CHECKS
        /// <summary>
        /// Get cell contents with null param
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SSGetCellContentsTestOnNullParam()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.GetCellContents(null);
        }

        /// <summary>
        /// Set cell contents (double) with null name param
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SSSetCellContentsTestOnNullParam()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents(null, 0.999);
        }

        /// <summary>
        /// Set cell contents to string with null text param
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SSSetCellContentsToStringTestOnNullText()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents("a1", null);
        }

        /// <summary>
        /// Set cell contents to string with null name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SSSetCellContentsToStringTestOnNullName()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents(null, "Unreachable");
        }

        /// <summary>
        /// Set cell contents to formula with null name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SSSetCellContentsToFormulaTestOnNullName()
        {
            Spreadsheet ss = new Spreadsheet();
            Formula f = new Formula("A99+B5");
            ss.SetCellContents(null, f);
        }
    }
}
