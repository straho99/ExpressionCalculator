namespace Calculator.Expressions
{
    using System.Collections.Generic;

    public class NumberExpression : Expression
    {
        private double number;

        public NumberExpression(double number)
        {
            this.number = number;
            this.operands = new List<Expression>();
        }

        public override double Calculate()
        {
            return this.number;
        }
    }
}
