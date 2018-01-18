using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ExprSharp.Core;
using iExpr;
using iExpr.Evaluators;
using iExpr.Exprs.Program;
using iExpr.Helpers;
using iExpr.Values;

namespace ExprSharp.Runtime
{
    public class EEContext : EvalContext
    {
        static EEContext Context { get; set; }

        protected EEContext() {
            if (Context == null) Context = this;
        }

        public override EvalContext GetChild(VariableFindMode mode = VariableFindMode.UpAll)
        {
            return new EEContext() { Evaluator = Evaluator, CancelToken = CancelToken, Parent = this };
        }

        public static T ConvertVal<T>(object obj)
        {
            return Context.ConvertValue<T>(obj);
        }

        protected override T ConvertValue<T>(object obj)
        {
            //if(obj==null)Console.WriteLine("!!");
            switch (obj)
            {
                case double d:
                case int i:
                case bool b:
                    try
                    {
                        return base.ConvertValue<T>(obj);
                    }
                    catch
                    {
                        double v = Convert.ToDouble(obj);
                        return (T)(object)(new RealNumber(v));
                    }
                case RealNumber r:
                    try
                    {
                        var d = (double)r;
                        return (T)Convert.ChangeType(d, typeof(T));
                    }
                    catch//(Exception ex)
                    {
                        return (T)(object)r;
                    }
                case string s:
                    return (T)(object)s;
                case StringValue s:
                    {
                        if (typeof(T) ==typeof(string))
                            return (T)(object)s.Value;
                        else return (T)(object)s;
                    }
            }
            /*if(exp.Value is RealNumber)
            {
                try
                {
                    return (T)Convert.ChangeType((double)((RealNumber)exp.Value),typeof(T));
                }
                catch { }
            }*/
            return base.ConvertValue<T>(obj);
        }

        public new static EvalContext Create(CancellationTokenSource cancel)
        {
            var res = new EEContext
            {
                CancelToken = cancel
            };
            return res;
        }
    }

    public class EEval : iExpr.Evaluators.EvalEnvironment
    {
        public EEval()
        {
            base.Evaluator = new iExpr.Evaluators.ExprEvaluator();
        }

        public override EvalContext CreateContext(CancellationTokenSource cancel = null)
        {
            var res = EEContext.Create(cancel ?? new System.Threading.CancellationTokenSource());
            res.Evaluator = Evaluator;
            res.Variables = Variables;
            return res;
        }
    }
}
