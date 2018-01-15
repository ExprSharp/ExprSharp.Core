using System;
using System.Collections.Generic;
using System.Text;
using iExpr;

namespace ExprSharp
{
    public struct RealNumber : IAdditive, ISubtractive, IMultiplicable, IDivisible, IMouldable, IPowerable, IEquatable<RealNumber>,IComparable<RealNumber>,IComparable
    {
        double Value;

        public static implicit operator RealNumber(double value)
        {
            return new RealNumber(value);
        }
        public static implicit operator double(RealNumber value)
        {
            return value.Value;
        }

        public RealNumber(double value)
        {
            Value = value;
        }

        public object Add(object right)
        {
            if (!(right is RealNumber)) throw new ArgumentException();
            var v = (RealNumber)right;
            return new RealNumber(Value + v.Value);
        }

        public object Divide(object right)
        {
            if (!(right is RealNumber)) throw new ArgumentException();
            var v = (RealNumber)right;
            return new RealNumber(Value/v.Value);
        }

        public object Mod(object right)
        {
            if (!(right is RealNumber)) throw new ArgumentException();
            var v = (RealNumber)right;
            return new RealNumber(Value % v.Value);
        }

        public object Multiply(object right)
        {
            if (!(right is RealNumber)) throw new ArgumentException();
            var v = (RealNumber)right;
            return new RealNumber(Value*v.Value);
        }

        public object Pow(object right)
        {
            if (!(right is RealNumber)) throw new ArgumentException();
            var v = (RealNumber)right;
            return new RealNumber(System.Math.Pow(Value,v.Value));
        }

        public object Subtract(object right)
        {
            if (!(right is RealNumber)) throw new ArgumentException();
            var v = (RealNumber)right;
            return new RealNumber(Value-v.Value);
        }

        public static bool operator ==(RealNumber number1, RealNumber number2)
        {
            return number1.Equals(number2);
        }

        public static bool operator !=(RealNumber number1, RealNumber number2)
        {
            return !(number1 == number2);
        }

        public static RealNumber operator +(RealNumber left, RealNumber right)
        {
            return (RealNumber)left.Add(right);
        }

        public static RealNumber operator -(RealNumber left, RealNumber right)
        {
            return (RealNumber)left.Subtract(right);
        }

        public static RealNumber operator *(RealNumber left, RealNumber right)
        {
            return (RealNumber)left.Multiply(right);
        }

        public static RealNumber operator /(RealNumber dividend, RealNumber divisor)
        {
            return (RealNumber)dividend.Divide(divisor);
        }

        public static RealNumber operator %(RealNumber dividend, RealNumber divisor)
        {
            return (RealNumber)dividend.Mod(divisor);
        }

        public static bool operator <(RealNumber left, RealNumber right)
        {
            return left.CompareTo(right) < 0;
        }
        public static bool operator <=(RealNumber left, RealNumber right)
        {
            return left.CompareTo(right) <= 0;
        }
        public static bool operator >(RealNumber left, RealNumber right)
        {
            return left.CompareTo(right) > 0;
        }
        public static bool operator >=(RealNumber left, RealNumber right)
        {
            return left.CompareTo(right) >= 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is double) return Value == ((double)obj);
            else if (obj is decimal) return (decimal)Value == ((decimal)obj);
            return obj is RealNumber && Equals((RealNumber)obj);
        }

        public bool Equals(RealNumber other)
        {
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            var hashCode = -783812246;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            return hashCode;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is RealNumber)) throw new ArgumentException();
            return CompareTo((RealNumber)obj);
        }

        public int CompareTo(RealNumber other)
        {
            return Value.CompareTo(other.Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
