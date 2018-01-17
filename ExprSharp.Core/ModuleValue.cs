using iExpr;
using iExpr.Evaluators;
using iExpr.Values;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExprSharp
{
    public class ModuleValue : ClassValue
    {
        const string _AUTHOR = "author", _VERSION = "version",_NAME="name";

        public string Author { get; protected set; }

        public string Version { get; protected set; }

        public string Name { get; protected set; }

        ConcreteValue author, version,name;

        public override IExpr Access(string id)
        {
            switch (id)
            {
                case _AUTHOR:
                    return author;
                case _VERSION:
                    return version;
                case _NAME:
                    return name;
            }
            return base.Access(id);
        }

        public ModuleValue(string name,string author,string version,EvalContext context=null):base(context)
        {
            Author = author;
            Name = name;
            Version = version;
            this.author = new ReadOnlyConcreteValue(author);
            this.version = new ReadOnlyConcreteValue(version);
            this.name = new ReadOnlyConcreteValue(name);
        }

        public override string ToString()
        {
            return $"<module {Name} {Version} from {Author}>";
        }
    }
}
