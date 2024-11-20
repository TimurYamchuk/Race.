using System;
using System.Runtime.ConstrainedExecution;
using RaceGame;

namespace RaceGame
{
    class Bus : Car
    {
        public Bus(string name, double initialSpeed, int number) : base(name, initialSpeed, number) { }

        public override void DisplayInfo()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            base.DisplayInfo();  
            Console.ResetColor();
        }

        public override void AdjustSpeed()
        {
            Speed = new Random().Next(30, 100);
        }

    }
}
