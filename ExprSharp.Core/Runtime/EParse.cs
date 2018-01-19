using System;
using System.Collections.Generic;
using System.Text;
using ExprSharp.Core;
using iExpr;
using iExpr.Exceptions;
using iExpr.Exprs.Core;
using iExpr.Exprs.Program;
using iExpr.Extensions.Math.Numerics;
using iExpr.Parsers;
using iExpr.Values;

namespace ExprSharp.Runtime
{
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
            base.Operations.Add(iExpr.Exprs.Program.CoreOperations.AssignPlus);
            base.Operations.Add(iExpr.Exprs.Program.CoreOperations.AssignMinus);
            base.Operations.Add(iExpr.Exprs.Program.CoreOperations.AssignMultiply);
            base.Operations.Add(iExpr.Exprs.Program.CoreOperations.AssignDivide);
            base.Operations.Add(iExpr.Exprs.Program.CoreOperations.AssignPow);
            base.Operations.Add(iExpr.Exprs.Program.CoreOperations.AssignMod);
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
            tchecker.Checkers.Add(new iExpr.Parsers.RealNumberTokenChecker());
            tchecker.Checkers.Add(new iExpr.Parsers.StringTokenChecker());

            base.BasicTokenChecker = tchecker;
            base.Constants = new ConstantList();
            Constants.Add(new ConstantToken("true", BuiltinValues.True));
            Constants.Add(new ConstantToken("false", BuiltinValues.False));
            Constants.Add(new ConstantToken("True", BuiltinValues.True));
            Constants.Add(new ConstantToken("False", BuiltinValues.False));
            Constants.Add(new ConstantToken("null", BuiltinValues.Null));
            Constants.AddClassValue(iExpr.Helpers.ClassValueBuilder.BuildStaticAndCtor(typeof(ExprSharp.Gift)),false);
            Constants.AddClassValue(iExpr.Helpers.ClassValueBuilder.BuildStaticAndCtor(typeof(ExprSharp.Stack)), false);
            Constants.AddClassValue(iExpr.Helpers.ClassValueBuilder.BuildStaticAndCtor(typeof(ExprSharp.Queue)), false);
            //Constants.AddClassValue(iExpr.Helpers.ClassValueBuilder.BuildStaticAndCtor(typeof(iExpr.Exprs.Program.DictionaryValue)), false);
            Constants.AddClassValue(iExpr.Helpers.ClassValueBuilder.BuildStaticAndCtor(typeof(ExprSharp.Math)),true);
            Constants.AddClassValue(iExpr.Helpers.ClassValueBuilder.BuildStaticAndCtor(typeof(ExprSharp.StringStatic)), true);
            Constants.AddClassValue(iExpr.Helpers.ClassValueBuilder.BuildStaticAndCtor(typeof(ExprSharp.Random)),true);
            Constants.AddClassValue(iExpr.Helpers.ClassValueBuilder.BuildStaticAndCtor(typeof(ExprSharp.FileStatic)), true);
            Constants.AddFunction(iExpr.Exprs.Core.CoreOperations.Length);
            Constants.AddFunction(iExpr.Exprs.Core.CoreOperations.HasVariable);
            Constants.AddFunction(iExpr.Exprs.Program.CoreOperations.List);
            Constants.AddFunction(iExpr.Exprs.Program.CoreOperations.Set);
            Constants.AddFunction(iExpr.Exprs.Program.CoreOperations.Tuple);
            Constants.AddFunction(iExpr.Exprs.Program.CoreOperations.Dict);
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
            Constants.AddFunction(iExpr.Exprs.Core.CoreOperations.Iterator);
            Constants.AddFunction(ExprSharp.Runtime.Operations.Print);
            Constants.AddFunction(ExprSharp.Runtime.Operations.Input);
            Constants.AddFunction(ExprSharp.Runtime.Operations.Integer);
            Constants.AddFunction(ExprSharp.Runtime.Operations.Number);
            Constants.AddFunction(ExprSharp.Runtime.Operations.String);
            Constants.AddFunction(ExprSharp.Runtime.Operations.Decimal);
            Constants.AddFunction(ExprSharp.Runtime.Operations.Sorted);
            Constants.AddFunction(ExprSharp.Runtime.Operations.Select);
            Constants.AddFunction(ExprSharp.Runtime.Operations.Where);
            Constants.AddFunction(ExprSharp.Runtime.Operations.Zip);
            Constants.AddFunction(ExprSharp.Runtime.Operations.Reduce);
            Constants.AddFunction(ExprSharp.Runtime.Operations.Range);
            Constants.AddFunction(ExprSharp.Runtime.Operations.Import);
            Constants.AddFunction(ExprSharp.Runtime.Operations.Module);
            Constants.AddFunction(ExprSharp.Runtime.Operations.View);
            base.BuildOpt();
        }

        public override SymbolType GetSpecialSymbolType(char expr)
        {
            if (expr == ';') return SymbolType.Comma;
            return base.GetSpecialSymbolType(expr);
        }

        public override IValue GetBasicValue(Symbol symbol)
        {
            if (symbol.Value.StartsWith("\""))
            {
                return new ConcreteValue(new StringValue( symbol.Value.Substring(1, symbol.Value.Length - 2)));
            }
            else return new ConcreteValue(new RealNumber(BigDecimal.Parse(symbol.Value)));
        }

        public override ListValueBase GetListValue()
        {
            return new iExpr.Exprs.Program.ListValue();
        }

        public override TupleValueBase GetTupleValue()
        {
            return new iExpr.Exprs.Program.TupleValue();
        }

        public override SetValueBase GetSetValue()
        {
            return new iExpr.Exprs.Program.SetValue();
        }
    }
}
