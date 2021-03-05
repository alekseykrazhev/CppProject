using System;
using System.Collections.Generic;
using System.Text;

namespace Fraction
{
    class Fraction
    {
        static long GCD(long num1, long num2)
        {
            long Remainder;

            while (num2 != 0)
            {
                Remainder = num1 % num2;
                num1 = num2;
                num2 = Remainder;
            }

            return num1;
        }

        private long numerator, denominator = 1;
        private void Simplify()
        {
            if (denominator < 0)
            {
                numerator *= -1;
                denominator *= -1;
            }
            if (Math.Abs(numerator) < 2)
            {
                return;
            }
            long nod = GCD(numerator, denominator);
            numerator /= nod;
            denominator /= nod;
            if (denominator < 0)
            {
                denominator *= -1;
                numerator *= -1;
            }
        }
        private void Check()
        {
            if (numerator == 0)
            {
                denominator = 1;
            }
            if (denominator == 0)
            {
                throw new ArithmeticException("Division by zero. Epic fail!");
            }

        }

        public Fraction()
        {
            SetNum(1);
            SetDen(1);
        }
        public Fraction(long num, long den = 1)
        {
            SetNum(num);
            SetDen(den);
            Check();
            Simplify();
        }
        public void SetNum(long num)
        {
            numerator = num;
            Check();
            Simplify();
        }
        public void SetDen(long den)
        {
            denominator = den;
            Check();
            Simplify();
        }
        public long GetNum()
        {
            return numerator;
        }
        public long GetDen()
        {
            return denominator;
        }

        public double Convert()
        {
            return (double)numerator / denominator;
        }

        public void Print()
        {
            Console.Write(numerator);
            if (denominator == 1)
            {
                Console.WriteLine();
                return;
            }
            Console.Write("/");
            Console.WriteLine(denominator);
        }
        public static Fraction operator +(Fraction first, Fraction second)
        {
            Fraction sum = new Fraction(first.GetNum() * second.GetDen() + first.GetDen() * second.GetNum(), first.GetDen() * second.GetDen());
            sum.Simplify();
            return sum;
        }
        public static Fraction operator +(Fraction first, int second)
        {
            Fraction sum = new Fraction(second);
            return first + sum;
        }
        public static Fraction operator +(int first, Fraction second)
        {
            return second + first;
        }
        public static Fraction operator -(Fraction first, Fraction second)
        {
            Fraction div = new Fraction(first.GetNum() * second.GetDen() - first.GetDen() * second.GetNum(), first.GetDen() * second.GetDen());
            div.Simplify();
            return div;
        }
        public static Fraction operator - (Fraction first, int second)
        {
            Fraction sub = new Fraction(second);
            return first - sub;
        }
        public static Fraction operator -(int first, Fraction second)
        {
            Fraction sub = new Fraction(first);
            return sub - second;
        }

        public static Fraction operator -(Fraction first)
        {
            Fraction opposite = new Fraction(-first.GetNum(), first.GetDen());
            return opposite;
        }

        public static Fraction operator *(Fraction first, Fraction second)
        {
            Fraction comp = new Fraction(first.GetNum() * second.GetNum(), first.GetDen() * second.GetDen());
            comp.Simplify();
            return comp;
        }
        public static Fraction operator *(Fraction first, int second)
        {
            Fraction comp = new Fraction(second);
            return first * comp;
        }
        public static Fraction operator *(int first, Fraction second)
        { 
            return second * first;
        }
        public static Fraction operator /(Fraction first, Fraction second)
        {
            Fraction div = new Fraction(first.GetNum() * second.GetDen(), first.GetDen() * second.GetNum());
            return div;
        }
        public static Fraction operator /(Fraction first, int second)
        {
            Fraction div = new Fraction(second);
            return first / div;
        }
        public static Fraction operator /(int first, Fraction second)
        {
            Fraction div = new Fraction(first);
            return div / second;
        }
        public static bool operator >(Fraction first, Fraction second)
        {
            return (first.GetNum() * second.GetDen() > first.GetDen() * second.GetNum());
        }
        public static bool operator >(Fraction first, int second)
        {
            return (first.GetNum() > second * first.GetDen());
        }
        public static bool operator <(Fraction first, Fraction second)
        {
            return !(first > second && first != second);
        }
        public static bool operator <(Fraction first, int second)
        {
            return !(first > second && first != second);
        }
        public static bool operator ==(Fraction first, Fraction second)
        {
            return (first.GetNum() * second.GetDen() == second.GetNum() * first.GetDen());
        }
        public static bool operator ==(Fraction first, int second)
        {
            return (first.GetNum() == second * first.GetDen());
        }
        public static bool operator !=(Fraction first, Fraction second)
        {
            return !(first == second);
        }
        public static bool operator !=(Fraction first, int second)
        {
            return !(first == second);
        }
        public static bool operator >=(Fraction first, Fraction second)
        {
            return !(first < second);
        }
        public static bool operator <=(Fraction first, Fraction second)
        {
            return !(first > second);
        }

    }
}
