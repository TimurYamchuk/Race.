using System;
using RaceGame;

namespace RaceGame
{
    class Sedan : Car
    {
        public Sedan(string name, double initialSpeed, int number) : base(name, initialSpeed, number) { }

        public override void DisplayInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            base.DisplayInfo();
            Console.ResetColor();
        }

        public override void AdjustSpeed()
        {
            Speed = new Random().Next(30, 100); 
        }
    }
}
