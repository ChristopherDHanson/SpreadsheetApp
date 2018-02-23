// Written by Christopher Hanson for CS 3500, February 2018

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
            ss.SetContentsOfCell("a1", "0.98");
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
            ss.SetContentsOfCell("a1", "Cook's");
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
            ss.SetContentsOfCell("a1", "=A2-2");
            Object result = ss.GetCellContents("A1");
            if (!(result is Formula))
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Set cell contents to a Formula with invalid cell name vars, expect FFE
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void SSBasicSetFormulaWithInvalidVars()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("a1", "=X-2");
        }

        /// <summary>
        /// Set cell contents, then set same cell contents again (number); check for result
        /// </summary>
        [TestMethod]
        public void SSBasicSetAndOverrideDouble()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("a1", "0.1");
            ss.SetContentsOfCell("a1", "0.9");
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
            ss.SetContentsOfCell("a1", "Lake");
            ss.SetContentsOfCell("a1", "Leelanau");
            Object result = ss.GetCellContents("a1");
            Assert.AreEqual("Leelanau", result);
        }

        /// <summary>
        /// Set cell contents to formula, then check to see if it contains valid formula
        /// </summary>
        [TestMethod]
        public void SSBasicSetAndOverrideFormula()
        {
            Spreadsheet ss = new Spreadsheet();
            Formula f = new Formula("A1-4");
            Formula g = new Formula("A2-4");
            ss.SetContentsOfCell("a1", "=29-b1");
            ss.SetContentsOfCell("b1", "=5-4");
            Object result = ss.GetCellContents("a1");
            if (!(result is Formula))
            {
                Assert.Fail();
            }
            Formula resultFormula = (Formula)result;

            if (!resultFormula.GetVariables().Contains("B1"))
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Set cell contents to formula, then set same cell contents to double; check for result
        /// </summary>
        [TestMethod]
        public void SSBasicSetAndOverrideFormulaWithDouble()
        {
            Spreadsheet ss = new Spreadsheet();
            Formula f = new Formula("A1-4");
            ss.SetContentsOfCell("a1", "A1-4");
            ss.SetContentsOfCell("a1", "5.10");
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
            ss.SetContentsOfCell("a1", "A1-4");
            ss.SetContentsOfCell("a1", "Grand Haven");
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
            ss.SetContentsOfCell("a1", "apple");
            ss.SetContentsOfCell("a2", "3.14159");
            ss.SetContentsOfCell("a3", "<3");
            IEnumerable<string> result = ss.GetNamesOfAllNonemptyCells();
            Assert.AreEqual(3, result.Count());
        }

        /// <summary>
        /// Check return value of SetCellContents()
        /// </summary>
        [TestMethod]
        public void SSSetCellContentsReturnCheck()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("B1", "=A1*2");
            ss.SetContentsOfCell("C1", "=B1+A1");
            ISet<string> result = ss.SetContentsOfCell("A1", "0.6");
            String[] expected = { "A1", "B1", "C1" };
            foreach (string s in expected)
            {
                if (!result.Contains<string>(s))
                {
                    Assert.Fail();
                }
            }
            if (result.Count() != expected.Length)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Get names of all nonempty, with three cells nonempty; test correct names
        /// </summary>
        [TestMethod]
        public void SSGetAllNonEmptyThreeFilledNames()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("a1", "apple");
            ss.SetContentsOfCell("a2", "3.14159");
            ss.SetContentsOfCell("a3", "<3");
            IEnumerable<string> result = ss.GetNamesOfAllNonemptyCells();
            String[] expected = { "A1", "A2", "A3" };
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
            ss.SetContentsOfCell("a1", "A1-4");
            ss.SetContentsOfCell("a2", "A1-4");
            ss.SetContentsOfCell("A1", "0.87");
            Object result = ss.GetCellContents("A1");
            Assert.AreEqual(0.87, result);
        }

        /// <summary>
        /// Create circular dependency, expect CircularException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void SSCircularExceptionTest()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("B1", "=C1*2");
            ss.SetContentsOfCell("C1", "=A1*2");
            ss.SetContentsOfCell("A1", "=B1*2");
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
            ss.SetContentsOfCell(null, "0.999");
        }

        /// <summary>
        /// Set cell contents to string with null text param
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SSSetCellContentsToStringTestOnNullText()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("a1", null);
        }

        /// <summary>
        /// Set cell contents to string with null name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SSSetCellContentsToStringTestOnNullName()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell(null, "Unreachable");
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
            ss.SetContentsOfCell(null, "A99+B5");
        }
    }
}
