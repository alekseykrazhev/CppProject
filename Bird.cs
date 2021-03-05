using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace birds
{
    public interface IFloating
    {
        bool Floating()
        {
            return true;
        }
    }
    public interface IFlying
    {
        bool Flyable()
        {
            return true;
        }
    }
    abstract class Bird : IFlying, IFloating
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
        public abstract bool CanFly ();
        public abstract bool CanFloat ();
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
        public override bool CanFly()
        {
            return false;
        }
        public override bool CanFloat()
        {
            return false;
        }
    }
    class Duck : Bird
    {
        public Duck(string name, double weight, Sex sex, double age) : base(name, weight, sex, age)
        { }
        public override bool CanFloat()
        {
            return this is IFloating;
        }
        public override bool CanFly()
        {
            return this is IFlying;
        }
    }
    class Penguin : Bird
    {
        public Penguin(string name, double weight, Sex sex, double age) : base(name, weight, sex, age)
        { }
        public override bool CanFloat ()
        {
            return this is IFloating;
        }
        public override bool CanFly()
        {
            return false;
        }
    }
    class Parrot : Bird
    {
        public Parrot(string name, double weight, Sex sex, double age) : base(name, weight, sex, age)
        { }
        public override bool CanFly ()
        {
            return this is IFlying;
        }
        public override bool CanFloat()
        {
            return false;
        }
    }
}
