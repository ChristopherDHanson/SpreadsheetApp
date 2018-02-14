// Dependency class, method bodies written by Christopher Hanson for CS 3500, January 2018.
// Skeleton implementation written by Joe Zachary for CS 3500, January 2018.

using System;
using System.Collections;
using System.Collections.Generic;

namespace Dependencies
{
    /// <summary>
    /// A DependencyGraph can be modeled as a set of dependencies, where a dependency is an ordered 
    /// pair of strings.  Two dependencies (s1,t1) and (s2,t2) are considered equal if and only if 
    /// s1 equals s2 and t1 equals t2.
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that the dependency (s,t) is in DG 
    ///    is called the dependents of s, which we will denote as dependents(s).
    ///        
    ///    (2) If t is a string, the set of all strings s such that the dependency (s,t) is in DG 
    ///    is called the dependees of t, which we will denote as dependees(t).
    ///    
    /// The notations dependents(s) and dependees(s) are used in the specification of the methods of this class.
    ///
    /// For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    ///     dependents("a") = {"b", "c"}
    ///     dependents("b") = {"d"}
    ///     dependents("c") = {}
    ///     dependents("d") = {"d"}
    ///     dependees("a") = {}
    ///     dependees("b") = {"a"}
    ///     dependees("c") = {"a"}
    ///     dependees("d") = {"b", "d"}
    ///     
    /// All of the methods below require their string parameters to be non-null.  This means that 
    /// the behavior of the method is undefined when a string parameter is null.  
    ///
    /// IMPORTANT IMPLEMENTATION NOTE
    /// 
    /// The simplest way to describe a DependencyGraph and its methods is as a set of dependencies, 
    /// as discussed above.
    /// 
    /// However, physically representing a DependencyGraph as, say, a set of ordered pairs will not
    /// yield an acceptably efficient representation.  DO NOT USE SUCH A REPRESENTATION.
    /// 
    /// You'll need to be more clever than that.  Design a representation that is both easy to work
    /// with as well acceptably efficient according to the guidelines in the PS3 writeup. Some of
    /// the test cases with which you will be graded will create massive DependencyGraphs.  If you
    /// build an inefficient DependencyGraph this week, you will be regretting it for the next month.
    /// </summary>
    public class DependencyGraph
    {
        /// <summary>
        /// A Dictionary<> object to hold the graph. (note: Count != size)
        /// </summary>
        private Dictionary<String, Dependency> depGraph;
        /// <summary>
        /// Holds the size of the Dependency Graph
        /// </summary>
        private int depCount = 0; 

        /// <summary>
        /// Creates a DependencyGraph containing no dependencies.
        /// </summary>
        public DependencyGraph()
        {
            depGraph = new Dictionary<string, Dependency>();
        }

        /// <summary>
        /// Creates a copy of the DependencyGraph supplied in the constructor. This copy is entirely independent of the
        /// original.
        /// </summary>
        /// <param name="d"></param>
        public DependencyGraph(DependencyGraph d)
        {
            depGraph = new Dictionary<string, Dependency>();
            foreach (var item in d.depGraph)
            {
                Dependency halfDepToAdd = new Dependency(item.Key);
                foreach (var theDee in item.Value.GetDees())
                {
                    halfDepToAdd.AddDependee(theDee);
                }
                foreach (var theDent in item.Value.GetDents())
                {
                    halfDepToAdd.AddDependent(theDent);
                }
                depGraph.Add(item.Key, halfDepToAdd);
                depCount = d.depCount;
            }
        }

        /// <summary>
        /// The number of dependencies in the DependencyGraph.
        /// </summary>
        public int Size
        {
            get { return depCount; }
        }

