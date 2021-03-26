using StarWarsApi.Models;
using System;

namespace StarWarsTerminal.UI
{
    public class TimeGetter
    {
        private (int X, int Y) HourXY { get; set; }
        private (int X, int Y) PriceXY { get; set; }
        private int MaxValue { get; set; }
        public decimal Price { get; set; } = 0;
        public double HoursSelected { get; set; } = 0;
        private Func<SpaceShip, double, decimal> Calculate { get; set; }
        public TimeGetter((int X, int Y) hourXY, (int X, int Y) priceXY, int maxValue, Func<SpaceShip, double, decimal> calculate)
        {
            HourXY = hourXY;
            PriceXY = priceXY;
            MaxValue = maxValue;
            Calculate = calculate;
        }
        public double GetMinutes(SpaceShip ship)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            double hoursSelected = HoursSelected;
            decimal price = Price;

            var priceData = new LineData(PriceXY);
            var hourData = new LineData(HourXY);
            priceData.Update(price.ToString());
            hourData.Update(HoursSelected.ToString());

            var keyInfo = Console.ReadKey(true);

            while (keyInfo.Key != ConsoleKey.Enter)
            {
                if (keyInfo.Key == ConsoleKey.UpArrow && hoursSelected <= MaxValue)
                {
                    hoursSelected++;
                    price = Math.Round(Calculate(ship, hoursSelected), 2);
                    Console.CursorVisible = false;
                    priceData.Update((price * 60).ToString());
                    Console.CursorVisible = true;
                    hourData.Update(hoursSelected.ToString());
                }
                if (keyInfo.Key == ConsoleKey.DownArrow && hoursSelected != 0)
                {
                    hoursSelected--;
                    price = Math.Round(Calculate(ship, hoursSelected), 2);
                    Console.CursorVisible = false;
                    priceData.Update((price * 60).ToString());
                    Console.CursorVisible = true;
                    hourData.Update(hoursSelected.ToString());
                }
                keyInfo = Console.ReadKey(true);
            }
            Price = price;
            HoursSelected = hoursSelected;

            return hoursSelected * 60;
        }
    }
}
