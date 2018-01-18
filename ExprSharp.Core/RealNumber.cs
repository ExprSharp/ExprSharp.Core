using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using iExpr;
using iExpr.Exprs.Core;
using iExpr.Extensions.Math.Numerics;

namespace ExprSharp
{
    public struct RealNumber : IAdditive, ISubtractive, IMultiplicable, IDivisible, IMouldable, IPowerable, IEquatable<RealNumber>,IComparable<RealNumber>,IComparable
    {
        public static BigDecimalFactory Factory = new BigDecimalFactory(100);

        public BigDecimal Value;

        public static implicit operator RealNumber(double value)
        {
            return new RealNumber(value);
        }
        public static implicit operator double(RealNumber value)
        {
            return (double)value.Value;
        }
        public static implicit operator RealNumber(bool value)
        {
            return new RealNumber(value?1:0);
        }
        public static implicit operator bool(RealNumber value)
        {
            return value.Value!=0;
        }
        public static implicit operator RealNumber(int value)
        {
            return new RealNumber(value);
        }
        public static implicit operator int(RealNumber value)
        {
            return (int)value.Value;
        }
        public static implicit operator RealNumber(long value)
        {
            return new RealNumber(value);
        }
        public static implicit operator long(RealNumber value)
        {
            return (int)value.Value;
        }

        public RealNumber(double value)
        {
            Value = Factory.Get(value);
        }

        public RealNumber(BigDecimal value)
        {
            if (value.Precision != Factory.Precision) value.SetPrecision(Factory.Precision);
            Value = value;
        }

        object IAdditive.Add(object right)
        {
            if (!(right is RealNumber)) throw new ArgumentException("Not a real number",nameof(right));
            var v = (RealNumber)right;
            return this + v;
        }

        object IDivisible.Divide(object right)
        {
            if (!(right is RealNumber)) throw new ArgumentException("Not a real number", nameof(right));
            var v = (RealNumber)right;
            return this / v;
        }

        object IMouldable.Mod(object right)
        {
            if (!(right is RealNumber)) throw new ArgumentException("Not a real number", nameof(right));
            var v = (RealNumber)right;
            return this % v;
        }

        object IMultiplicable.Multiply(object right)
        {
            if (!(right is RealNumber)) throw new ArgumentException("Not a real number", nameof(right));
            var v = (RealNumber)right;
            return this * v;
        }

        object IPowerable.Pow(object right)
        {
            if (!(right is RealNumber)) throw new ArgumentException("Not a real number", nameof(right));
            var v = (RealNumber)right;
            //return new RealNumber(System.Math.Pow(Value,v.Value));
            return new RealNumber(BigDecimal.Pow(Value,(BigInteger)v.Value));
        }

        object ISubtractive.Subtract(object right)
        {
            if (!(right is RealNumber)) throw new ArgumentException("Not a real number", nameof(right));
            var v = (RealNumber)right;
            return this - v;
        }

        object ISubtractive.Negtive()
        {
            return -this;
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
            return new RealNumber(left.Value + right.Value);
        }

        public static RealNumber operator -(RealNumber left, RealNumber right)
        {
            return new RealNumber(left.Value - right.Value);
        }

        public static RealNumber operator -(RealNumber left)
        {
            return new RealNumber(-left.Value);
        }

        public static RealNumber operator *(RealNumber left, RealNumber right)
        {
            return new RealNumber(left.Value * right.Value);
        }

        public static RealNumber operator /(RealNumber dividend, RealNumber divisor)
        {
            return new RealNumber(dividend.Value / divisor.Value);
        }

        public static RealNumber operator %(RealNumber dividend, RealNumber divisor)
        {
            return new RealNumber(dividend.Value % divisor.Value);
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
            return (obj is RealNumber && Equals((RealNumber)obj)) || this.Value.Equals(obj);
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
            if (!(obj is RealNumber)) throw new ArgumentException("Not a real number", nameof(obj));
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
