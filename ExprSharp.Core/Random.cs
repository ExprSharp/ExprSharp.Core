using iExpr.Evaluators;
using iExpr.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using number = ExprSharp.RealNumber;

namespace ExprSharp
{
    [CanClassValue(Name = "random")]
    public class Random
    {
        static System.Random rand = new System.Random();
        
        [ClassMethod(Name = "next", ArgumentCount = 2)]
        public static number Next(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertCertainValueThrowIf(null,args);
            var ov = cal.GetValue<number>(args);
            switch (ov.Length)
            {
                case 0:
                    return new number(rand.Next());
                case 1:
                    return new number(rand.Next(ov[0]));
                case 2:
                    return new number(rand.Next(ov[0],ov[1]));
            }
            ExceptionHelper.RaiseWrongArgsNumber(null, 2, args?.Length ?? 0);
            return default;
        }

        [ClassMethod(Name = "nextd", ArgumentCount = 0)]
        public static number NextDouble(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(null,0,args);
            return new number(rand.NextDouble());
        }
    }
}
