using iExpr;
using iExpr.Evaluators;
using iExpr.Exceptions;
using iExpr.Exprs.Program;
using iExpr.Extensions.Math.Numerics;
using iExpr.Helpers;
using iExpr.Parsers;
using iExpr.Values;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ExprSharp.Runtime
{
    internal class ModuleBuildFunctionValue : PreFunctionValue
    {
        public ModuleValue Module { get; private set; }

        static void Execute(ModuleBuildFunctionValue main, FunctionArgument args, EvalContext cal)
        {
            var children = args.Contents;
            var c = main.Module.Context;
            foreach (var x in children)
            {
                c.Evaluate(x);
            }
            /*foreach (var v in c.Variables)
            {
                main.Module.Add(v.Key, v.Value is CollectionItemValue ? (CollectionItemValue)v.Value : new CollectionItemValue(v.Value));
            }*/
        }

        public ModuleBuildFunctionValue(ModuleValue module) : base()
        {
            this.Module = module;
            Keyword = $"module({module.Name},{module.Author},{Module.Version})";
            EvaluateFunc = (x, y) => { Execute(this, x, y); return this.Module; };
        }
    }

    public class Operations
    {
        public static PreFunctionValue Print { get; } = new PreFunctionValue(
                    "print",
                    (FunctionArgument _args, EvalContext cal) =>
                    {
                        var args = _args.Arguments;
                        foreach(var v in args)
                        {
                            if(v!=null)Console.Write(v.ToString().Trim('"') + " ");
                            else Console.Write("null ");
                        }
                        Console.WriteLine();
                        return BuiltinValues.Null;
                    },
                    -1
                    );

        public static PreFunctionValue Input { get; } = new PreFunctionValue(
                    "input",
                    (FunctionArgument _args, EvalContext cal) =>
                    {
                        var args = _args.Arguments;
                        if (args.Length == 0)
                        {
                            
                        }
                        else
                        {
                            Console.Write(args[0]?.ToString());
                        }
                        return new ConcreteValue(Console.ReadLine());
                    },
                    1
                    );

        public static PreFunctionValue Number { get; } = new PreFunctionValue(
                    "num",
                    (FunctionArgument _args, EvalContext cal) =>
                    {
                        var args = _args.Arguments;
                        OperationHelper.AssertArgsNumberThrowIf(1, args);
                        OperationHelper.AssertCertainValueThrowIf(args);
                        var s = cal.GetValue<string>(args[0]);
                        return new ConcreteValue(new RealNumber(BigDecimal.Parse(s)));
                    },
                    1
                    );

        public static PreFunctionValue String { get; } = new PreFunctionValue(
                    "str",
                    (FunctionArgument _args, EvalContext cal) =>
                    {
                        var args = _args.Arguments;
                        OperationHelper.AssertArgsNumberThrowIf(1, args);
                        OperationHelper.AssertCertainValueThrowIf(args);
                        var s = cal.GetValue<object>(args[0]);
                        return new ConcreteValue(s.ToString());
                    },
                    1
                    );

        public static PreFunctionValue Integer { get; } = new PreFunctionValue(
                    "int",
                    (FunctionArgument _args, EvalContext cal) =>
                    {
                        var args = _args.Arguments;
                        OperationHelper.AssertArgsNumberThrowIf(1, args);
                        OperationHelper.AssertCertainValueThrowIf(args);
                        var s = cal.GetValue<RealNumber>(args[0]);
                        return new ConcreteValue(new RealNumber(new BigDecimal(s.Value.Integer)));
                    },
                    1
                    );

        public static PreFunctionValue Decimal { get; } = new PreFunctionValue(
                    "decimal",
                    (FunctionArgument _args, EvalContext cal) =>
                    {
                        var args = _args.Arguments;
                        OperationHelper.AssertArgsNumberThrowIf(1, args);
                        OperationHelper.AssertCertainValueThrowIf(args);
                        int p = cal.GetValue<int>(args[0]);
                        return new ConcreteValue(new RealNumber(new BigDecimal(0, p)));
                    },
                    1
                    );

        /// <summary>
        /// 排序
        /// </summary>
        public static PreFunctionValue Sorted { get; } = new PreFunctionValue(
            "sorted",
            (FunctionArgument _args, EvalContext cal) =>
            {
                var args = _args.Arguments;
                if (OperationHelper.AssertArgsNumber(1, args))
                {
                    var v = cal.GetValue<CollectionValue>(args[0]) as IEnumerable<CollectionItemValue>;
                    return new ListValue(v.OrderBy(x => x.Value));
                }
                else if (OperationHelper.AssertArgsNumber(2, args))
                {
                    var v = cal.GetValue<CollectionValue>(args[0]) as IEnumerable<CollectionItemValue>;
                    var func = cal.GetValue<FunctionValue>(args[1]);
                    var c = cal.GetChild();
                    return new ListValue(v.OrderBy(x => func.EvaluateFunc(new FunctionArgument(x),c)));
                }
                else
                {
                    throw new EvaluateException("wrong argument count");
                }
            },
            2
            );

        public static PreFunctionValue Select { get; } = new PreFunctionValue(
            "select",
            (FunctionArgument _args, EvalContext cal) =>
            {
                var args = _args.Arguments;
                OperationHelper.AssertArgsNumberThrowIf(2, args);
                var v = cal.GetValue<CollectionValue>(args[0]) as IEnumerable<CollectionItemValue>;
                var func = cal.GetValue<FunctionValue>(args[1]);
                var c = cal.GetChild();
                return new PreEnumeratorValue(v.Select(x => (IValue)func.EvaluateFunc(new FunctionArgument(x), c)));
            },
            2
            );

        public static PreFunctionValue Where { get; } = new PreFunctionValue(
            "where",
            (FunctionArgument _args, EvalContext cal) =>
            {
                var args = _args.Arguments;
                OperationHelper.AssertArgsNumberThrowIf(2, args);
                var v = cal.GetValue<CollectionValue>(args[0]) as IEnumerable<CollectionItemValue>;
                var func = cal.GetValue<FunctionValue>(args[1]);
                var c = cal.GetChild();
                return new PreEnumeratorValue(v.Where(x => cal.GetValue<bool>(func.EvaluateFunc(new FunctionArgument(x), c))));
            },
            2
            );

        public static PreFunctionValue Zip { get; } = new PreFunctionValue(
            "zip",
            (FunctionArgument _args, EvalContext cal) =>
            {
                var args = _args.Arguments;
                OperationHelper.AssertArgsNumberThrowIf(3, args);
                var v1 = cal.GetValue<CollectionValue>(args[0]) as IEnumerable<CollectionItemValue>;
                var v2 = cal.GetValue<CollectionValue>(args[1]) as IEnumerable<CollectionItemValue>;
                var func = cal.GetValue<FunctionValue>(args[2]);
                var c = cal.GetChild();
                return new PreEnumeratorValue(v1.Zip(v2, (x, y) => (IValue)func.EvaluateFunc(new FunctionArgument(x, y), c)));
            },
            3
            );

        public static PreFunctionValue Reduce { get; } = new PreFunctionValue(
            "reduce",
            (FunctionArgument _args, EvalContext cal) =>
            {
                var args = _args.Arguments;
                OperationHelper.AssertArgsNumberThrowIf(2, args);
                var v = (cal.GetValue<CollectionValue>(args[0]) as IEnumerable<CollectionItemValue>).ToArray();
                var func = cal.GetValue<FunctionValue>(args[1]);
                var c = cal.GetChild();
                if (v.Length == 0) return func.EvaluateFunc(new FunctionArgument(), c);
                IExpr last = v[0];
                for(int i = 1; i < v.Length; i++)
                {
                    last = func.EvaluateFunc(new FunctionArgument(last, v[i]), c);
                }
                return last;
            },
            2
            );

        public static PreFunctionValue Range { get; } = new PreFunctionValue(
            "range",
            (FunctionArgument _args, EvalContext cal) =>
            {
                var args = _args.Arguments;
                OperationHelper.AssertArgsNumberThrowIf(2, args);
                int[] l = cal.GetValue<int>(args);
                return new PreEnumeratorValue(Enumerable.Range(l[0], l[1] - l[0] + 1).Select(x => new ConcreteValue( new RealNumber(new BigDecimal(x)))));
            },
            2
            );

        public static PreFunctionValue Import { get; } = new PreFunctionValue(
            "import",
            (FunctionArgument _args, EvalContext cal) =>
            {
                var args = _args.Arguments;
                OperationHelper.AssertArgsNumberThrowIf(1, args);
                var vs = cal.GetValue<string>(args[0]);
                if (File.Exists(vs))
                {
                    string mod = File.ReadAllText(vs);
                    try
                    {
                        var c = cal.GetChild();
                        var builder = new ExprBuilder(new EParse());
                        var m = cal.GetValue<ModuleValue>(c.Evaluate(builder.GetExpr(mod)));
                        var baseContext = cal;
                        while (baseContext.Parent != null) baseContext = baseContext.Parent;
                        baseContext.Variables.Set(m.Name, m);
                        return new ConcreteValue(true);
                    }
                    catch
                    {
                        return new ConcreteValue(false);
                    }
                }
                return new ConcreteValue(false);
            },
            1
            );

        public static PreFunctionValue Module { get; } = new PreFunctionValue(
            "module",
            (FunctionArgument _args, EvalContext cal) =>
            {
                var args = _args.Arguments;
                OperationHelper.AssertArgsNumberThrowIf(3, args);
                //if(args.Length==0) throw new EvaluateException("wrong argument count");
                var vs = cal.GetValue<string>(args);
                string name = "", author = "", version = "";
                name = vs[0];
                author = vs[1];
                version = vs[2];
                return new ModuleBuildFunctionValue(new ModuleValue(name, author, version, cal));
            },
            3
            );

        public static PreFunctionValue View { get; } = new PreFunctionValue(
            "view",
            (FunctionArgument _args, EvalContext cal) =>
            {
                var args = _args.Arguments;
                OperationHelper.AssertArgsNumberThrowIf(1, args);
                //if(args.Length==0) throw new EvaluateException("wrong argument count");
                var vs = cal.GetValue<ClassValue>(args[0]);
                var res = new DictionaryValue();
                foreach(var v in vs)
                {
                    res.Add(new ConcreteValue(v.Key), v.Value);
                }
                return res;
            },
            1
            );
    }
}
