# ExpressionCalculator
A small class that parses arithmetic expressions and calculates the results, taking into account operators' precedence and parenthesis. Doesn't use shunting yard algorithm.

Currently only works with addition, subtraction, division and multiplication. Additional operations can be added easily by inheriting the base Expression class and adding one more case to the switch inside CalculatorEngine, CreateExpression method. Also, the symbol of the operator must be added to the operator's stack.
