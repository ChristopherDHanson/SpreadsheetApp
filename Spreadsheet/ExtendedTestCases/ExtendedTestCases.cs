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
        /// Create a DG with one dep, copy it using copy constructor, then
        /// remove the dependency from the copy. See if the dependency in
        /// the original chart still has dependees. This would only be true
        /// if the Dependency and DG objects are independent of each other.
        /// </summary>
        [TestMethod]
        public void TestWhetherDependenciesAreIndependent()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            DependencyGraph copyGraph = new DependencyGraph(testGraph);
            copyGraph.RemoveDependency("a", "b");
            bool result = testGraph.HasDependees("b");
            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Check independence of dependencies in original DG and copy DG
        /// </summary>
        [TestMethod]
        public void TestWhetherDependenciesAreIndependentReverse()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("a", "b");
            DependencyGraph copyGraph = new DependencyGraph(testGraph);
            copyGraph.RemoveDependency("a", "b");
            bool result = copyGraph.HasDependees("b");
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Place a null value in the IEnumerable that is to replace dependents
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullItemInIEnumToReplaceDependents()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("Home", "is");
            testGraph.AddDependency("Grand", "Rapids");
            String[] toReplace = { "in", "bound", null };
            testGraph.ReplaceDependents("Home", toReplace);
        }

        /// <summary>
        /// Place a null value in the IEnumerable that is to replace dependees
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullItemInIEnumToReplaceDependees()
        {
            DependencyGraph testGraph = new DependencyGraph();
            testGraph.AddDependency("Home", "is");
            testGraph.AddDependency("Grand", "Rapids");
            String[] toReplace = { "Family", "Far away", null };
            testGraph.ReplaceDependees("is", toReplace);
        }
    }
}
