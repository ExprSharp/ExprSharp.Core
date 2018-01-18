using iExpr;
using iExpr.Evaluators;
using iExpr.Helpers;
using iExpr.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using number = ExprSharp.RealNumber;

namespace ExprSharp.Runtime
{
    public static class StatsOperations
    {
        internal static List<number> GetAll(IExpr[] args, EvalContext context)
        {
            List<IExpr> ls = new List<IExpr>();

            foreach (var v in args)
            {
                switch (v)
                {
                    case CollectionValue c:
                        ls.AddRange(c);
                        break;
                    default:
                        ls.Add(v);
                        break;
                }
            }
            return new List<number>(ls.Select(x=>context.GetValue<number>(x)));
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public static PreFunctionValue Maximum { get; } = new PreFunctionValue(
            "max",
            (FunctionArgument _args, EvalContext cal) =>
            {
                var args = _args.Arguments;
                OperationHelper.AssertCertainValueThrowIf(Maximum, args);
                var vs = GetAll(args, cal);
                //var vs = OperationHelper.GetConcreteValue<double>(args);

                return new ConcreteValue(new number(vs.AsParallel().WithCancellation(cal.CancelToken.Token).Max()));
            }
            );

        /// <summary>
        /// 最小值
        /// </summary>
        public static PreFunctionValue Minimum { get; } = new PreFunctionValue(
            "min",
            (FunctionArgument _args, EvalContext cal) =>
            {
                var args = _args.Arguments;
                OperationHelper.AssertCertainValueThrowIf(Minimum, args);
                var vs = GetAll(args, cal);
                //var vs = OperationHelper.GetConcreteValue<double>(args);

                return new ConcreteValue(vs.AsParallel().WithCancellation(cal.CancelToken.Token).Min());
            }
            );

        /// <summary>
        /// 总和
        /// </summary>
        public static PreFunctionValue Total { get; } = new PreFunctionValue(
            "total",
            (FunctionArgument _args, EvalContext cal) =>
            {
                var args = _args.Arguments;
                OperationHelper.AssertCertainValueThrowIf(Total,args);
                var vs = GetAll(args, cal);
                //var vs = OperationHelper.GetConcreteValue<double>(args);

                return new ConcreteValue(vs.AsParallel().WithCancellation(cal.CancelToken.Token).Sum(x=>x));

            }
            );



        /// <summary>
        /// 平均值
        /// </summary>
        public static PreFunctionValue Mean { get; } = new PreFunctionValue(
            "mean",
            (FunctionArgument _args, EvalContext cal) =>
            {
                var args = _args.Arguments;
                OperationHelper.AssertCertainValueThrowIf(Mean,args);
                var vs = GetAll(args, cal);
                //var vs = OperationHelper.GetConcreteValue<double>(args);

                return new ConcreteValue(vs.AsParallel().WithCancellation(cal.CancelToken.Token).Average(x=>x));

            }
            );
    }
}
