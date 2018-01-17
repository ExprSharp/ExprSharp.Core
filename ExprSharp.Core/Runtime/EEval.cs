using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using iExpr;
using iExpr.Evaluators;
using iExpr.Helpers;
using iExpr.Values;

namespace ExprSharp.Runtime
{
    public class EEContext : EvalContext
    {
        protected EEContext() { }

        public override EvalContext GetChild(VariableFindMode mode = VariableFindMode.UpAll)
        {
            return new EEContext() { Evaluator = Evaluator, CancelToken = CancelToken, Parent = this };
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
                    catch
                    {
                        return (T)(object)r;
                    }
                case string s:
                    return (T)(object)s;
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
