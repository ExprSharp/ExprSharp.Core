using ExprSharp.Core;
using iExpr.Evaluators;
using iExpr.Exprs.Program;
using iExpr.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExprSharp
{
    [CanClassValue(Name = "string")]
    public static class StringStatic
    {
        [ClassMethod(Name = "split", ArgumentCount = 2, IsReadOnly = true)]
        public static TupleValue Split(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(null, 2, args);
            OperationHelper.AssertCertainValueThrowIf(null, args);
            var ov = cal.GetValue<StringValue>(args);
            
            return new TupleValue(ov[0].Value.Split(ov[1].Value[0]).Select(x => new iExpr.Values.ConcreteValue(new StringValue(x))));
        }

        [ClassMethod(Name = "sub", ArgumentCount = 3, IsReadOnly = true)]
        public static StringValue Substring(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(null, 3, args);
            OperationHelper.AssertCertainValueThrowIf(null, args);
            var ov = cal.GetValue<StringValue>(args[0]);
            var begin= (int)cal.GetValue<RealNumber>(args[1]);
            var len = (int)cal.GetValue<RealNumber>(args[2]);
            return new StringValue(ov.Value.Substring(begin, len));
        }

        [ClassMethod(Name = "remove", ArgumentCount = 3, IsReadOnly = true)]
        public static StringValue Remove(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertCertainValueThrowIf(null, args);
            if (OperationHelper.AssertArgsNumber(3, args))
            {
                var ov = cal.GetValue<StringValue>(args[0]);
                var begin = (int)cal.GetValue<RealNumber>(args[1]);
                var len = (int)cal.GetValue<RealNumber>(args[2]);
                return new StringValue(ov.Value.Remove(begin, len));
            }
            else if(OperationHelper.AssertArgsNumber(2, args))
            {
                var ov = cal.GetValue<StringValue>(args[0]);
                var begin = (int)cal.GetValue<RealNumber>(args[1]);
                return new StringValue(ov.Value.Remove(begin));
            }
            ExceptionHelper.RaiseWrongArgsNumber(null, 3, args?.Length ?? 0);
            return default;
        }

        [ClassMethod(Name = "padleft", ArgumentCount = 3, IsReadOnly = true)]
        public static StringValue PadLeft(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertCertainValueThrowIf(null, args);
            if (OperationHelper.AssertArgsNumber(3, args))
            {
                var ov = cal.GetValue<StringValue>(args[0]);
                var len = (int)cal.GetValue<RealNumber>(args[1]);
                var ch = cal.GetValue<StringValue>(args[2]).Value[0];
                return new StringValue(ov.Value.PadLeft(len,ch));
            }
            else if (OperationHelper.AssertArgsNumber(2, args))
            {
                var ov = cal.GetValue<StringValue>(args[0]);
                var len = (int)cal.GetValue<RealNumber>(args[1]);
                return new StringValue(ov.Value.PadLeft(len));
            }
            ExceptionHelper.RaiseWrongArgsNumber(null, 3, args?.Length ?? 0);
            return default;
        }

        [ClassMethod(Name = "padright", ArgumentCount = 3, IsReadOnly = true)]
        public static StringValue PadRight(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertCertainValueThrowIf(null, args);
            if (OperationHelper.AssertArgsNumber(3, args))
            {
                var ov = cal.GetValue<StringValue>(args[0]);
                var len = (int)cal.GetValue<RealNumber>(args[1]);
                var ch = cal.GetValue<StringValue>(args[2]).Value[0];
                return new StringValue(ov.Value.PadRight(len, ch));
            }
            else if (OperationHelper.AssertArgsNumber(2, args))
            {
                var ov = cal.GetValue<StringValue>(args[0]);
                var len = (int)cal.GetValue<RealNumber>(args[1]);
                return new StringValue(ov.Value.PadRight(len));
            }
            ExceptionHelper.RaiseWrongArgsNumber(null, 3, args?.Length ?? 0);
            return default;
        }

        [ClassMethod(Name = "replace", ArgumentCount = 3, IsReadOnly = true)]
        public static StringValue Replace(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertCertainValueThrowIf(null, args);
            OperationHelper.AssertArgsNumberThrowIf(null, 3, args);
            var ov = cal.GetValue<StringValue>(args);
            return new StringValue(ov[0].Value.Replace(ov[1].Value, ov[2].Value));
        }

        [ClassMethod(Name = "startsWith", ArgumentCount = 2, IsReadOnly = true)]
        public static bool StartsWith(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertCertainValueThrowIf(null, args);
            OperationHelper.AssertArgsNumberThrowIf(null, 2, args);
            var ov = cal.GetValue<StringValue>(args);
            return ov[0].Value.StartsWith(ov[1].Value);
        }

        [ClassMethod(Name = "endsWith", ArgumentCount = 2, IsReadOnly = true)]
        public static bool EndsWith(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertCertainValueThrowIf(null, args);
            OperationHelper.AssertArgsNumberThrowIf(null, 2, args);
            var ov = cal.GetValue<StringValue>(args);
            return ov[0].Value.EndsWith(ov[1].Value);
        }

        [ClassMethod(Name = "isempty", ArgumentCount = 1, IsReadOnly = true)]
        public static bool IsNullOrEmpty(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertCertainValueThrowIf(null, args);
            OperationHelper.AssertArgsNumberThrowIf(null, 1, args);
            var ov = cal.GetValue<StringValue>(args[0]);
            return String.IsNullOrEmpty(ov);
        }
    }
}
