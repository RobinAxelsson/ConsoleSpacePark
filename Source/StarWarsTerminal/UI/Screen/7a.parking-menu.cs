using StarWarsTerminal.Main;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StarWarsTerminal.Main.Program;

namespace StarWarsTerminal.UI.Screen
{
    public static partial class Screen
    {
        public static Option ParkingScreen()
        {
            ConsoleWriter.ClearScreen();
            string[] lines = File.ReadAllLines(@"UI/TextFrames/7.parking-menu.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            var selectionList = new SelectionList<Option>(ConsoleColor.Green, '$');
            selectionList.GetCharPositions(drawables);
            selectionList.AddSelections(new Option[]
            {
                Option.PurchaseTicket,
                Option.ReEnterhours,
                Option.Login
            });

            var drawProps = drawables.FindAll(x => x.Chars == "¤");
            var props = drawProps.Select(x => (x.CoordinateX, x.CoordinateY)).ToList();
            var parkFromXY = props[0];
            var pricePerHourXY = props[1];
            var shipLengthXY = props[2];

            var calculatedPriceXY = props[3];
            var enterHoursXY = props[4];

            Console.ForegroundColor = ConsoleColor.Green;
            LineTools.SetCursor(parkFromXY);
            Console.Write("Now");
            LineTools.SetCursor(pricePerHourXY);
            Console.Write("10");
            LineTools.SetCursor(shipLengthXY);
            Console.Write("45");
            ConsoleWriter.TryAppend(drawables.Except(drawProps).ToList());
            ConsoleWriter.Update();

            Func<double, double, double> calculate = (double length, double hours)
               => length * hours / 10;
            Option menuSel;
            var priceGetter = new PriceGetter(enterHoursXY, calculatedPriceXY, 10000, calculate);
            do
            {
                double price = priceGetter.GetPrice(45);
                menuSel = selectionList.GetSelection();
            } while (menuSel == Option.ReEnterhours);

            return menuSel;
        }
    }
}
