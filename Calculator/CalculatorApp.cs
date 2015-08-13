namespace Calculator
{
    using System;
    using System.Text.RegularExpressions;

    public class CalculatorApp
    {
        public static void Main()
        {
            string firstExpression = "2 + 3 - 4 / 5 * 7 /2 -   6.6 + 2.345";
            string secondExpression = "3+3-1";
            string thirdExpression = "2*2*2/2.3";
            string fourthExpression = "(1+2)/3 - (4.5*(5-2.4))";
            string fifthExpression = "(1+(1+1*(1/(1/1))))+1";

            Console.WriteLine("{0} = {1}", firstExpression, CalculatorEngine.Calculate(firstExpression));
            Console.WriteLine("{0} = {1}", secondExpression, CalculatorEngine.Calculate(secondExpression));
            Console.WriteLine("{0} = {1}", thirdExpression, CalculatorEngine.Calculate(thirdExpression));
            Console.WriteLine("{0} = {1}", fourthExpression, CalculatorEngine.Calculate(fourthExpression));
            Console.WriteLine("{0} = {1}", fifthExpression, CalculatorEngine.Calculate(fifthExpression));
        }
    }
}