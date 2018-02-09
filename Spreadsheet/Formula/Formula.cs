// Skeleton written by Joe Zachary for CS 3500, January 2017
// Formula and Evaluate methods written by Christopher Hanson (u1130422) for CS 3500, January 2017

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace Formulas
{
    /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  Provides a means to evaluate Formulas.  Formulas can be composed of
    /// non-negative floating-point numbers, variables, left and right parentheses, and
    /// the four binary operator symbols +, -, *, and /.  (The unary operators + and -
    /// are not allowed.)
    /// </summary>
    public struct Formula
    {
        /// <summary>
        /// IEnumerable that holds the tokens of formula
        /// </summary>
        private List<string> theFormula;
        /// <summary>
        /// ISet that holds all variables of formula
        /// </summary>
        private HashSet<string> theVars;

        /// <summary>
        /// Creates a Formula from a string that consists of a standard infix expression composed
        /// from non-negative floating-point numbers (using C#-like syntax for double/int literals), 
        /// variable symbols (a letter followed by zero or more letters and/or digits), left and right
        /// parentheses, and the four binary operator symbols +, -, *, and /.  White space is
        /// permitted between tokens, but is not required.
        /// 
        /// Examples of a valid parameter to this constructor are:
        ///     "2.5e9 + x5 / 17"
        ///     "(5 * 2) + 8"
        ///     "x*y-2+35/9"
        ///     
        /// Examples of invalid parameters are:
        ///     "_"
        ///     "-5.3"
        ///     "2 5 + 3"
        /// 
        /// If the formula is syntacticaly invalid, throws a FormulaFormatException with an 
        /// explanatory Message.
        /// </summary>
        public Formula(String formula) : this(formula, s => s, v => true)
        {
        }

        /// <summary>
        /// Constructor similar to above, but also has a Normalizer, which will convert any variables to a new format,
        /// and a Validator, which will determine whether a variable is valid according to rules given by the 
        /// Validator.
        /// </summary>
        public Formula(String formula, Normalizer n, Validator v)
        {
            if (formula == null || n == null || v == null)
            {
                throw new ArgumentNullException("One or more parameters are null; invalid");
            }

            int tokenCount = 0, lParenCt = 0, rParenCt = 0; // total tokens, total '(', and total ')'
            String lpPattern = @"^\($";
            String rpPattern = @"^\)$";
            String opPattern = @"^[\+\-*/]$";
            String varPattern = @"^[a-zA-Z][0-9a-zA-Z]*$";
            String doublePattern = @"^(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: e[\+-]?\d+)?$";
            Boolean wasOP = false, wasNVC = false; // booleans act as switches to show whether last token was a certain type
            IEnumerable<string> theTokens = GetTokens(formula);
            theFormula = new List<string>();
            theVars = new HashSet<string>();

            foreach (string s in theTokens)
            {
                String toAdd = s;
                if (wasOP) // if token follows opening parenth or operator
                {
                    if (!Regex.IsMatch(s, lpPattern) && !Regex.IsMatch(s, varPattern)
                        && !Regex.IsMatch(s, @"^\d+$") && !Regex.IsMatch(s, doublePattern, RegexOptions.IgnorePatternWhitespace))
                    {
                        throw new FormulaFormatException("Opening parentheses and operators must be followed by a number, " +
                            "variable, or opening parenthesis.");
                    }
                    wasOP = false; // turn switch off, as the case has been dealt with
                }
                if (wasNVC) // if token follows num, var, or closing parenth
                {
                    if (!Regex.IsMatch(s, opPattern) && !Regex.IsMatch(s, rpPattern))
                    {
                        throw new FormulaFormatException("Number, variable, or closing parenthesis must be followed " +
                            "by an operator or closing parenthesis");
                    }
                    wasNVC = false; // turn switch off, case has been dealt with
                }

                if (s.Equals("("))
                {
                    lParenCt++;
                    wasOP = true; // this token is an opening parenthesis, so change the corresponding Boolean
                }
                else if (s.Equals(")"))
                {
                    rParenCt++;
                    wasNVC = true; // token is a closing parenthesis, so change corresponding Boolean
                }
                if (rParenCt > lParenCt)
                {
                    throw new FormulaFormatException("More right parentheses than left parentheses");
                }

                if (Regex.IsMatch(s, opPattern))
                {
                    wasOP = true; // token was operator, so switch on Boolean
                }

                if (Regex.IsMatch(s, @"^\d+$") || Regex.IsMatch(s, doublePattern, RegexOptions.IgnorePatternWhitespace)
                    || Regex.IsMatch(s, varPattern))
                {
                    wasNVC = true; // doken was int, double, or variable, so switch on Boolean

                    if (Regex.IsMatch(s, varPattern))
                    {
                        String normalized = n(s);
                        if (!Regex.IsMatch(normalized, varPattern)) // If result of Normalizer(s) is still valid
                        {
                            throw new FormulaFormatException("Normalized variable is invalid");
                        }
                        if (!v(normalized))
                        {
                            throw new FormulaFormatException("Validator considers result of Normalizer invalid");
                        }
                        theVars.Add(normalized);
                        toAdd = normalized;
                    }
                }

                theFormula.Add(toAdd);
                tokenCount++;
            }
            if (tokenCount < 1)
            {
                throw new FormulaFormatException("At least one token is required.");
            }
            if (rParenCt != lParenCt)
            {
                throw new FormulaFormatException("Unequal numbers of right and left parentheses");
            }

            // Check first and last
            if (!Regex.IsMatch(theTokens.First(), lpPattern) && !Regex.IsMatch(theTokens.First(), varPattern)
                && !Regex.IsMatch(theTokens.First(), @"^\d+$")
                && !Regex.IsMatch(theTokens.First(), doublePattern, RegexOptions.IgnorePatternWhitespace))
            {
                throw new FormulaFormatException("First token must be a number, variable, or an opening parenthesis");
            }
            if (!Regex.IsMatch(theTokens.Last(), rpPattern) && !Regex.IsMatch(theTokens.Last(), varPattern)
                && !Regex.IsMatch(theTokens.Last(), @"^\d+$")
                && !Regex.IsMatch(theTokens.Last(), doublePattern, RegexOptions.IgnorePatternWhitespace))
            {
                throw new FormulaFormatException("Last token must be a number, variable, or a closing parenthesis");
            }
        }

        /// <summary>
        /// Evaluates this Formula, using the Lookup delegate to determine the values of variables.  (The
        /// delegate takes a variable name as a parameter and returns its value (if it has one) or throws
        /// an UndefinedVariableException (otherwise).  Uses the standard precedence rules when doing the evaluation.
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating 
        /// this Formula, its value is returned.  Otherwise, throws a FormulaEvaluationException  
        /// with an explanatory Message.
        /// </summary>
        public double Evaluate(Lookup lookup)
        {
            if (lookup == null)
            {
                throw new ArgumentNullException("Lookup is null; invalid");
            }

            Stack<double> val = new Stack<double>(); // stack holds values of tokens that are doubles, including var -> double
            Stack<string> op = new Stack<string>(); // holds tokens that are operators
            String doublePattern = @"^(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: e[\+-]?\d+)?$"; // detects tokens that are doubles

            if (theFormula == null)
            {
                theFormula = new List<string>();
                theFormula.Add("0");
            }

            foreach (string s in theFormula)
            {
                if (Regex.IsMatch(s, @"^\d+$") || Regex.IsMatch(s, doublePattern, RegexOptions.IgnorePatternWhitespace)) // token is double
                {
                    double sDb = Convert.ToDouble(s); // token value in type double
                    if (op.Count > 0 && op.Peek().Equals("*")) {
                        op.Pop();
                        double newVal = val.Pop() * sDb;
                        val.Push(newVal);
                    }
                    else if (op.Count > 0 && op.Peek().Equals("/"))
                    {
                        if (sDb == 0)
                        {
                            throw new FormulaEvaluationException("Cannot divide by zero");
                        }
                        op.Pop();
                        double newVal = val.Pop() / sDb;
                        val.Push(newVal);
                    }
                    else
                    {
                        val.Push(sDb);
                    }
                }
                else if (Regex.IsMatch(s, @"^[a-zA-Z][0-9a-zA-Z]*$")) // token is a variable
                {
                    double sDb; // will hold the double value of the token
                    try
                    {
                        sDb = lookup(s);
                    }
                    catch (UndefinedVariableException) // catch naturally thrown exception, throw desired
                    {
                        throw new FormulaEvaluationException("Variable has no value");
                    }


                    if (op.Count > 0 && op.Peek().Equals("*"))
                    {
                        op.Pop();
                        double newVal = val.Pop() * sDb;
                        val.Push(newVal);
                    }
                    else if (op.Count > 0 && op.Peek().Equals("/"))
                    {
                        if (sDb == 0)
                        {
                            throw new FormulaEvaluationException("Cannot divide by zero");
                        }
                        op.Pop();
                        double newVal = val.Pop() / sDb;
                        val.Push(newVal);
                    }
                    else
                    {
                        val.Push(sDb);
                    }
                }
                else if (s.Equals("+") || s.Equals("-")) // token is a + or -
                {
                    if (op.Count > 0 && op.Peek().Equals("+"))
                    {
                        op.Pop();
                        double sum = val.Pop() + val.Pop();
                        val.Push(sum);
                    }
                    else if (op.Count > 0 && op.Peek().Equals("-"))
                    {
                        op.Pop();
                        double val2 = val.Pop();
                        double val1 = val.Pop();
                        double dif = val1 - val2;
                        val.Push(dif);
                    }
                    op.Push(s);
                }
                else if (s.Equals("*") || s.Equals("/") || s.Equals("(")) // if token is one of these, push onto op stack
                {
                    op.Push(s);
                }
                else if (s.Equals(")"))
                {
                    if (op.Count > 0 && op.Peek().Equals("+")) // if there is a '+' on top of op stack
                    {
                        op.Pop();
                        double sum = val.Pop() + val.Pop();
                        val.Push(sum);
                    }
                    else if (op.Count > 0 && op.Peek().Equals("-")) // if there is a '-' on top of op stack
                    {
                        op.Pop();
                        double val2 = val.Pop();
                        double val1 = val.Pop();
                        double dif = val1 - val2;
                        val.Push(dif);
                    }
                    op.Pop();
                    if (op.Count > 0 && op.Peek().Equals("*")) // if there is a '*' on top of op stack
                    {
                        op.Pop();
                        double prod = val.Pop() * val.Pop();
                        val.Push(prod);
                    }
                    else if (op.Count > 0 && op.Peek().Equals("/")) // if there is a "/" on top of op stack
                    {
                        op.Pop();
                        double val2 = val.Pop();
                        double val1 = val.Pop();
                        if (val2 == 0)
                        {
                            throw new FormulaEvaluationException("Cannot divide by zero.");
                        }
                        val.Push(val1 / val2); // push quotient of top two values onto value stack

                    }
                }
            }

            if (op.Count == 0) // if there are no operators left in the stack
            {
                return val.Pop(); // return the final value
            }
            else // otherwise, there will be one operator and two values
            {
                double toReturn;
                if (op.Count > 0 && op.Peek().Equals("+"))
                {
                    toReturn = val.Pop() + val.Pop();
                }
                else
                {
                    double val2 = val.Pop();
                    double val1 = val.Pop();
                    toReturn = val1 - val2;
                }
                return toReturn;
            }
        }

        /// <summary>
        /// Returns all variables that are part of the formula as an ISet of type string
        /// </summary>
        /// <returns></returns>
        public ISet<string> GetVariables()
        {
            return theVars;
        }

        /// <summary>
        /// Override ToString() so that it returns 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            String toReturn = "";
            foreach (string s in theFormula)
            {
                toReturn += s;
            }

            return toReturn;
        }

        /// <summary>
        /// Given a formula, enumerates the tokens that compose it.  Tokens are left paren,
        /// right paren, one of the four operator symbols, a string consisting of a letter followed by
        /// zero or more digits and/or letters, a double literal, and anything that doesn't
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens.
            // NOTE:  These patterns are designed to be used to create a pattern to split a string into tokens.
            // For example, the opPattern will match any string that contains an operator symbol, such as
            // "abc+def".  If you want to use one of these patterns to match an entire string (e.g., make it so
            // the opPattern will match "+" but not "abc+def", you need to add ^ to the beginning of the pattern
            // and $ to the end (e.g., opPattern would need to be @"^[\+\-*/]$".)
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z][0-9a-zA-Z]*";

            // PLEASE NOTE:  I have added white space to this regex to make it more readable.
            // When the regex is used, it is necessary to include a parameter that says
            // embedded white space should be ignored.  See below for an example of this.
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: e[\+-]?\d+)?";
            String spacePattern = @"\s+";

            // Overall pattern.  It contains embedded white space that must be ignored when
            // it is used.  See below for an example of this.  This pattern is useful for 
            // splitting a string into tokens.
            String splittingPattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                            lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);

            // Enumerate matching tokens that don't consist solely of white space.
            // PLEASE NOTE:  Notice the second parameter to Split, which says to ignore embedded white space
            /// in the pattern.
            foreach (String s in Regex.Split(formula, splittingPattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }
        }
    }

    /// <summary>
    /// A Lookup method is one that maps some strings to double values.  Given a string,
    /// such a function can either return a double (meaning that the string maps to the
    /// double) or throw an UndefinedVariableException (meaning that the string is unmapped 
    /// to a value. Exactly how a Lookup method decides which strings map to doubles and which
    /// don't is up to the implementation of the method.
    /// </summary>
    public delegate double Lookup(string var);

    /// <summary>
    /// A Normalizer converts variables to a certain format. Given the variable 's' as a
    /// a string, the method will return a reformatted version of the variables.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public delegate string Normalizer(string s);

    /// <summary>
    /// A Validator method determines whether or not a formula is considered valid after
    /// an additional set of specified rules. The formula is passed in as a string, 's',
    /// and a boolean indicating whether or not the formula is valid is returned.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public delegate bool Validator(string s);

    /// <summary>
    /// Used to report that a Lookup delegate is unable to determine the value
    /// of a variable.
    /// </summary>
    [Serializable]
    public class UndefinedVariableException : Exception
    {
        /// <summary>
        /// Constructs an UndefinedVariableException containing whose message is the
        /// undefined variable.
        /// </summary>
        /// <param name="variable"></param>
        public UndefinedVariableException(String variable)
            : base(variable)
        {
        }
    }

    /// <summary>
    /// Used to report syntactic errors in the parameter to the Formula constructor.
    /// </summary>
    [Serializable]
    public class FormulaFormatException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public FormulaFormatException(String message) : base(message)
        {
        }
    }

    /// <summary>
    /// Used to report errors that occur when evaluating a Formula.
    /// </summary>
    [Serializable]
    public class FormulaEvaluationException : Exception
    {
        /// <summary>
        /// Constructs a FormulaEvaluationException containing the explanatory message.
        /// </summary>
        public FormulaEvaluationException(String message) : base(message)
        {
        }
    }
}
