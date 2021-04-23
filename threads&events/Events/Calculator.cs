using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Events
{
    public delegate void CalculatorEventHandler(object sender, CalculateEventArgs e);
    
    public class Calculator
    {
        public event CalculatorEventHandler CalcEv;
        private int _delay;
        private bool _calculate = true;
        public static bool working;
        private int amount = 0;

        public Calculator(int delay)
        {
            _delay = delay;
        }

        public void Calculate()
        {
            working = true;
            while (true)
            {
                if (!_calculate)
                {
                    working = false;
                    return;
                }

                amount = 0;
                Random ran = new Random();
                int A = ran.Next(2, 1000);
                int B = ran.Next(2, 1000);
                if (A > B)
                {
                    int t = A;
                    A = B;
                    B = t;
                }

                for (int i = A; i < B; ++i)
                {
                    if (IsArmstrong(i))
                    {
                        ++amount;
                    }
                }
                CalcEv?.Invoke(this, new CalculateEventArgs(amount, A, B));
                Thread.Sleep(_delay);
            }
        }

        bool IsArmstrong(int number)
        {
            int n = number;
            var list = new List<int>(15);
            while (n > 0) 
            {
                list.Add(n % 10);
                n /= 10;
            }
 
            var digits = list.ToArray();
            var poweredDigits = (int[])digits.Clone();
 
            int lastsum = int.MinValue;
            while (true)
            {
                int sum = poweredDigits.Sum(); 
                if (sum > number || lastsum == sum)
                    return false;
                if (sum == number)
                    return true;
                for (int i = 0; i < poweredDigits.Length; i++) 
                {
                    poweredDigits[i] *= digits[i];
                }
                lastsum = sum;
            }
        }

        public void Stop(object sender, EventArgs e)
        {
            _calculate = false;
        }
    }

    public class CalculateEventArgs : EventArgs
    {
        private int _result;
        public int Result => _result;
        
        private int A;
        public int Begin => A;
        
        private int B;
        public int End => B;

        public CalculateEventArgs(int res, int a, int b)
        {
            _result = res;
            A = a;
            B = b;
        }
    }
}