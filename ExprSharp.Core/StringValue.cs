using ExprSharp.Runtime;
using iExpr.Exprs.Core;
using iExpr.Helpers;
using iExpr.Values;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ExprSharp.Core
{
    public struct StringValue : IAdditive, IMultiplicable,IEquatable<StringValue>,IEquatable<string>,IComparable<StringValue>,IComparable<string>,IComparable
    {
        public static implicit operator StringValue(string value)
        {
            return new StringValue(value);
        }
        public static implicit operator string(StringValue value)
        {
            return value.Value;
        }

        public static bool operator ==(StringValue value1, StringValue value2)
        {
            return EqualityComparer<StringValue>.Default.Equals(value1, value2);
        }

        public static bool operator !=(StringValue value1, StringValue value2)
        {
            return !(value1 == value2);
        }

        public StringValue(string val)
        {
            Value = val;
        }

        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }

        object IMultiplicable.Multiply(object right)
        {
            var r = EEContext.ConvertVal<int>(right);
            var sb = new StringBuilder();
            for(int i = 1; i <= r; i++)
            {
                sb.Append(this);
            }
            return new StringValue(sb.ToString());
        }

        object IAdditive.Add(object right)
        {
            var r = (StringValue)right;
            return new StringValue(string.Concat(Value, r.Value));
        }

        public bool Equals(string other)
        {
            return Value.Equals(other);
        }

        public bool Equals(StringValue other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is string) return Equals((string)obj);
            else if (obj is StringValue) return Equals((StringValue)obj);
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            var hashCode = -783812246;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Value);
            return hashCode;
        }

        public int CompareTo(string other)
        {
            return Value.CompareTo(other);
        }

        public int CompareTo(StringValue other)
        {
            return Value.CompareTo(other.Value);
        }

        public int CompareTo(object obj)
        {
            if (!(obj is StringValue)) throw new ArgumentException("Not a string value", nameof(obj));
            return CompareTo((StringValue)obj);
        }
    }
}
