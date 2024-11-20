using System;
using RaceGame;  // Пространство имен для класса Game
using Race;      // Пространство имен для классов автомобилей

namespace RacingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RaceGame.Game raceGame = new RaceGame.Game();

                raceGame.FinishedDrive += (sender, winnerMessage) =>
                {
                    Console.WriteLine($"\nГонка завершена! {winnerMessage}");
                };
                Car sportCar = new Race.SportCar("Sport car", 0, 1);
                Car bus = new Race.Bus("Bus", 0, 2);
                Car sedan = new Race.Sedan("Sedan", 0, 3);
                Car truck = new Race.Truck("Truck", 0, 4);

                raceGame.RegisterCar(sportCar);
                raceGame.RegisterCar(bus);
                raceGame.RegisterCar(sedan);
                raceGame.RegisterCar(truck);

                raceGame.RaceStarted += () => raceGame.PrepareRace();

                raceGame.RaceStep += () =>
                {
                    foreach (var car in raceGame.Cars)
                    {
                        car.AdjustSpeed();
                        car.Drive();
                    }
                };

                raceGame.BeginRace();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
