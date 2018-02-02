using Dependencies;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DependencyGraphTestCases
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Basic constructor test; add one dependency
        /// </summary>
        [TestMethod]
        public void TestConstructorBasic()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
        }

        /// <summary>
        /// Add a few dependencies
        /// </summary>
        [TestMethod]
        public void AddAFewDependencies()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("b", "c");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("d", "c");
        }

        /// <summary>
        /// Add two duplicate dependencies
        /// </summary>
        [TestMethod]
        public void AddDuplicateDependencies()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("d", "c");
        }

        /// <summary>
        /// Get size for small depGraph
        /// </summary>
        [TestMethod]
        public void SizeForSmallgraph()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("d", "c");
            int size = testGraph.Size;
            Assert.AreEqual(4, size);
        }

        /// <summary>
        /// Test the HasDependees method, expecting a false result
        /// </summary>
        [TestMethod]
        public void HasDependeesExpectFalse()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("b", "c");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("d", "c");
            Assert.AreEqual(false, testGraph.HasDependees("a"));
        }

        /// <summary>
        /// Test the HasDependees method, expecting a true result
        /// </summary>
        [TestMethod]
        public void HasDependeesExpectTrue()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("b", "c");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("d", "c");
            Assert.AreEqual(true, testGraph.HasDependees("c"));
        }

        /// <summary>
        /// Test the HasDependents method, expecting a false result
        /// </summary>
        [TestMethod]
        public void HasDependentsExpectFalse()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("b", "c");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("d", "c");
            Assert.AreEqual(false, testGraph.HasDependents("c"));
        }

        /// <summary>
        /// Test the HasDependents method, expecting a true result
        /// </summary>
        [TestMethod]
        public void HasDependentsExpectTrue()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("b", "c");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("d", "c");
            Assert.AreEqual(true, testGraph.HasDependents("a"));
        }

        /// <summary>
        /// Test the GetDependents method with a small depGraph
        /// </summary>
        [TestMethod]
        public void GetDependentsSmallGraph()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("b", "c");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("d", "c");
            IEnumerable<string> testDependents = testGraph.GetDependents("a");

            String[] expectedContents = { "b", "c" };
            foreach (string s in testDependents)
            {
                if (!Array.Exists<string>(expectedContents, sa => sa == s)) {
                    Assert.Fail();
                }
            }
        }

        /// <summary>
        /// Test the GetDependents method with a small depGraph
        /// </summary>
        [TestMethod]
        public void GetDependeesSmallGraph()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("b", "c");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("d", "c");
            IEnumerable<string> testDependents = testGraph.GetDependees("c");

            String[] expectedContents = { "b", "a", "d"};
            foreach (string s in testDependents)
            {
                if (!Array.Exists<string>(expectedContents, sa => sa == s))
                {
                    Assert.Fail();
                }
            }
        }

        /// <summary>
        /// Test the GetDependees method with a small depGraph
        /// </summary>
        [TestMethod]
        public void GetDependees()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("b", "c");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("d", "c");
            IEnumerable<string> testDependents = testGraph.GetDependees("c");

            String[] expectedContents = { "b", "d", "a" };
            foreach (string s in testDependents)
            {
                if (!Array.Exists<string>(expectedContents, sa => sa == s))
                {
                    Assert.Fail();
                }
            }
        }
    }
}
