using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Antlr4_practice
{
    class MyGrammarVisitor:MyGrammarBaseVisitor<object>
    {
        Dictionary<string, object> memory = new Dictionary<string, object>();

        public override object VisitParenthesis([NotNull] MyGrammarParser.ParenthesisContext context)
        {
            object obj = Visit(context.expression());
            return obj;
        }

        public override object VisitMultiplyDivide([NotNull] MyGrammarParser.MultiplyDivideContext context)
        {
            double left = Convert.ToDouble(Visit(context.expression(0)));
            double right = Convert.ToDouble(Visit(context.expression(1)));

            object obj = new object();
            if (context.operate.Type == MyGrammarParser.MUL)
            {
                obj = left * right;
            }else if(context.operate.Type == MyGrammarParser.DIV)
            {
                if (right == 0)
                {
                    throw new Exception("Cannot divide by zero");
                    obj = left / right;
                }
            }

            return obj;
        }

        public override object VisitAddSubtraction(MyGrammarParser.AddSubtractionContext context)
        {
            double left = Convert.ToDouble(Visit(context.expression(0)));
            double right = Convert.ToDouble(Visit(context.expression(1)));

            object obj = new object();
            if (context.operate.Type == MyGrammarParser.ADD)
            {
                obj = left + right;
            }
            else if (context.operate.Type == MyGrammarParser.SUB)
            {
                obj = left - right;
            }

            return obj;
        }

        public override object VisitNumber(MyGrammarParser.NumberContext context)
        {
            object obj = context.GetText();
            return obj;
        }
    }
}
