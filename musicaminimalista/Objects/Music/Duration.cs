using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml;

namespace MusicaMinimalista.Objects.Music
{
    [DataContract]
    public class Duration : IComparable
    {
        [DataMember(Name = "Numerator")]
        private int num;
        [DataMember(Name = "Denominator")]
        private int den;

        public Duration(int x, int y)
        {
            this.num = x;
            this.den = y;
            this.reduce();
        }

        public Duration(int x)
        {
            this.num = x;
            this.den = 1;
        }

        private static int gcd(int a, int b)
        {
            return b == 0 ? a : gcd(b, a % b);
        }

        private void reduce()
        {
            if (this.den == 0) throw new DivideByZeroException();
            int aux = gcd(this.num, this.den);
            this.num /= aux;
            this.den /= aux;
        }

        /** IMPLICIT CONVERSION INTEGER -> DURATION **/

        public static implicit operator Duration(int a)
        {
            return new Duration(a, 1);
        }

        /** OPERATORS **/

        public static Duration operator +(Duration d1, Duration d2)
        {
            return new Duration(
                d1.num * d2.den + d2.num * d1.den,
                d1.den * d2.den
            );
        }
        public static Duration operator -(Duration d1, Duration d2)
        {
            return new Duration(
                d1.num * d2.den - d2.num * d1.den,
                d1.den * d2.den
            );
        }
        public static Duration operator *(Duration d1, Duration d2)
        {
            return new Duration(
                d1.num * d2.num,
                d1.den * d2.den
            );
        }
        public static Duration operator /(Duration d1, Duration d2)
        {
            return new Duration(
                d1.num * d2.den,
                d1.den * d2.num
            );
        }
        
        public static bool operator ==(Duration d1, Duration d2)
        {
            if (object.ReferenceEquals(d1, null))
            {
                return object.ReferenceEquals(d2, null);
            }
            return d1.Equals(d2);
        }

        public static bool operator !=(Duration d1, Duration d2)
        {
            return !(d1 == d2);
        }

        public static bool operator >(Duration d1, Duration d2)
        {
            return (((double)d1.num / (double)d1.den) > ((double)d2.num / (double)d2.den));
        }
        public static bool operator <(Duration d1, Duration d2)
        {
            return (((double)d1.num / (double)d1.den) < ((double)d2.num / (double)d2.den));
        }
        public static bool operator >=(Duration d1, Duration d2)
        {
            return (((double)d1.num / (double)d1.den) >= ((double)d2.num / (double)d2.den));
        }
        public static bool operator <=(Duration d1, Duration d2)
        {
            return (((double)d1.num / (double)d1.den) <= ((double)d2.num / (double)d2.den));
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            Duration d = obj as Duration;
            if ((Object)d == null)
                return false;

            return (this.num == d.num && this.den == d.den);
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Duration d = obj as Duration;
            if (d != null)
            {
                if (this == d) return 0;
                if (this > d) return 1;
                else return -1;
            }
            else
                throw new ArgumentException("Object is not a Duration");
        }

        public static Duration Parse(string s)
        {
            //Remove all white spaces
            s = new string(s.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());

            if (!s.Contains('/')) return int.Parse(s);

            int barposition = s.IndexOf('/');

            string num_str = s.Substring(0, barposition);
            string den_str = s.Substring(barposition + 1);

            if (num_str == "") num_str = "1";
            if (den_str == "") den_str = "1";

            return new Duration(int.Parse(num_str), int.Parse(den_str));
        }

        public override string ToString()
        {
            if (this.den == 1) return String.Format("{0}", this.num);
            return String.Format("{0}/{1}", this.num, this.den);
        }

        public override int GetHashCode()
        {
            return (31 + num.GetHashCode()) * 31 + den.GetHashCode();
        }

        public int ToInt()
        {
            return this.num / this.den;
        }

        public double ToDouble()
        {
            return (double)this.num / (double)this.den;
        }

        public int getNum()
        {
            return this.num;
        }

        public int getDen()
        {
            return this.den;
        }

        public Duration BiggestPowerOf2()
        {
            for (int i = 10; i > -10; i--)
            {
                Duration duration;
                if (i > 0) duration = (int)Math.Pow(2, i);
                else duration = new Duration(1, (int)Math.Pow(2, -i));
                if (duration <= this) return duration;
            }
            return new Duration(1, (int)Math.Pow(2, 10));
        }

        public Duration Clone()
        {
            return new Duration(this.num, this.den);
        }
    }
}
