using System;
using System.Collections.Generic;
using Formulas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FormulaExtensionTests
{
    [TestClass]
    public class ExtensionTests
    {
        /// <summary>
        /// Confirm basic use of overridden ToString() method
        /// </summary>
        [TestMethod]
        public void TestOverriddenToString()
        {
            Formula f = new Formula("x+4");
            Formula g = new Formula(f.ToString());

            double fAnswer = f.Evaluate(x => 5);
            double gAnswer = g.Evaluate(x => 5);

            Assert.AreEqual(fAnswer, gAnswer);
        }

        /// <summary>
        /// Div by zero
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaEvaluationException))]
        public void TestBasicDivideByZero()
        {
            Formula f = new Formula("49519 / var");
            f.Evaluate(v => 0);
        }

        /// <summary>
        /// Many parentheses
        /// </summary>
        [TestMethod]
        public void TestManyParenthesesWithAdditionInside()
        {
            Formula f = new Formula("(5+5+5+5+5+5+(5+5+5+(5+5)))");
            double result = f.Evaluate(v => 0);
            Assert.AreEqual(55, result);
        }

        /// <summary>
        /// Division with many parentheses
        /// </summary>
        [TestMethod]
        public void TestDivWithPars()
        {
            Formula f = new Formula("(98/(65/(5/5)))");
            double result = f.Evaluate(v => 0);
            if (Math.Abs(1.50769230769231 - result) > 0.000001)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Test basic GetVariables() call
        /// </summary>
        [TestMethod]
        public void TestBasicGetVars()
        {
            Formula f = new Formula("xerath + ziggs + yorick");
            HashSet<string> results = (HashSet<string>)f.GetVariables();
            String[] target = { "xerath", "ziggs", "yorick" };
            for (int i = 0; i < 3; i++)
            {
                if (!results.Contains(target[i]))
                {
                    Assert.Fail();
                }
            }
        }

        /// <summary>
        /// Normalizer that makes vars invalid according to PS2 rules
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestNormalizerThatInvalidatesVars()
        {
            Formula f = new Formula("x + 89", n => "++++++++++", v => true);
        }

        /// <summary>
        /// Invalid var due to validator
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestValidator()
        {
            Formula f = new Formula("x + 89", n => n.ToUpper(), v => { if (v.Equals("never"))
                    return true; else return false; });
        }

        // NULL CHECKS
        /// <summary>
        /// Use null string param for formula contructor
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullFormula()
        {
            Formula f = new Formula(null);
        }

        /// <summary>
        /// Use null Normalizer param for formula contructor
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullNormalizer()
        {
            Formula f = new Formula("(''')<(--_--)>(''')", null, v => true);
        }

        /// <summary>
        /// Use null Validator param for formula contructor
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullValidator()
        {
            Formula f = new Formula("(''')<(--_--)>(''')", n => n, null);
        }

        /// <summary>
        /// Use zero param formula contstructor, then evaluate. Formula will be null
        /// </summary>
        [TestMethod]
        public void TestNoParamFormulaConstructor()
        {
            Formula f = new Formula();
            double result = f.Evaluate(v => 616);
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// Use zero param formula contstructor, then evaluate. Formula will be null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullLookupOnEvaluate()
        {
            Formula f = new Formula();
            double result = f.Evaluate(null);
        }
    }
}