        /// <summary>
        /// Reports whether dependents(s) is non-empty.  Requires s != null.
        /// </summary>
        public bool HasDependents(string s)
        {
            if (depGraph.TryGetValue(s, out Dependency halfDep)) // Try to get target value, 's'
            {
                if (halfDep.GetDents().Count > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Reports whether dependees(s) is non-empty.  Requires s != null. Throws ArgumentNullException 
        /// otherwise.
        /// </summary>
        public bool HasDependees(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("Parameter must not be null");
            }

            if (depGraph.TryGetValue(s, out Dependency halfDep)) // See if 's' is in the graph
            {
                if (halfDep.GetDees().Count > 0) // If so, see if 's' has any dependees
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Enumerates dependents(s).  Requires s != null. Throws ArgumentNullException otherwise.
        /// </summary>
        public IEnumerable<string> GetDependents(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("Parameter must not be null");
            }

            if (depGraph.TryGetValue(s, out Dependency halfDep)) // If 's' is in the graph,
            {
                return halfDep.GetDents(); // Return all of its dependents using a helper method
            }

            return new String[0]; // Otherwise, return a new empty IEnumerable
        }

        /// <summary>
        /// Enumerates dependees(s).  Requires s != null. Throws ArgumentNullException otherwise
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException("Parameter must not be null");
            }

            if (depGraph.TryGetValue(s, out Dependency halfDep)) // If 's' is in the graph,
            {
                return halfDep.GetDees(); // Return all of its dependees using a helper method
            }

            return new String[0]; // Else, return a new empty IEnumerable
        }

        /// <summary>
        /// Adds the dependency (s,t) to this DependencyGraph.
        /// This has no effect if (s,t) already belongs to this DependencyGraph.
        /// Requires s != null and t != null. Throws ArgumentNullException otherwise.
        /// </summary>
        public void AddDependency(string s, string t)
        {
            if (s == null || t == null)
            {
                throw new ArgumentNullException("Strings cannot be null");
            }

            Dependency sDep, tDep;
            if (depGraph.TryGetValue(s, out sDep)) // If 's' is in the graph, put it in sDep
            {
            }
            else // Else, make a new Dependency named 's' and add it to graph
            {
                sDep = new Dependency(s);
                depGraph.Add(s, sDep);
            }

            if (depGraph.TryGetValue(t, out tDep)) // If 't' is in graph, put it in tDep
            {
            }
            else // Else make new Dependency named 't', add it to graph
            {
                tDep = new Dependency(t);
                depGraph.Add(t, tDep);
            }

            if (sDep.AddDependent(t)) // If 't' is not already a dependent of 's', add it
            {
                tDep.AddDependee(s); // and add 's' as a dependee of 't'
                depCount++; // Increase size of graph
            }
        }

        /// <summary>
        /// Removes the dependency (s,t) from this DependencyGraph.
        /// Does nothing if (s,t) doesn't belong to this DependencyGraph.
        /// Requires s != null and t != null. Throws ArgumentNullException otherwise
        /// </summary>
        public void RemoveDependency(string s, string t)
        {
            if (s == null || t == null) {
                throw new ArgumentNullException("Parameters must not be null");
            }

            if (depGraph.TryGetValue(s, out Dependency halfDepS)) // If 's' is in the graph
            {
                int ind = halfDepS.IndexOfDependent(t); // Find index of 't' in 's'' List of dependents
                if (ind > -1) // If 't' is a dependent of 's'
                {
                    halfDepS.RemoveFromDependents(ind);  // Remove it from 's'' List of dependents

                    if (halfDepS.GetDents().Count == 0 && halfDepS.GetDees().Count == 0) // If 's' has no dees/dents
                    {
                        depGraph.Remove(s); // Remove it from the graph to save memory
                    }

                    depGraph.TryGetValue(t, out Dependency halfDepT); // Get value of 't', put it in Dependency
                    halfDepT.RemoveFromDependees(halfDepT.IndexOfDependee(s)); // Remove 's' from Dependees of 't'

                    if (halfDepT.GetDents().Count == 0 && halfDepT.GetDees().Count == 0) // If 't' has no dees/dents
                    {
                        depGraph.Remove(t); // Remove it from the graph to save memory
                    }

                    depCount--; // Decrease size of graph
                }
            }

        }

        /// <summary>
        /// Removes all existing dependencies of the form (s,r).  Then, for each
        /// t in newDependents, adds the dependency (s,t).
        /// Requires s != null and t != null. Throws ArgumentNullException if otherwise
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            if (s == null || newDependents == null)
            {
                throw new ArgumentNullException("Parameter must not be null");
            }

            if (depGraph.TryGetValue(s, out Dependency halfDep)) // Is 's' in the graph?
            {
                foreach (string x in halfDep.GetDents()) // If so, for each of its dependents,
                {
                    depGraph.TryGetValue(x, out Dependency dependentOfS); // Obtain the dependent
                    dependentOfS.RemoveFromDependees(dependentOfS.IndexOfDependee(s)); // Remove 's' from dees
                    depCount--; // Decrease size of graph
                }
                halfDep.ClearDependents(); // Clear 's' of all dependents

                foreach (string t in newDependents)
                {
                    if (t != null)
                    {
                        AddDependency(s, t); // Add the new dependencies
                    }
                    else
                    {
                        throw new ArgumentNullException("Each String in IEnumerable must not be null");
                    }
                }
            }
        }

