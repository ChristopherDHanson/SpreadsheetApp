// Written by Christopher Hanson for CS 3500, January 2018.

using Dependencies;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DependencyGraphTestCases
{
    [TestClass]
    public class UnitTest1
    {
        // BASIC TESTS
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
            Assert.AreEqual(3, size);
        }

        /// <summary>
        /// Size for empty graph
        /// </summary>
        [TestMethod]
        public void EmptyGraphSize()
        {
            DependencyGraph testGraph = new DependencyGraph();
            int size = testGraph.Size;
            Assert.AreEqual(0, size);
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
        /// Test the GetDependents method on str with no dependents
        /// </summary>
        [TestMethod]
        public void GetDependentsOnStringWithNoDees()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("b", "c");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("d", "c");

            String[] shouldBeEmpty = (String[])testGraph.GetDependents("x");
            if (!(shouldBeEmpty.Length == 0))
            {
                Assert.Fail();
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
            IEnumerable<string> testDependees = testGraph.GetDependees("c");

            String[] expectedContents = { "b", "d", "a" };
            foreach (string s in testDependees)
            {
                if (!Array.Exists<string>(expectedContents, sa => sa == s))
                {
                    Assert.Fail();
                }
            }
        }

        /// <summary>
        /// Test the GetDependees method on str with no dependees
        /// </summary>
        [TestMethod]
        public void GetDependeesOnStringWithNoDees()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("b", "c");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("d", "c");

            String[] shouldBeEmpty = (String[])testGraph.GetDependees("x");
            if (!(shouldBeEmpty.Length == 0))
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Test very basic remove for fatal errors
        /// </summary>
        [TestMethod]
        public void TestRemoveForFatalErrors()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("b", "c");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("d", "c");

            testGraph.RemoveDependency("b", "c");
        }

        /// <summary>
        /// Remove on basic graph, then get size
        /// </summary>
        [TestMethod]
        public void RemoveThenGetSize()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("b", "c");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("d", "c");

            testGraph.RemoveDependency("b", "c");
            int size = testGraph.Size;
            Assert.AreEqual(3, size);
        }

        /// <summary>
        /// Remove twice on basic graph, then get size
        /// </summary>
        [TestMethod]
        public void RemoveTwiceThenGetSize()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("b", "c");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("d", "c");

            testGraph.RemoveDependency("b", "c");
            testGraph.RemoveDependency("a", "b");
            int size = testGraph.Size;
            Assert.AreEqual(2, size);
        }

        /// <summary>
        /// Removing a dependency causes a string to be without
        /// either dependees or dependents, so it is removed from
        /// the graph.
        /// </summary>
        [TestMethod]
        public void RemoveCausesCompleteRemoval()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("b", "c");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("d", "c");

            testGraph.RemoveDependency("a", "c");
            testGraph.RemoveDependency("a", "b");
            int size = testGraph.Size;
            Assert.AreEqual(2, size);
        }

        /// <summary>
        /// Test basic ReplaceDependents for fatal errors
        /// </summary>
        [TestMethod]
        public void TestReplaceDependentsForFatalError()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("b", "c");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("d", "c");

            String[] replacements = { "x", "y" };
            testGraph.ReplaceDependents("a", replacements);
        }

        /// <summary>
        /// Test basic ReplaceDependents and check dependents of s
        /// </summary>
        [TestMethod]
        public void ReplaceDependentsThenCheckDents()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("b", "c");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("d", "c");

            String[] replacements = { "x", "y" };
            testGraph.ReplaceDependents("c", replacements);
            IEnumerable<string> testDependents = testGraph.GetDependents("c");
            foreach (string s in testDependents)
            {
                Console.WriteLine(s);
            }
        }

        /// <summary>
        /// Test basic ReplaceDependees for fatal errors
        /// </summary>
        [TestMethod]
        public void ReplaceDependeesFatalErrors()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("b", "c");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("d", "c");

            String[] replacements = { "x", "y" };
            testGraph.ReplaceDependees("a", replacements);
        }

        /// <summary>
        /// Test basic ReplaceDependees and check dependees of t
        /// </summary>
        [TestMethod]
        public void ReplaceDependeesThenCheckDees()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            testGraph.AddDependency("b", "c");
            testGraph.AddDependency("a", "c");
            testGraph.AddDependency("d", "c");

            String[] replacements = { "x", "y" };
            testGraph.ReplaceDependees("c", replacements);
            IEnumerable<string> testDependees = testGraph.GetDependees("c");
            foreach (string s in testDependees)
            {
                Console.WriteLine(s);
            }
        }

        // NULL CHECK TESTS
        /// <summary>
        /// GetDependees with null argument passed in
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void GetDependeesNullParameterCheck()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.GetDependees(null);
        }

        /// <summary>
        /// GetDependents with null argument passed in
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void GetDependentsNullParameterCheck()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.GetDependents(null);
        }

        /// <summary>
        /// Add a dependency with a null second parameter
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void AddDependencyWithNullParam()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", null);
        }

        /// <summary>
        /// Add a dependency with a null first parameter
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void AddDependencyWithNullFirstParam()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency(null, null);
        }

        /// <summary>
        /// Remove a dependency with a null second parameter
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void RemoveDependencyWithNullParam()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.RemoveDependency("a", null);
        }

        /// <summary>
        /// Add a dependency with a null first parameter
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void RemoveDependencyWithNullFirstParam()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.RemoveDependency(null, null);
        }

        /// <summary>
        /// Call ReplaceDependents with a null second parameter
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void ReplaceDependentsWithNullParam()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.ReplaceDependents("a", null);
        }

        /// <summary>
        /// Call ReplaceDependents with a null first parameter
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void ReplaceDependentsWithNullFirstParam()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.ReplaceDependents(null, null);
        }

        /// <summary>
        /// Call ReplaceDependees with a null second parameter
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void ReplaceDependeesWithNullParam()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.ReplaceDependees("a", null);
        }

        /// <summary>
        /// Call ReplaceDependees with a null first parameter
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void ReplaceDependeesWithNullFirstParam()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.ReplaceDependees(null, null);
        }

        // STRESS TESTS
        /// <summary>
        /// AddOneHundredDependencies, see if fatal error occurs
        /// </summary>
        [TestMethod]
        public void AddOneHundredDependencies()
        {
            DependencyGraph testGraph = new DependencyGraph();

            for (int i = 0; i < 100; i++)
            {
                testGraph.AddDependency(i.ToString(), (i * 3).ToString());
            }
        }

        /// <summary>
        /// AddOneThousandDependencies, see if fatal error occurs
        /// </summary>
        [TestMethod]
        public void AddOneThousandDependencies()
        {
            DependencyGraph testGraph = new DependencyGraph();

            for (int i = 0; i < 1000; i++)
            {
                testGraph.AddDependency(i.ToString(), (i * 3).ToString());
            }
        }
    }
}
