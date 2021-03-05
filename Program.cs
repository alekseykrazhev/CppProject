using System;

namespace birds
{
    class Program
    {
        static void Main(string[] args)
        {
            // enter name, weight, sex, age
            Bird check = new Kiwi("Kiwiiii", 0.25, Bird.Sex.Male, 1);
            Bird parrot = new Parrot("Artur", 0.12, Bird.Sex.Female, 0.7);
            Bird duck = new Duck("Donald", 0.45, Bird.Sex.Male, 2);
            if (!check.CanFly())
            {
                Console.WriteLine("Kiwi can't fly :(");
            }
            if (!check.CanFloat())
            {
                Console.WriteLine("Apparently, kiwi can't float");
            }

            check.Name = "Not Kiwiiii";
            Console.WriteLine(check.Name);
            Console.WriteLine(check.Gender);
            parrot.Weight = 0.22;
            Console.WriteLine(parrot.Weight);
            duck.Age = 3;
            Console.WriteLine(duck.Age);
        }
    }
}
