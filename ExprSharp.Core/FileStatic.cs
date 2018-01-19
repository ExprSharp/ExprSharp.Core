using ExprSharp.Core;
using iExpr.Evaluators;
using iExpr.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExprSharp
{
    [CanClassValue(Name = "file")]
    public class FileStatic
    {
        [ClassMethod(Name = "exists", ArgumentCount = 1, IsReadOnly = true)]
        public static bool Exists(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(null, 1, args);
            OperationHelper.AssertCertainValueThrowIf(null, args);
            var ov = cal.GetValue<StringValue>(args)[0];
            return File.Exists(ov);
        }

        [ClassMethod(Name = "openEasyRead", ArgumentCount = 1, IsReadOnly = true)]
        public static FileEasyReader OpenEasyRead(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(null, 1, args);
            OperationHelper.AssertCertainValueThrowIf(null, args);
            var ov = cal.GetValue<StringValue>(args)[0];
            return new FileEasyReader(ov);
        }

        [ClassMethod(Name = "openEasyWrite", ArgumentCount = 1, IsReadOnly = true)]
        public static FileEasyWriter OpenEasyWrite(FunctionArgument _args, EvalContext cal)
        {
            var args = _args.Arguments;
            OperationHelper.AssertArgsNumberThrowIf(null, 1, args);
            OperationHelper.AssertCertainValueThrowIf(null, args);
            var ov = cal.GetValue<StringValue>(args)[0];
            return new FileEasyWriter(ov);
        }
    }
}
