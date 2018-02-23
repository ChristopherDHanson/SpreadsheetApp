// Written by Christopher Hanson for CS 3500, February 2018

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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


        // ADDED FOR PS6
        /// <summary>
        /// Get Changed after changing
        /// </summary>
        [TestMethod]
        public void GetChangedAfterChanging()
        {
            Spreadsheet ss = new Spreadsheet();
            if (ss.Changed != false)
                Assert.Fail();
            ss.SetContentsOfCell("a1", "A99+B5");
            if (ss.Changed != true)
                Assert.Fail();
        }

        /// <summary>
        /// Create spreadsheet using constructor with isValid param
        /// </summary>
        [TestMethod]
        public void SecondConstructorTest()
        {
            Regex r = new Regex(".*");
            Spreadsheet ss = new Spreadsheet(r);
            if (ss.Changed != false)
                Assert.Fail();
        }

        /// <summary>
        /// Create spreadsheet using third constructor
        /// </summary>
        [TestMethod]
        public void ThirdConstructorTest()
        {
            Regex r = new Regex(".*");
            TextReader reader = new StringReader("<spreadsheet IsValid = \".*\" ><cell name =" +
                " \"A1\" contents = \"0.67\"></cell></spreadsheet>");
            Spreadsheet ss = new Spreadsheet(reader, r);
            if (ss.Changed != false)
                Assert.Fail();
        }

        /// <summary>
        /// Create spreadsheet using third constructor, check its values
        /// </summary>
        [TestMethod]
        public void ThirdConstructorTestExtraChecks()
        {
            Regex r = new Regex(".*");
            TextReader reader = new StringReader("<spreadsheet IsValid = \".*\" ><cell name =" +
                " \"A1\" contents = \"0.67\"></cell></spreadsheet>");
            Spreadsheet ss = new Spreadsheet(reader, r);
            if (ss.Changed != false)
                Assert.Fail();
            if (!(ss.GetCellValue("A1") is double) || (double)ss.GetCellValue("A1") != 0.67)
                Assert.Fail();
        }

        /// <summary>
        /// Create spreadsheet using third constructor, with invalid IsValid
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadException))]
        public void ThirdConstructorTestInvalidIsValid()
        {
            Regex r = new Regex(".*");
            TextReader reader = new StringReader("<spreadsheet IsValid = \"poo[\" ><cell name =" +
                " \"A1\" contents = \"0.67\"></cell></spreadsheet>");
            Spreadsheet ss = new Spreadsheet(reader, r);
        }

        /// <summary>
        /// Create spreadsheet using third constructor, with duplicate cells
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadException))]
        public void ThirdConstructorTestHasDuplicateCells()
        {
            Regex r = new Regex(".*");
            TextReader reader = new StringReader("<spreadsheet IsValid = \".*\" ><cell name =" +
                " \"A1\" contents = \"0.67\"></cell><cell name =" +
                " \"A1\" contents = \"0.76\"></cell></spreadsheet>");
            Spreadsheet ss = new Spreadsheet(reader, r);
        }

        /// <summary>
        /// Create spreadsheet using third constructor, with invalid according to old
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadException))]
        public void ThirdConstructorTestInvalidCellNameAccToOld()
        {
            Regex r = new Regex(".*");
            TextReader reader = new StringReader("<spreadsheet IsValid = \"[a-z]\" ><cell name =" +
                " \"A1\" contents = \"0.67\"><cell name =" +
                " \"A1\" contents = \"0.76\"></cell></spreadsheet>");
            Spreadsheet ss = new Spreadsheet(reader, r);
        }

        /// <summary>
        /// Create spreadsheet using third constructor, with invalid according to new
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetVersionException))]
        public void ThirdConstructorTestInvalidCellNameAccToNew()
        {
            Regex r = new Regex("[a]");
            TextReader reader = new StringReader("<spreadsheet IsValid = \".*\" ><cell name =" +
                " \"A1\" contents = \"0.67\"></cell></spreadsheet>");
            Spreadsheet ss = new Spreadsheet(reader, r);
        }

        /// <summary>
        /// Create spreadsheet using third constructor, with CircularException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadException))]
        public void ThirdConstructorTestCircularException()
        {
            Regex r = new Regex(".*");
            TextReader reader = new StringReader("<spreadsheet IsValid = \".*\" ><cell name =" +
                " \"A1\" contents = \"=B1*2\"></cell><cell name =" +
                " \"B1\" contents = \"=C1*2\"></cell><cell name =" +
                " \"C1\" contents = \"=A1*2\"></cell></spreadsheet>");
            Spreadsheet ss = new Spreadsheet(reader, r);
        }

        /// <summary>
        /// Create spreadsheet using third constructor, with ValidationError
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadException))]
        public void ThirdConstructorTestValidationError()
        {
            Regex r = new Regex(".*");
            TextReader reader = new StringReader("<spreadsheetx IsValid = \".*\" ><cell name =" +
                " \"A1\" contents = \"=B1*2\"></cell><cell name =" +
                " \"B1\" contents = \"=C1*2\"></cell><cell name =" +
                " \"C1\" contents = \"=A1*2\"></cell></spreadsheet>");
            Spreadsheet ss = new Spreadsheet(reader, r);
        }

        /// <summary>
        /// Create spreadsheet, have double replace formula as contents
        /// </summary>
        [TestMethod]
        public void DoubleReplacesFormulaContents()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("B1", "0.99");
            ss.SetContentsOfCell("A1", "=B2-4");
            ss.SetContentsOfCell("A1", "0.01");
            if (!(ss.GetCellValue("A1") is double)|| (double)ss.GetCellValue("A1") != 0.01)
                Assert.Fail();
        }

        /// <summary>
        /// Create spreadsheet, have string replace formula as contents
        /// </summary>
        [TestMethod]
        public void StringReplacesFormulaContents()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("B1", "0.99");
            ss.SetContentsOfCell("A1", "=B2-4");
            ss.SetContentsOfCell("A1", "Sleeping Bear");
            if (!(ss.GetCellValue("A1") is string) || (string)ss.GetCellValue("A1") != "Sleeping Bear")
                Assert.Fail();
        }

        /// <summary>
        /// Create spreadsheet, have string replace formula as contents
        /// </summary>
        [TestMethod]
        public void FormulaReplacesFormulaContents()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("B1", "0.99");
            ss.SetContentsOfCell("A1", "=B2-4");
            ss.SetContentsOfCell("A1", "=B1-99");
            if (!(ss.GetCellValue("A1") is double))
                Assert.Fail();
        }

        /// <summary>
        /// Create spreadsheet, have double set to invalid cell name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void DoubleSetInvalidCellName()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("B01", "0.99");
            ss.SetContentsOfCell("A1", "=B2-4");
            ss.SetContentsOfCell("A1", "Sleeping Bear");
        }

        /// <summary>
        /// Create spreadsheet, have string set to invalid cell name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void StringSetInvalidCellName()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("B1", "0.99");
            ss.SetContentsOfCell("A1", "=B2-4");
            ss.SetContentsOfCell("A01", "Monroe Center");
        }

        /// <summary>
        /// Create spreadsheet, have Formula set to invalid cell name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void FormulaSetInvalidCellName()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("B1", "0.99");
            ss.SetContentsOfCell("A01", "=B2-4");
            ss.SetContentsOfCell("A1", "The GRAM");
        }

        /// <summary>
        /// Create spreadsheet, have Formula variable points to invalid cell name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void FormulaVarIsInvalidCellName()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("B1", "0.99");
            ss.SetContentsOfCell("A21", "=B02-4");
            ss.SetContentsOfCell("A1", "The GRAM");
        }

        /// <summary>
        /// Get cell value of invalid name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void GetValueInvalidCellName()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.GetCellValue("A001");
        }

        /// <summary>
        /// Get cell value of null name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void GetValueNullCellName()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.GetCellValue(null);
        }

        /// <summary>
        /// Save basic spreadsheet, view output manually
        /// </summary>
        [TestMethod]
        public void SaveTestBasic()
        {
            StringBuilder result = new StringBuilder();
            StringWriter writer = new StringWriter(result);
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("B1", "0.99");
            ss.SetContentsOfCell("A1", "=B2-4");
            ss.SetContentsOfCell("A1", "0.01");
            ss.Save(writer);
            result.ToString();
            Console.Write(result);
            Assert.AreEqual('<', result[0]);
        }

        /// <summary>
        /// Save basic spreadsheet, construct from save
        /// </summary>
        [TestMethod]
        public void SaveTestBasicThenCreateFromSave()
        {
            StringBuilder result = new StringBuilder();
            StringWriter writer = new StringWriter(result);
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("B1", "0.99");
            ss.SetContentsOfCell("A1", "=B2-4");
            ss.SetContentsOfCell("A1", "0.01");
            ss.Save(writer);
            String resultStr = result.ToString();

            TextReader reader = new StringReader(resultStr);
            Regex r = new Regex(".*");
            Spreadsheet newSS = new Spreadsheet(reader, r);
            Assert.AreEqual(newSS.GetCellContents("A1"), 0.01);
        }

        /// <summary>
        /// Save spreadsheet with all three types of contents
        /// </summary>
        [TestMethod]
        public void SaveTestAllTypesOfContents()
        {
            StringBuilder result = new StringBuilder();
            StringWriter writer = new StringWriter(result);
            Spreadsheet ss = new Spreadsheet();
            ss.SetContentsOfCell("B1", "0.99");
            ss.SetContentsOfCell("A1", "=B2-4");
            ss.SetContentsOfCell("C1", "Breckenridge");
            ss.Save(writer);
            result.ToString();
            Console.Write(result);
            Assert.AreEqual('<', result[0]);
        }
    }
}
