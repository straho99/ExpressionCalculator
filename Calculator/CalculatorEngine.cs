namespace Calculator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Calculator.Expressions;

    public class CalculatorEngine
    {
        private static Stack<char> operators = new Stack<char>();
        private static List<dynamic> expression = new List<dynamic>();

        /// <summary>
        /// Calculates an arithmetic expression, represented as a string.
        /// </summary>
        /// <param name="expressionString">The string containign the arithmetic expression.</param>
        /// <returns>The result of the calculation as a double number.</returns>
        public static double Calculate(string expressionString)
        {
            string simplifiedExpression = CalculateSubexpressions(expressionString);
            return CalculateSimpleExpression(simplifiedExpression);
        }

        /// <summary>
        /// Calculate a 'simple' expression - one that has no subexpressions in parenthesis.
        /// Works iteratively, starting with the operator on top of the operator's stack.
        /// Finishes when the expression list is shrunk into one root expression, which
        /// is basically a tree with many child expressions.
        /// </summary>
        /// <param name="expressionString">The string containing the expression to be calculated.</param>
        /// <returns>Retirns the resut from the calculation in the form of a double number.</returns>
        private static double CalculateSimpleExpression(string expressionString)
        {
            InitOperators();
            expression.Clear();
            ParseExpression(expressionString);

            while (expression.Count > 1)
            {
                char currentOperator = operators.Pop();
                while (expression.Contains(currentOperator))
                {
                    CreateExpression(expression, currentOperator);
                }
            }

            return expression[0].Calculate();
        }

        /// <summary>
        /// Finds the first instance of currentOperator (i.e. '+'), takes the operands on the left and right
        /// to the operator and builds an Expression instance (i.e. AddExpression in case of '+').
        /// </summary>
        /// <param name="expression">A list containing numbers, operators and expressions.</param>
        /// <param name="currentOperator">The symbol of the current operator to be processed.</param>
        private static void CreateExpression(List<dynamic> expression, char currentOperator)
        {
            int operatorIndex = expression.IndexOf(currentOperator);
            Expression operation;
            dynamic leftOperand, rightOperand;
            try
            {
                if (expression.ElementAt(operatorIndex - 1) is double)
                {
                    leftOperand = new NumberExpression(expression.ElementAt(operatorIndex - 1));
                }
                else if (expression.ElementAt(operatorIndex - 1) is Expression)
                {
                    leftOperand = expression.ElementAt(operatorIndex - 1);
                }
                else
                {
                    throw new ArgumentException("Invalid expression string.");
                }

                if (expression.ElementAt(operatorIndex + 1) is double)
                {
                    rightOperand = new NumberExpression(expression.ElementAt(operatorIndex + 1));
                }
                else if (expression.ElementAt(operatorIndex + 1) is Expression)
                {
                    rightOperand = expression.ElementAt(operatorIndex + 1);
                }
                else
                {
                    throw new ArgumentException("Invalid expression string.");
                }
            }
            catch (Exception ex)
            {
                
                throw new ArgumentException("Invalid expression string.");
            }

            switch (currentOperator)
            {
                case '+':
                    operation = new AddExpression(leftOperand, rightOperand);
                    break;
                case '-':
                    operation = new SubtractExpression(leftOperand, rightOperand);
                    break;
                case '*':
                    operation = new MultiplyExpression(leftOperand, rightOperand);
                    break;
                case '/':
                    operation = new DivideExpression(leftOperand, rightOperand);
                    break;
                default:
                    operation = new NumberExpression(0);
                    break;
            }

            expression.RemoveAt(operatorIndex + 1);
            expression.RemoveAt(operatorIndex);
            expression.RemoveAt(operatorIndex - 1);
            expression.Insert(operatorIndex - 1, operation);
        }

        /// <summary>
        /// Loads the operators in the stack before each calculation. (On each calculation the stack is emptied.)
        /// </summary>
        private static void InitOperators()
        {
            operators.Push('+');
            operators.Push('-');
            operators.Push('*');
            operators.Push('/');
        }

        /// <summary>
        /// Parses the input expression into numbers (doubles) and operators (as strings)
        /// </summary>
        /// <param name="expressionString">String containing the expression to be parsed.</param>
        private static void ParseExpression(string expressionString)
        {
            string currentNumber = "";

            foreach (var character in expressionString)
            {
                if (operators.Contains(character))
                {
                    try
                    {
                        double number = double.Parse(currentNumber);
                        expression.Add(number);
                        expression.Add(character);
                        currentNumber = "";
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException("Invalid expression string.");
                    }
                }
                else
                {
                    currentNumber += character;
                }
            }

            if (currentNumber.Length > 0)
            {
                try
                {
                    double number = double.Parse(currentNumber);
                    expression.Add(number);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Invalid expression string.");
                }
            }
        }

        /// <summary>
        /// Extracts all expressions inside parentheses, calculates them and replaces them with the result
        /// in the original expression string.
        /// </summary>
        /// <param name="expression">The string, containing the expression to be calculated.</param>
        /// <returns>A new string where all expressions, contained inside parenthesis are replaced
        /// with their respective results.</returns>
        private static string CalculateSubexpressions(string expression)
        {
            string text = expression;
            string pattern = @"\(([^(]*?)\)";
            var matches = Regex.Matches(text, pattern);
            while (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    string currentExpression = match.Groups[1].Value;
                    double result = CalculateSimpleExpression(currentExpression);
                    text = text.Replace(match.Value, result.ToString());
                }
                matches = Regex.Matches(text, pattern);
            }

            return text;
        }
    }
}