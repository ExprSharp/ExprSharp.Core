﻿using iExpr.Evaluators;
using iExpr.Helpers;
using iExpr.Values;
using System;
using System.Collections.Generic;
using System.Text;
using number = ExprSharp.RealNumber;

namespace ExprSharp
{
    [CanClassValue(Name = "math")]
    public static class Math
    {
        [ClassMethod(Name = "ceil",ArgumentCount =1,IsReadOnly =true)]
        public static number Ceil(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(null,1, args);
            OperationHelper.AssertCertainValueThrowIf(null, args);
            var ov = cal.GetValue<number>(args[0]);
            return new number(System.Math.Ceiling((double)ov));
        }
        
        [ClassMethod(Name = "floor", ArgumentCount = 1, IsReadOnly = true)]
        public static number Floor(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(null, 1, args);
            OperationHelper.AssertCertainValueThrowIf(null, args);
            var ov = cal.GetValue<number>(args[0]);
            return new number(System.Math.Floor((double)ov));
        }

        [ClassMethod(Name = "round", ArgumentCount = 1, IsReadOnly = true)]
        public static number Round(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(null, 1, args);
            OperationHelper.AssertCertainValueThrowIf(null, args);
            var ov = cal.GetValue<number>(args[0]);
            return new number(System.Math.Round((double)ov));
        }

        [ClassMethod(Name = "sign", ArgumentCount = 1, IsReadOnly = true)]
        public static number Sign(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(null, 1, args);
            OperationHelper.AssertCertainValueThrowIf(null, args);
            var ov = cal.GetValue<number>(args[0]);
            return new number(System.Math.Sign((decimal)ov));
        }

        [ClassMethod(Name = "exp", ArgumentCount = 1, IsReadOnly = true)]
        public static number Exp(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(null, 1, args);
            OperationHelper.AssertCertainValueThrowIf(null, args);
            var ov = cal.GetValue<number>(args[0]);
            return new number(System.Math.Exp((double)ov));
        }

        [ClassMethod(Name = "abs", ArgumentCount = 1, IsReadOnly = true)]
        public static number Abs(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(null, 1, args);
            OperationHelper.AssertCertainValueThrowIf(null, args);
            var ov = cal.GetValue<number>(args[0]);
            return new number(System.Math.Abs((double)ov));
        }

        [ClassMethod(Name = "sin", ArgumentCount = 1, IsReadOnly = true)]
        public static number Sin(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(null, 1, args);
            OperationHelper.AssertCertainValueThrowIf(null, args);
            var ov = cal.GetValue<number>(args[0]);
            return new number(System.Math.Sin(ov));
        }

        [ClassMethod(Name = "cos", ArgumentCount = 1, IsReadOnly = true)]
        public static number Cos(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(null, 1, args);
            OperationHelper.AssertCertainValueThrowIf(null, args);
            var ov = cal.GetValue<number>(args[0]);
            return new number(System.Math.Cos(ov));
        }

        [ClassMethod(Name = "tan", ArgumentCount = 1, IsReadOnly = true)]
        public static number Tan(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(null, 1, args);
            OperationHelper.AssertCertainValueThrowIf(null, args);
            var ov = cal.GetValue<number>(args[0]);
            return new number(System.Math.Tan(ov));
        }

        [ClassMethod(Name = "asin", ArgumentCount = 1, IsReadOnly = true)]
        public static number ArcSin(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(null, 1, args);
            OperationHelper.AssertCertainValueThrowIf(null, args);
            var ov = cal.GetValue<number>(args[0]);
            return new number(System.Math.Asin(ov));
        }

        [ClassMethod(Name = "acos", ArgumentCount = 1, IsReadOnly = true)]
        public static number ArcCos(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(null, 1, args);
            OperationHelper.AssertCertainValueThrowIf(null, args);
            var ov = cal.GetValue<number>(args[0]);
            return new number(System.Math.Acos(ov));
        }

        [ClassMethod(Name = "atan", ArgumentCount = 1, IsReadOnly = true)]
        public static number ArcTan(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(null, 1, args);
            OperationHelper.AssertCertainValueThrowIf(null, args);
            var ov = cal.GetValue<number>(args[0]);
            return new number(System.Math.Atan(ov));
        }

        [ClassMethod(Name = "ln", ArgumentCount = 1, IsReadOnly = true)]
        public static number Ln(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(null, 1, args);
            OperationHelper.AssertCertainValueThrowIf(null, args);
            var ov = cal.GetValue<number>(args[0]);
            return new number(System.Math.Log(ov));
        }

        [ClassMethod(Name = "log", ArgumentCount = 2, IsReadOnly = true)]
        public static number Log(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(null, 2, args);
            OperationHelper.AssertCertainValueThrowIf(null, args);
            var ov = cal.GetValue<number>(args);
            return new number(System.Math.Log(ov[1]) / System.Math.Log(ov[0]));
        }

        [ClassField(Name = "e", IsReadOnly = true)]
        public static ReadOnlyConcreteValue E { get; } = new ReadOnlyConcreteValue(new number(iExpr.Extensions.Math.Numerics.BigDecimal.Parse(
            "2.7182818284590452353602874713526624977572470936999595749669676277240766303535475945713821785251664274")));
        [ClassField(Name = "pi", IsReadOnly = true)]
        public static ReadOnlyConcreteValue PI { get; } = new ReadOnlyConcreteValue(new number(iExpr.Extensions.Math.Numerics.BigDecimal.Parse(
            "3.141592653589793238462643383279502884197169399375105820974944592307816406286208998628034825342117067982")));
    }
}
