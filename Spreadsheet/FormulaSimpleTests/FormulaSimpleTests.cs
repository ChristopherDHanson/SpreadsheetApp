// Written by Joe Zachary for CS 3500, January 2017.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Formulas;

namespace FormulaTestCases
{
    /// <summary>
    /// These test cases are in no sense comprehensive!  They are intended to show you how
    /// client code can make use of the Formula class, and to show you how to create your
    /// own (which we strongly recommend).  To run them, pull down the Test menu and do
    /// Run > All Tests.
    /// </summary>
    [TestClass]
    public class UnitTests
    {
        /// <summary>
        /// This tests that a syntactically incorrect parameter to Formula results
        /// in a FormulaFormatException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct1()
        {
            Formula f = new Formula("_");
        }

        /// <summary>
        /// This is another syntax error
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct2()
        {
            Formula f = new Formula("2++3");
        }

        /// <summary>
        /// Another syntax error.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct3()
        {
            Formula f = new Formula("2 3");
        }

        /// <summary>
        /// Makes sure that "2+3" evaluates to 5.  Since the Formula
        /// contains no variables, the delegate passed in as the
        /// parameter doesn't matter.  We are passing in one that
        /// maps all variables to zero.
        /// </summary>
        [TestMethod]
        public void Evaluate1()
        {
            Formula f = new Formula("2+3");
            Assert.AreEqual(f.Evaluate(v => 0), 5.0, 1e-6);
        }

        /// <summary>
        /// The Formula consists of a single variable (x5).  The value of
        /// the Formula depends on the value of x5, which is determined by
        /// the delegate passed to Evaluate.  Since this delegate maps all
        /// variables to 22.5, the return value should be 22.5.
        /// </summary>
        [TestMethod]
        public void Evaluate2()
        {
            Formula f = new Formula("x5");
            Assert.AreEqual(f.Evaluate(v => 22.5), 22.5, 1e-6);
        }

        /// <summary>
        /// Here, the delegate passed to Evaluate always throws a
        /// UndefinedVariableException (meaning that no variables have
        /// values).  The test case checks that the result of
        /// evaluating the Formula is a FormulaEvaluationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaEvaluationException))]
        public void Evaluate3()
        {
            Formula f = new Formula("x + y");
            f.Evaluate(v => { throw new UndefinedVariableException(v); });
        }

        /// <summary>
        /// The delegate passed to Evaluate is defined below.  We check
        /// that evaluating the formula returns in 10.
        /// </summary>
        [TestMethod]
        public void Evaluate4()
        {
            Formula f = new Formula("x + y");
            Assert.AreEqual(f.Evaluate(Lookup4), 10.0, 1e-6);
        }

        /// <summary>
        /// This uses one of each kind of token.
        /// </summary>
        [TestMethod]
        public void Evaluate5 ()
        {
            Formula f = new Formula("(x + y) * (z / x) * 1.0");
            Assert.AreEqual(f.Evaluate(Lookup4), 20.0, 1e-6);
        }

        /// <summary>
        /// A Lookup method that maps x to 4.0, y to 6.0, and z to 8.0.
        /// All other variables result in an UndefinedVariableException.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public double Lookup4(String v)
        {
            switch (v)
            {
                case "x": return 4.0;
                case "y": return 6.0;
                case "z": return 8.0;
                default: throw new UndefinedVariableException(v);
            }
        }


        //MY TESTS
        /// <summary>
        /// Empty formula passed into constructor
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestNoTokens()
        {
            Formula f = new Formula("");
        }

        /// <summary>
        /// Begin formula string with invalid first token
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidFirst()
        {
            Formula f = new Formula(")x-y)");
        }

        /// <summary>
        /// Invalid last token
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidLast()
        {
            Formula f = new Formula("(5*x)-y!");
        }

        /// <summary>
        /// Test constructor with formula consisting of '()'
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestJustParentheses()
        {
            Formula f = new Formula("()");
        }

        /// <summary>
        /// Formula with more opening parantheses than closing; invalid
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestMoreOpeningPars()
        {
            Formula f = new Formula("((x+y)");
        }

        /// <summary>
        /// Formula with more closing parentheses than opening; invalid
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestMoreClosingPars()
        {
            Formula f = new Formula("(x+y))");
        }

        /// <summary>
        /// Formula has more closing parentheses than opening at one point, but finishes
        /// with equal numbers of op and clos.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestMoreClosingAtOnePoint()
        {
            Formula f = new Formula("(x+y))-((5)");
        }

        /// <summary>
        /// Two consecutive tokens of type variable; invalid
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestVarToVar()
        {
            Formula f = new Formula("(x - y y)");
        }

        /// <summary>
        /// Uses example formula with double literals (containing 'e')
        /// </summary>
        [TestMethod]
        public void TestExampleFormula()
        {
            Formula f = new Formula("2.5e9 + x5 / 17");
        }

        /// <summary>
        /// Tests Evaluate() with a division by zero in the formula
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaEvaluationException))]
        public void TestEvalWithDivByZero()
        {
            Formula f = new Formula("(2 / 0) + 5");
            f.Evaluate(v => 7);
        }

        /// <summary>
        /// Tests subtraction
        /// </summary>
        [TestMethod]
        public void TestEvalWithSubtraction()
        {
            Formula f = new Formula("4949494-5574999");
            double result = f.Evaluate(v => 7);
            Assert.AreEqual(4949494 - 5574999, result);
        }

        /// <summary>
        /// Division of doubles
        /// </summary>
        [TestMethod]
        public void TestDivisionOfDoubles()
        {
            Formula f = new Formula("(4949494/5574999)");
            double result = f.Evaluate(v => 7);
            Assert.AreEqual((4949494.0 / 5574999.0), result);
        }
        
        /// <summary>
        /// Somewhat complicated switching operators
        /// </summary>
        [TestMethod]
        public void TestSubFollowedByAddOperators()
        {
            Formula f = new Formula("((4-3+9)-5)");
            double result = f.Evaluate(v => 7);
            Assert.AreEqual((4-3+9)-5, result);
        }

        /// <summary>
        /// Many divisions repeatedly
        /// </summary>
        [TestMethod]
        public void TestDoubleDivisionTwice()
        {
            Formula f = new Formula("(494/94/94/5574999)");
            double result = f.Evaluate(v => 7);
            Assert.AreEqual((494.0 / 94.0 / 94.0 / 5574999.0), result);
        }

        /// <summary>
        /// Add thrice
        /// </summary>
        [TestMethod]
        public void TestDoubleAddThrice()
        {
            Formula f = new Formula("(494+94+94+5574999)");
            double result = f.Evaluate(v => 7);
            Assert.AreEqual((494.0 + 94.0 + 94.0 + 5574999.0), result);
        }
    }
}
