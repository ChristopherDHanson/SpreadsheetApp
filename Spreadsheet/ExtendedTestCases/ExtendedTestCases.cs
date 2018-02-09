using System;
using Dependencies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtendedTestCases
{
    [TestClass]
    public class ExtendedTestCases
    {
        /// <summary>
        /// Basic constructor test; add one dependency
        /// </summary>
        [TestMethod]
        public void TestCopyConstructor()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");

            DependencyGraph copyGraph = new DependencyGraph(testGraph);
            Assert.AreEqual(1, copyGraph.Size);
        }

        /// <summary>
        /// Basic constructor test; add one dependency
        /// </summary>
        [TestMethod]
        public void TestasdgasdCopyConstructor()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");

            DependencyGraph copyGraph = new DependencyGraph(testGraph);
            Assert.AreEqual(1, copyGraph.Size);
        }
    }
}
