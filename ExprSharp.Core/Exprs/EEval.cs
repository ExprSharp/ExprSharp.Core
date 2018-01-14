using System;
using System.Collections.Generic;
using System.Text;
using iExpr.Evaluators;

namespace ExprSharp.Core.Exprs
{
    public class EEval : EvalEnvironment
    {
        public EEval()
        {
            base.Evaluator = new iExpr.Evaluators.ExprEvaluator();
        }
    }
}
