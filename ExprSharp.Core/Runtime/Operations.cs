using iExpr.Evaluators;
using iExpr.Exceptions;
using iExpr.Extensions.Math.Numerics;
using iExpr.Values;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExprSharp.Runtime
{
    public class Operations
    {
        public static PreFunctionValue Decimal { get; } = new PreFunctionValue(
                    "decimal",
                    (FunctionArgument _args, EvalContext cal) =>
                    {
                        var args = _args.Arguments;
                        if (args.Length == 0) return new ConcreteValue(new RealNumber(new BigDecimal(0, 20)));
                        else if (args.Length == 1)
                        {
                            int p = cal.GetValue<int>(args[0]);
                            return new ConcreteValue(new RealNumber(new BigDecimal(0, p)));
                        }
                        else
                        {
                            throw new EvaluateException("wrong argument count");
                        }
                    },
                    1
                    );
        public static PreFunctionValue Print { get; } = new PreFunctionValue(
                    "print",
                    (FunctionArgument _args, EvalContext cal) =>
                    {
                        var args = _args.Arguments;
                        foreach(var v in args)
                        {
                            Console.Write(v.ToString() + " ");
                        }
                        Console.WriteLine();
                        return BuiltinValues.Null;
                    },
                    -1
                    );
    }
}
