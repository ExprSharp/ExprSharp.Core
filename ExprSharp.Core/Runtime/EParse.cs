using System;
using System.Collections.Generic;
using System.Text;
using iExpr;
using iExpr.Exceptions;
using iExpr.Exprs.Core;
using iExpr.Exprs.Program;
using iExpr.Parsers;
using iExpr.Values;

namespace ExprSharp.Runtime
{

    internal class BasicTokenChecker : TokenChecker
    {
        int pointcnt = 0;
        //bool isneg = false;

        public override void Clear()
        {
            base.Clear(); pointcnt = 0;
            //isneg = false;
        }

        public override bool? Append(char c)
        {
            var res = base.Append(c);
            if (c == '.') { pointcnt++; return null; }
            //if (c == '-') isneg = true;
            return res;
        }

        public override bool? Test(char c)
        {
            if (Flag == null)
                return char.IsDigit(c);// || c=='-';
            if (c == '.') if (pointcnt == 0) return null; else return false;
            return char.IsDigit(c);
        }
    }

    internal class StrTokenChecker : TokenChecker
    {
        bool? isended = null;
        //bool isneg = false;

        public override void Clear()
        {
            base.Clear(); isended = null;
            //isneg = false;
        }

        public override bool? Append(char c)
        {
            var res = base.Append(c);
            if (c == '"')
            {
                if (isended == null)
                {
                    isended = false;
                }
                else if (isended == false)
                {
                    isended = true;
                }
            }
            else
            {
                if (Flag !=true)//str starts(not null, because it has been appended)
                    isended = true;
            }
            return res;
        }

        public override bool? Test(char c)
        {
            if (Flag == null)
                return c == '"';// || c=='-';
            if (isended==true) return false;
            return true;
        }
    }

    public class EParse : iExpr.Parsers.ParseEnvironment
    {
        public EParse()
        {
            base.Operations = new OperationList();
            base.Operations.Add(LogicOperations.And);
            base.Operations.Add(LogicOperations.Or);
            base.Operations.Add(LogicOperations.Not);
            base.Operations.Add(LogicOperations.Xor);
            base.Operations.Add(iExpr.Exprs.Program.CoreOperations.Assign);
            base.Operations.Add(iExpr.Exprs.Program.CoreOperations.ReAssign);
            base.Operations.Add(ArithmeticOperations.Plus);
            base.Operations.Add(ArithmeticOperations.Minus);
            base.Operations.Add(ArithmeticOperations.Multiply);
            base.Operations.Add(ArithmeticOperations.Divide);
            base.Operations.Add(ArithmeticOperations.Mod);
            base.Operations.Add(ArithmeticOperations.Pow);
            base.Operations.Add(CompareOperations.Equal);
            base.Operations.Add(CompareOperations.Unequal);
            base.Operations.Add(CompareOperations.Bigger);
            base.Operations.Add(CompareOperations.Smaller);
            base.Operations.Add(CompareOperations.NotBigger);
            base.Operations.Add(CompareOperations.NotSmaller);
            base.Operations.Add(ControlStatements.Continue);
            base.Operations.Add(ControlStatements.Break);
            base.Operations.Add(ControlStatements.Return);
            base.Operations.Add(iExpr.Exprs.Core.CoreOperations.Lambda);
            base.Operations.Add(iExpr.Exprs.Core.CoreOperations.In);
            base.VariableChecker = new VariableTokenChecker();

            var tchecker = new TokenCheckerSelector();
            tchecker.Checkers.Add(new BasicTokenChecker());
            tchecker.Checkers.Add(new StrTokenChecker());

            base.BasicTokenChecker = tchecker;
            base.Constants = new ConstantList();
            Constants.Add(new ConstantToken("true", new ReadOnlyConcreteValue(true)));
            Constants.Add(new ConstantToken("false", new ReadOnlyConcreteValue(false)));
            Constants.Add(new ConstantToken("True", new ReadOnlyConcreteValue(true)));
            Constants.Add(new ConstantToken("False", new ReadOnlyConcreteValue(false)));
            Constants.Add(new ConstantToken("null", BuiltinValues.Null));
            Constants.AddClassValue(iExpr.Helpers.ClassValueBuilder.Build(typeof(ExprSharp.Gift)));
            Constants.AddClassValue(iExpr.Helpers.ClassValueBuilder.Build(typeof(ExprSharp.Math)));
            Constants.AddFunction(iExpr.Exprs.Core.CoreOperations.Length);
            Constants.AddFunction(iExpr.Exprs.Core.CoreOperations.HasVariable);
            Constants.AddFunction(iExpr.Exprs.Core.CoreOperations.List);
            Constants.AddFunction(iExpr.Exprs.Core.CoreOperations.Set);
            Constants.AddFunction(iExpr.Exprs.Core.CoreOperations.Tuple);
            Constants.AddFunction(StatsOperations.Maximum);
            Constants.AddFunction(StatsOperations.Minimum);
            Constants.AddFunction(StatsOperations.Mean);
            Constants.AddFunction(StatsOperations.Total);
            Constants.AddFunction(ControlStatements.For);
            Constants.AddFunction(ControlStatements.ForEach);
            Constants.AddFunction(ControlStatements.If);
            Constants.AddFunction(ControlStatements.While);
            Constants.AddFunction(ControlStatements.DoWhile);
            Constants.AddFunction(iExpr.Exprs.Program.CoreOperations.Array);
            Constants.AddFunction(iExpr.Exprs.Program.CoreOperations.Func);
            Constants.AddFunction(iExpr.Exprs.Core.CoreOperations.Class);
            Constants.AddFunction(ExprSharp.Runtime.Operations.Decimal);
            Constants.AddFunction(ExprSharp.Runtime.Operations.Print);
            base.BuildOpt();
        }

        public override IValue GetBasicValue(Symbol symbol)
        {
            if (double.TryParse(symbol.Value, out double res))
            {
                return new ConcreteValue(new RealNumber(res));
            }
            else return new ConcreteValue(symbol.Value.Substring(1, symbol.Value.Length-2));
        }
    }
}
