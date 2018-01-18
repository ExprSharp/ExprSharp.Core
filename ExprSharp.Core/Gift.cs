using iExpr;
using iExpr.Evaluators;
using iExpr.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExprSharp
{
    [CanClassValue(Name ="gift")]
    public class Gift
    {
        [ClassField(Name="name")]
        public string Name { get; set; }

        [ClassMethod(Name ="hi",ArgumentCount =0)]
        public string Hi(FunctionArgument _args,EvalContext cal)
        {
            return "hello, "+Name;
        }

        [ClassCtorMethod(ArgumentCount =1)]
        public Gift(FunctionArgument _args, EvalContext cal)
        {
            OperationHelper.AssertArgsNumberThrowIf(this,1, _args.Arguments);
            Name = cal.GetValue<string>(_args.Arguments[0]);
        }
    }
}
