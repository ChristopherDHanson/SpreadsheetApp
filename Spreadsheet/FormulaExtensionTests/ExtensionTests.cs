using System;
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
    }
}
