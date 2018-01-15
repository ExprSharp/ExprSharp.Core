using iExpr;
using iExpr.Evaluators;
using iExpr.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExprSharp
{
    public static class Gift
    {
        public static string hi(FunctionArgument _args,EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(1, args);
            var name = cal.GetValue<string>(args[0]);
            return "hello, "+name;
        }
    }
}
