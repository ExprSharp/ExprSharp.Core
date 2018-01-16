using iExpr;
using iExpr.Evaluators;
using iExpr.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExprSharp
{
    [CanClassValue(Name ="gift")]
    public static class Gift
    {
        [ClassMethod(Name ="hi",ArgumentCount =1)]
        public static string Hi(FunctionArgument _args,EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(1, args);
            var name = cal.GetValue<string>(args[0]);
            return "hello, "+name;
        }
    }
}
