using System;
using System.Collections.Generic;
using System.Threading;

namespace RaceGame
{
    class Race
    {
        private List<Car> cars;
        private bool isRaceOngoing;
        private Car raceWinner;

        public delegate void RaceEvent();
        public event RaceEvent RaceStarted;
        public event RaceEvent RaceStep;
        public event EventHandler<string> RaceFinished;

        public Race()
        {
            cars = new List<Car>();
            isRaceOngoing = false;
        }

        // Добавляем автомобиль в гонку
        public void RegisterCar(Car car)
        {
            car.Finished += OnCarFinished;
            cars.Add(car);
        }

        // Старт гонки
        public void BeginRace()
        {
            Console.WriteLine("Гонка начнется через 3 секунды! Настройтесь...\n");

            RaceStarted?.Invoke();

            Thread.Sleep(3000);

            isRaceOngoing = true;
            Timer raceTimer = new Timer(ExecuteRaceStep, null, 0, 500);

            // Ожидаем завершения гонки
            while (isRaceOngoing) { }

            raceTimer.Dispose();
        }

        // Шаг гонки (каждые 0.5 секунды)
        private void ExecuteRaceStep(object state)
        {
            RaceStep?.Invoke();

            Console.Clear();
            Console.WriteLine("Текущие позиции участников:");

            foreach (var car in cars)
            {
                car.DisplayInfo();
            }

            // Отображаем все автомобили в их текущих позициях
            foreach (var car in cars)
            {
                car.Render();
            }
        }

        // Подготовка автомобилей к гонке
        public void PrepareRace()
        {
            Console.WriteLine("Готовимся к гонке. Стартовые позиции:");
            foreach (var car in cars)
            {
                car.DisplayInfo();
            }
            Console.WriteLine();
        }

        // Событие, которое срабатывает, когда один из автомобилей финиширует
        private void OnCarFinished(object sender, EventArgs e)
        {
            if (sender is Car car && isRaceOngoing)
            {
                isRaceOngoing = false;
                raceWinner = car;

                Console.Clear();
                RaceFinished?.Invoke(this, $"Победитель гонки: #{raceWinner.Number} - {raceWinner.Name}");
            }
        }
    }

    abstract class Car
    {
        public event EventHandler Finished;
        public double Speed { get; protected set; }
        public string Name { get; private set; }
        public int Number { get; private set; }
        public double Position { get; protected set; } = 0;
        protected double FinishSpeed { get; set; } = 100.0;
        protected Random random = new Random();

        public Car(string name, double initialSpeed, int num)
        {
            Name = name;
            Speed = initialSpeed;
            Number = num;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"#{Number} - {Name}: Позиция {Position:F1}, Скорость: {Speed:F1}");
        }

        public void Drive()
        {
            Position += Speed * 0.1;
            if (Position >= FinishSpeed)
            {
                Position = FinishSpeed;
                OnFinished();
            }
        }

        public virtual void AdjustSpeed()
        {
            Speed = random.Next(30, 100);
        }

        protected virtual void OnFinished()
        {
            Finished?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Render()
        {
            int carPosition = (int)Position;
            Console.WriteLine(new string(' ', carPosition) + $"#{Number}");
        }
    }

    class PassengerCar : Car
    {
        public PassengerCar(string name, double initialSpeed, int num) : base(name, initialSpeed, num) { }

        public override void AdjustSpeed()
        {
            Speed = random.Next(30, 100); 
        }

        public override void DisplayInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            base.DisplayInfo();
            Console.ResetColor();
        }
    }
}
