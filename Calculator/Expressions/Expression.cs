namespace Calculator.Expressions
{
    using System;
    using System.Collections.Generic;

    public abstract class Expression
    {
        protected List<Expression> operands;

        protected Expression(params Expression[] operands)
        {
            this.operands = new List<Expression>();
            foreach (var operand in operands)
            {
                this.operands.Add(operand);
            }
        }

        public abstract double Calculate();
    }
}
