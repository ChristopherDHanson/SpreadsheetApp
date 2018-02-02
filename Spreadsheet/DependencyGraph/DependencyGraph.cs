// Skeleton implementation written by Joe Zachary for CS 3500, January 2018.
// Dependency class, method bodies written by Christopher Hanson for CS 3500, January 2018.

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
        Dictionary<String, Dependency> depGraph;
        int depCount = 0;

        /// <summary>
        /// Creates a DependencyGraph containing no dependencies.
        /// </summary>
        public DependencyGraph()
        {
            depGraph = new Dictionary<string, Dependency>();
        }

        /// <summary>
        /// The number of dependencies in the DependencyGraph.
        /// </summary>
        public int Size
        {
            get { return depGraph.Count; }
        }

        /// <summary>
        /// Reports whether dependents(s) is non-empty.  Requires s != null.
        /// </summary>
        public bool HasDependents(string s)
        {
            if (depGraph.TryGetValue(s, out Dependency halfDep))
            {
                if (halfDep.GetDents().Count > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Reports whether dependees(s) is non-empty.  Requires s != null.
        /// </summary>
        public bool HasDependees(string s)
        {
            if (depGraph.TryGetValue(s, out Dependency halfDep))
            {
                if (halfDep.GetDees().Count > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Enumerates dependents(s).  Requires s != null.
        /// </summary>
        public IEnumerable<string> GetDependents(string s)
        {
            if (s == null)
            {
                throw new InvalidParameterException("Parameter must not be null");
            }

            if (depGraph.TryGetValue(s, out Dependency halfDep))
            {
                return halfDep.GetDents();
            }

            return null;
        }

        /// <summary>
        /// Enumerates dependees(s).  Requires s != null.
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            if (s == null)
            {
                throw new InvalidParameterException("Parameter must not be null");
            }

            if (depGraph.TryGetValue(s, out Dependency halfDep))
            {
                return halfDep.GetDees();
            }

            return null;
        }

        /// <summary>
        /// Adds the dependency (s,t) to this DependencyGraph.
        /// This has no effect if (s,t) already belongs to this DependencyGraph.
        /// Requires s != null and t != null.
        /// </summary>
        public void AddDependency(string s, string t)
        {
            if (s == null || t == null)
            {
                throw new InvalidParameterException("Strings cannot be null");
            }

            Dependency sDep, tDep;
            if (depGraph.TryGetValue(s, out sDep))
            {
            }
            else
            {
                sDep = new Dependency(s);
                depGraph.Add(s, sDep);
            }

            if (depGraph.TryGetValue(t, out tDep))
            {
            }
            else
            {
                tDep = new Dependency(t);
                depGraph.Add(t, tDep);
            }

            if (sDep.AddDependent(t))
            {
                tDep.AddDependee(s);
                depCount++;
            }
        }

        /// <summary>
        /// Removes the dependency (s,t) from this DependencyGraph.
        /// Does nothing if (s,t) doesn't belong to this DependencyGraph.
        /// Requires s != null and t != null.
        /// </summary>
        public void RemoveDependency(string s, string t)
        {
            if (s == null || t == null) {
                throw new InvalidParameterException("Parameters must not be null");
            }

            if (depGraph.TryGetValue(s, out Dependency halfDepS))
            {
                int ind = halfDepS.IndexOfDependent(t);
                if (ind > -1)
                {
                    halfDepS.RemoveFromDependents(ind);

                    if (halfDepS.GetDents().Count == 0 && halfDepS.GetDees().Count == 0)
                    {
                        depGraph.Remove(s);
                    }

                    depGraph.TryGetValue(t, out Dependency halfDepT);
                    halfDepT.RemoveFromDependees(halfDepT.IndexOfDependee(s));

                    if (halfDepT.GetDents().Count == 0 && halfDepT.GetDees().Count == 0)
                    {
                        depGraph.Remove(t);
                    }

                    depCount--;
                }
            }

        }

        /// <summary>
        /// Removes all existing dependencies of the form (s,r).  Then, for each
        /// t in newDependents, adds the dependency (s,t).
        /// Requires s != null and t != null.
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            if (s == null || newDependents == null)
            {
                throw new InvalidParameterException("Parameter must not be null");
            }

            if (depGraph.TryGetValue(s, out Dependency halfDep))
            {
                foreach (string x in halfDep.GetDents())
                {
                    depGraph.TryGetValue(x, out Dependency dependentOfS);
                    dependentOfS.RemoveFromDependees(dependentOfS.IndexOfDependee(s));
                    depCount--;
                }
                halfDep.ClearDependents();

                foreach (string t in newDependents)
                {
                    if (t != null)
                    {
                        AddDependency(s, t);
                    }
                }
            }
        }

        /// <summary>
        /// Removes all existing dependencies of the form (r,t).  Then, for each 
        /// s in newDependees, adds the dependency (s,t).
        /// Requires s != null and t != null.
        /// </summary>
        public void ReplaceDependees(string t, IEnumerable<string> newDependees)
        {
            if (t == null || newDependees == null)
            {
                throw new InvalidParameterException("Parameter must not be null");
            }

            if (depGraph.TryGetValue(t, out Dependency halfDep))
            {
                foreach (string x in halfDep.GetDees())
                {
                    depGraph.TryGetValue(x, out Dependency dependeeOfT);
                    dependeeOfT.RemoveFromDependents(dependeeOfT.IndexOfDependent(t));
                    depCount--;
                }
                halfDep.ClearDependents();

                foreach (string s in newDependees)
                {
                    if (s != null)
                    {
                        AddDependency(s, t);
                    }
                }
            }
        }

        /// <summary>
        /// Technically each is only half of a dependency
        /// </summary>
        private class Dependency
        {
            internal String name;
            List<String> dents;
            List<String> dees;

            internal Dependency(string n)
            {
                name = n;
            }

            internal bool AddDependent (string dent)
            {
                if (!dents.Contains(dent))
                {
                    dents.Add(dent);
                    return true;
                }
                return false;
            }

            internal void AddDependee (string dee)
            {
                if (!dees.Contains(dee))
                {
                    dees.Add(dee);
                }
            }

            internal List<String> GetDents ()
            {
                return dents;
            }

            internal List<String> GetDees ()
            {
                return dees;
            }

            internal int IndexOfDependent (String target)
            {
                return dents.IndexOf(target);
            }

            internal int IndexOfDependee(String target)
            {
                return dees.IndexOf(target);
            }

            internal void RemoveFromDependents (int index)
            {
                dents.RemoveAt(index);
            }

            internal void RemoveFromDependees(int index)
            {
                dees.RemoveAt(index);
            }

            internal void ClearDependents()
            {
                dents.Clear();
            }

            internal void ClearDependees()
            {
                dees.Clear();
            }
        }
    }

    [Serializable]
    public class InvalidParameterException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public InvalidParameterException(String message) : base(message)
        {
        }
    }
}
