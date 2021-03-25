using StarWarsApi.Models;
using System;

namespace StarWarsTerminal.UI
{
    public class PriceGetter
    {
        private (int X, int Y) HourXY { get; set; }
        private (int X, int Y) PriceXY { get; set; }
        private int MaxValue { get; set; }
        public double Price { get; set; } = 0;
        public double HoursSelected { get; set; } = 0;
        private Func<SpaceShip, double, double> Calculate { get; set; }
        public PriceGetter((int X, int Y) hourXY, (int X, int Y) priceXY, int maxValue, Func<SpaceShip, double, double> calculate)
        {
            HourXY = hourXY;
            PriceXY = priceXY;
            MaxValue = maxValue;
            Calculate = calculate;
        }
        public double GetPrice(SpaceShip ship)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;
            double hoursSelected = HoursSelected;
            double price = Price;

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
                    price = Calculate(ship, hoursSelected);
                    Console.CursorVisible = false;
                    priceData.Update(price.ToString());
                    Console.CursorVisible = true;
                    hourData.Update(hoursSelected.ToString());
                }
                if (keyInfo.Key == ConsoleKey.DownArrow && hoursSelected != 0)
                {
                    hoursSelected--;
                    price = Calculate(ship, hoursSelected);
                    Console.CursorVisible = false;
                    priceData.Update(price.ToString());
                    Console.CursorVisible = true;
                    hourData.Update(hoursSelected.ToString());
                }
                keyInfo = Console.ReadKey(true);
            }
            Price = price;
            HoursSelected = hoursSelected;

            return price;
        }
    }
}
