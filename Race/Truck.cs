using System;

namespace Race
{
    class Truck : Car
    {
        public Truck(string name, double initialSpeed, int num) : base(name, initialSpeed, num) { }

        public override void Description()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            base.Description();
            Console.ResetColor();
        }

        public override void RandomSpeed()
        {
            Speed = new Random().Next(30, 100);
        }

    }
}
