using StarWarsApi.Database;
using System;
using System.IO;
using System.Linq;
using static StarWarsTerminal.Main.Program;

namespace StarWarsTerminal.UI.Screens
{
    public static partial class Screen
    {
        public static Option Parking()
        {
            ConsoleWriter.ClearScreen();

            var lines = File.ReadAllLines(@"UI/maps/7.Parking.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            var selectionList = new SelectionList<Option>(ConsoleColor.Green, '$');
            selectionList.GetCharPositions(drawables);
            selectionList.AddSelections(new[]
            {
                Option.PurchaseTicket,
                Option.ReEnterhours,
                Option.Account
            });

            var drawProps = drawables.FindAll(x => x.Chars == "¤");
            var props = drawProps.Select(x => (x.CoordinateX, x.CoordinateY)).ToList();
            var parkFromXY = props[0];
            var pricePerHourXY = props[1];
            var shipLengthXY = props[2];

            var calculatedPriceXY = props[3];
            var enterHoursXY = props[4];
            var receiptXY = props[5];
            var openNext = DatabaseManagement.ParkingManagement.CheckParkingStatus();

            Console.ForegroundColor = ConsoleColor.Green;
            LineTools.SetCursor(parkFromXY);

            if (openNext.isOpen)
                Console.Write("Now");
            else
                Console.Write(openNext.nextAvailable);

            LineTools.SetCursor(pricePerHourXY);
            Console.Write(DatabaseManagement.ParkingManagement.CalculatePrice(_account.SpaceShip, 1) * 60);
            LineTools.SetCursor(shipLengthXY);
            Console.Write(_account.SpaceShip.ShipLength);

            ConsoleWriter.TryAppend(drawables.Except(drawProps).ToList());
            ConsoleWriter.Update();

            Option menuSel;
            double hours;


            var timeGetter = new TimeGetter(enterHoursXY, calculatedPriceXY, 10000,
                DatabaseManagement.ParkingManagement.CalculatePrice);

            if (openNext.isOpen == false)
            {
                Console.ReadKey(true);
                return Option.Account;
            }

            do
            {
                hours = timeGetter.GetMinutes(_account.SpaceShip);
                menuSel = selectionList.GetSelection();

                if (menuSel == Option.PurchaseTicket && hours == 0)
                    menuSel = Option.ReEnterhours;
            } while (menuSel == Option.ReEnterhours);

            if (Option.PurchaseTicket == menuSel)
            {
                ConsoleWriter.ClearScreen();

                var receipt = DatabaseManagement.ParkingManagement.SendInvoice(_account, hours);
                var boxData = new BoxData((Console.WindowWidth/2 - 10, parkFromXY.CoordinateY));
                boxData.Update(new[] { "Loading receipt..." });

                string[] receiptString =
                {
                    "Purchase successful!",
                    "",
                    "Ticket Holder: " + receipt.Account.AccountName,
                    "Start time: " + receipt.StartTime,
                    "End time: " + receipt.EndTime,
                    "Price: " + receipt.Price
                };
                boxData.Update(receiptString);

                Console.ReadKey(true);
            }

            return Option.Account;
        }
    }
}