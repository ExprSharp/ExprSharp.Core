using iExpr;
using iExpr.Evaluators;
using iExpr.Exceptions;
using iExpr.Values;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExprSharp
{
    public class ClassValue : Dictionary<string, CollectionItemValue>,IAccessibleValue
    {
        public bool IsConstant => true;

        public virtual IExpr Access(string id)
        {
            if (this.ContainsKey(id)) return this[id];
            else
            {
                var r = new CollectionItemValue(BuiltinValues.Null);
                this.Add(id, r);
                return r;
            }
        }

        public bool Equals(IExpr other)
        {
            return false;
        }

        public bool Equals(IValue other)
        {
            return false;
        }
    }
    
    public class PreClassValue : ClassValue
    {
        public string ClassName { get; protected set; }

        public bool CanChangeMember { get; protected set; }

        public override IExpr Access(string id)
        {
            if (this.ContainsKey(id)) return this[id];
            else if(CanChangeMember)
            {
                var r = new CollectionItemValue(BuiltinValues.Null);
                this.Add(id, r);
                return r;
            }
            else
            {
                throw new EvaluateException("can't access the id.");
            }
        }

        public PreClassValue(object obj, string classname=null, bool canchangeMember=false)
        {
            var type = obj.GetType();
            foreach(var v in type.GetMethods())
            {
                this.Add(v.Name, new CollectionItemValue(new PreFunctionValue(v.Name, (args, cal) =>
                 {
                     var r = v.Invoke(obj, args.Arguments);
                     if (r is IExpr) return (IExpr)r;
                     else return new ConcreteValue(r);
                 })));
            }
            foreach (var v in type.GetFields())
            {
                this.Add(v.Name, new CollectionItemValue(v.GetValue(obj)));
            }
            foreach (var v in type.GetProperties())
            {
                this.Add(v.Name, new CollectionItemValue(v.GetValue(obj)));
            }
            ClassName = classname??type.Name;
            CanChangeMember = canchangeMember;
        }

        public PreClassValue(Type type, string classname=null,bool canchangeMember = false)
        {
            foreach (var v in type.GetMethods())
            {
                this.Add(v.Name, new CollectionItemValue(new PreFunctionValue(v.Name, (args, cal) =>
                {
                    var r = v.Invoke(null, new object[] { args, cal });
                    if (r is IExpr) return (IExpr)r;
                    else return new ConcreteValue(r);
                })));
            }
            foreach (var v in type.GetFields())
            {
                this.Add(v.Name, new CollectionItemValue(v.GetValue(null)));
            }
            foreach (var v in type.GetProperties())
            {
                this.Add(v.Name, new CollectionItemValue(v.GetValue(null)));
            }
            ClassName = classname ?? type.Name;
            CanChangeMember = canchangeMember;
        }
    }
}
