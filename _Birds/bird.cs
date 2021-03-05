using System;

namespace _Birds
{
    public interface IFloating
    {
        bool CanFly();
    }
    public interface IFlying
    {
        bool CanFloat();
    }
    abstract class Bird
    {
        protected Bird (string name, double weight, Sex sex, double age)
        {
            Name = name;
            Weight = weight;
            Gender = sex;
            Age = age;
            Check();
        }

        private void Check()
        {
            if (Weight <= 0)
            {
                throw new Exception("Weight is way too small!");
            }

            if (Name == null)
            {
                throw new Exception("Give it a name!");
            }
        }

        public bool CanFly()
        {
            return this is IFlying;
        }

        public bool CanFloat()
        {
            return this is IFloating;
        }
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Age { get; set; }
        public enum Sex
        {
            Male,
            Female
        }
        public Sex Gender { get; }
        
    }
    class Kiwi : Bird
    {
        public Kiwi(string name, double weight, Sex sex, double age) : base (name, weight, sex, age)
        { }
    }
    class Duck : Bird, IFloating, IFlying
    {
        public Duck(string name, double weight, Sex sex, double age) : base(name, weight, sex, age)
        { }
    }
    class Penguin : Bird, IFloating
    {
        public Penguin(string name, double weight, Sex sex, double age) : base(name, weight, sex, age)
        { }
        
    }
    class Parrot : Bird, IFlying
    {
        public Parrot(string name, double weight, Sex sex, double age) : base(name, weight, sex, age)
        { }
        
    }
}