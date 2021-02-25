using System;

namespace Fraction
{
    class Program
    {
        static void Main(string[] args)
        {
            Fraction first = new Fraction(5, 8);
            Fraction sec = new Fraction(58, 73);
            first.Print();
            Console.WriteLine(first.Convert());
            Fraction sum = first + sec;
            sum.Print();
            Fraction comp = sec * first;
            comp.Print();
            Fraction div = first / sec;
            div.Print();
            if (sec > 1)
            {
                Console.WriteLine("sec is bigger than first");
            }
            if (sec < 2)
            {
                Console.WriteLine("sec is smaller than first");
            }
            Fraction opposite = -first;
            opposite.Print();
            Fraction check = new Fraction ();
            check.Print();
            Fraction sum1 = first + 2 + (7 + sec);
            sum1.Print();
            Fraction sub = first - 2;
            sub.Print();
            Fraction div1 = first / 5 * (7 / sec);
            div1.Print();
            Fraction sub1 = 5 - first + (sec - 7) + 15;
            sub1.Print();
        }
    }
}
