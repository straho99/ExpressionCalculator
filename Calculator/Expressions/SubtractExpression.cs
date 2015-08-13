namespace Calculator.Expressions
{
    public class SubtractExpression : Expression
    {
        public SubtractExpression(Expression operand1, Expression operand2)
            : base(operand1, operand2)
        {
        }

        public override double Calculate()
        {
            return this.operands[0].Calculate() - this.operands[1].Calculate();
        }
    }
}
