using iExpr.Evaluators;
using iExpr.Helpers;
using iExpr.Parsers;
using iExpr.Values;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExprSharp.Runtime
{
    public class ESRuntime
    {
        EParse ep = new EParse();
        EEval ev = new EEval();
        ExprBuilder eb = null;
        EvalContext context = null;

        public ESRuntime()
        {
            eb = new ExprBuilder(ep);
            context = ev.CreateContext();
        }

        public object Execute(string code)
        {
            var e = eb.GetExpr(code);
            return OperationHelper.GetValue(context.Evaluate(e));
        }

        public EvalEnvironment EvalEnvironment { get => ev; }

        public ParseEnvironment ParseEnvironment { get => ep; }

        public object Execute(string code, Dictionary<string, object> vars)
        {
            var e = eb.GetExpr(code);
            var c = context.GetChild();
            foreach (var v in vars)
            {
                c.Variables.Add(v.Key, new ConcreteValue(v.Value));
            }
            return OperationHelper.GetValue(c.Evaluate(e));
        }
    }
}
