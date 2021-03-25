using System;

namespace StarWarsTerminal.UI
{
    public class HourPriceSelection
    {
        private (int X, int Y) HourXY { get; set; }
        private (int X, int Y) PriceXY { get; set; }
        private int MaxValue { get; set; }
        private int HoursSelected { get; set; } = 0;
        private Func<double, double> Calculate { get; set; }
        public HourPriceSelection((int X, int Y) hourXY, (int X, int Y) priceXY, int maxValue, Func<double, double> calculate)
        {
            HourXY = hourXY;
            PriceXY = priceXY;
            MaxValue = maxValue;
            Calculate = calculate;
        }
        public double GetSelection()
        {
            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.Green;
            int hoursSelected = HoursSelected;
            double price;
            LineTools.SetCursor(HourXY);
            Console.Write(hoursSelected.ToString());

            var keyInfo = Console.ReadKey(true);

            while (keyInfo.Key != ConsoleKey.Enter)
            {
                if (keyInfo.Key == ConsoleKey.UpArrow && hoursSelected <= MaxValue)
                {
                    HoursSelected++;
                    price = Calculate(HoursSelected);
                    ConsoleWriter.UpdateDynamicString(hoursSelected.ToString(), HourXY);
                    ConsoleWriter.UpdateDynamicString(price.ToString(), PriceXY);
                }
                if (keyInfo.Key == ConsoleKey.DownArrow && hoursSelected != 0)
                {
                    HoursSelected--;
                    price = Calculate(HoursSelected);
                    ConsoleWriter.UpdateDynamicString(hoursSelected.ToString(), HourXY);
                    ConsoleWriter.UpdateDynamicString(price.ToString(), PriceXY);
                }
                keyInfo = Console.ReadKey(true);
            }
            return hoursSelected;
        }
    }
}
