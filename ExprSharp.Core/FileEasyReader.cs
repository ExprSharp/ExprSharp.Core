using ExprSharp.Core;
using iExpr;
using iExpr.Evaluators;
using iExpr.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExprSharp
{
    public class FileEasyReader : IAccessibleValue
    {
        protected iExpr.Helpers.ExtendAccessibleValueHelper access = null;

        string Path;
        StreamReader sr = null;

        public bool IsCertain => true;

        void init()
        {
            access = new iExpr.Helpers.ExtendAccessibleValueHelper(false, this);
            access.Add(ClassValueBuilder.BuildFunction(this.Next, "next", 0));
            access.Add(ClassValueBuilder.BuildFunction(this.Close, "close", 0));
        }

        public FileEasyReader(string path)
        {
            init();
            sr = new StreamReader(path, Encoding.UTF8);
            Path = path;
        }

        public object Close(FunctionArgument _args, EvalContext cal)
        {
            sr?.Close();
            return null;
        }

        public object Next(FunctionArgument _args, EvalContext cal)
        {
            StringBuilder sb = new StringBuilder();
            char? c = null;
            while (!sr.EndOfStream)
            {
                c = (char)sr.Read();
                if (!char.IsWhiteSpace(c.Value)) break;
            }
            if (c.HasValue == false) return (StringValue)(sb.ToString());
            sb.Append(c.Value);
            while (!sr.EndOfStream)
            {
                c = (char)sr.Read();
                if (char.IsWhiteSpace(c.Value)) break;
                sb.Append(c);
            }
            return (StringValue)sb.ToString();
        }

        public IExpr Access(string id)
        {
            return ((IAccessibleValue)access).Access(id);
        }

        public IDictionary<string, IExpr> GetMembers()
        {
            return ((IAccessibleValue)access).GetMembers();
        }

        public bool Equals(IExpr other)
        {
            return ((object)this).Equals(other);
        }

        public override string ToString()
        {
            return $"<file reader linked to {Path}>";
        }
    }

    public class FileEasyWriter : IAccessibleValue
    {
        protected iExpr.Helpers.ExtendAccessibleValueHelper access = null;

        string Path;

        StreamWriter sr = null;

        public bool IsCertain => true;

        void init()
        {
            access = new iExpr.Helpers.ExtendAccessibleValueHelper(false, this);
            access.Add(ClassValueBuilder.BuildFunction(this.Write, "write", -1));
            access.Add(ClassValueBuilder.BuildFunction(this.WriteLine, "writeln", -1));
            access.Add(ClassValueBuilder.BuildFunction(this.Close, "close", 0));
        }

        public FileEasyWriter(string path)
        {
            init();
            Path = path;
            sr = new StreamWriter(path, false, Encoding.UTF8);
        }

        public object Close(FunctionArgument _args, EvalContext cal)
        {
            sr?.Close();
            return null;
        }

        public object Write(FunctionArgument _args, EvalContext cal)
        {
            foreach (var v in _args.Arguments)
            {
                if (v != null) sr.Write(v.ToString()+" ");
            }
            return null;
        }

        public object WriteLine(FunctionArgument _args, EvalContext cal)
        {
            foreach (var v in _args.Arguments)
            {
                if (v != null) sr.Write(v.ToString() + " ");
            }
            sr.WriteLine();
            return null;
        }

        public IExpr Access(string id)
        {
            return ((IAccessibleValue)access).Access(id);
        }

        public IDictionary<string, IExpr> GetMembers()
        {
            return ((IAccessibleValue)access).GetMembers();
        }

        public bool Equals(IExpr other)
        {
            return ((object)this).Equals(other);
        }

        public override string ToString()
        {
            return $"<file writer linked to {Path}>";
        }
    }
}