        /// <summary>
        /// Removes all existing dependencies of the form (r,t).  Then, for each 
        /// s in newDependees, adds the dependency (s,t).
        /// Requires s != null and t != null. Throws ArgumentNullException if otherwise
        /// </summary>
        public void ReplaceDependees(string t, IEnumerable<string> newDependees)
        {
            if (t == null || newDependees == null)
            {
                throw new ArgumentNullException("Parameter must not be null");
            }

            if (depGraph.TryGetValue(t, out Dependency halfDep)) // Is 't' in the graph?
            {
                foreach (string x in halfDep.GetDees()) // If so, for each dependee of 't',
                {
                    depGraph.TryGetValue(x, out Dependency dependeeOfT); // Obtain the dependee
                    dependeeOfT.RemoveFromDependents(dependeeOfT.IndexOfDependent(t)); // Remove 't' from dent
                    depCount--; // Decrement size of graph
                }
                halfDep.ClearDependees(); // Clear 't' of all dependees

                foreach (string s in newDependees)
                {
                    if (s != null)
                    {
                        AddDependency(s, t); // Add replacement dependencies
                    }
                    else
                    {
                        throw new ArgumentNullException("Each String in IEnumerable must not be null");
                    }
                }
            }
        }

        /// <summary>
        /// Objects of the Dependency class represent "half"
        /// of a dependency within the DependencyGraph. Each
        /// string added to the graph as part of a dependency
        /// will have a Dependency object. Each holds a list of
        /// dependents and dependees.
        /// </summary>
        private class Dependency
        {
            internal String name; // The string this Dependency is for
            List<String> dents = new List<string>(); // List of dependents of 'name'
            List<String> dees = new List<string>(); // List of dependees of 'name'

            /// <summary>
            /// Create new dependency named 'n'
            /// </summary>
            /// <param name="n"></param>
            internal Dependency(string n)
            {
                name = n;
            }

            /// <summary>
            /// Add a dependent to this 'dependency'
            /// </summary>
            /// <param name="dent"></param>
            /// <returns></returns>
            internal bool AddDependent (string dent) // Add an item to 'dents'
            {
                if (!dents.Contains(dent)) // if it is not there already
                {
                    dents.Add(dent);
                    return true; // return true if add was successful
                }
                return false; // or false otherwise
            }

            /// <summary>
            /// Add a dependee to this 'dependency'
            /// </summary>
            /// <param name="dee"></param>
            internal void AddDependee(string dee) // Add an item to 'dees'
            {
                if (!dees.Contains(dee)) // If it is not there already
                {
                    dees.Add(dee);
                }
            }

            /// <summary>
            /// Return all dependents of the string in name ('dependency')
            /// </summary>
            /// <returns></returns>
            internal List<String> GetDents ()
            {
                return dents;
            }

            /// <summary>
            /// Return all dependees of the string in name (the 'dependency')
            /// </summary>
            /// <returns></returns>
            internal List<String> GetDees ()
            {
                return dees;
            }

            /// <summary>
            /// Return the index of a target dependent in the array of
            /// dependents, 'dents', in this Dependency
            /// </summary>
            /// <param name="target"></param>
            /// <returns></returns>
            internal int IndexOfDependent (String target)
            {
                return dents.IndexOf(target);
            }

            /// <summary>
            /// Return the index of a target dependee in the array of
            /// dependees, 'dees', in this Dependency
            /// </summary>
            /// <param name="target"></param>
            /// <returns></returns>
            internal int IndexOfDependee(String target)
            {
                return dees.IndexOf(target);
            }

            /// <summary>
            /// Remove item at certain index (of 'dents') from dependents
            /// of 'name'
            /// </summary>
            /// <param name="index"></param>
            internal void RemoveFromDependents (int index)
            {
                dents.RemoveAt(index);
            }

            /// <summary>
            /// Remove dependee of 'name' by index (removes from 'dees')
            /// </summary>
            /// <param name="index"></param>
            internal void RemoveFromDependees(int index)
            {
                dees.RemoveAt(index);
            }

            /// <summary>
            /// Removes all dependents of 'name'
            /// </summary>
            internal void ClearDependents()
            {
                dents.Clear();
            }

            /// <summary>
            /// Removes all dependees of 'name'
            /// </summary>
            internal void ClearDependees()
            {
                dees.Clear();
            }
        }
    }
}
