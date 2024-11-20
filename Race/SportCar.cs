using System;
using RaceGame;

namespace Race
{
    class SportCar : Car
    {
        public SportCar(string name, double initialSpeed, int num) : base(name, initialSpeed, num) { }

        public override void Description()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            base.Description();
            Console.ResetColor();
        }

        public override void RandomSpeed()
        {
            Speed = new Random().Next(50, 120);
        }

    }
}
