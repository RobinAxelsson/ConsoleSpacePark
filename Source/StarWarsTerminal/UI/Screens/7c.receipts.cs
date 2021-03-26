using StarWarsApi.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using static StarWarsTerminal.Main.Program;

namespace StarWarsTerminal.UI.Screens
{
    public static partial class Screen
    {
        public static Option Receipts()
        {
            ConsoleWriter.ClearScreen();
            var lines = Map.GetMap(Option.Receipts);

            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.AllUnitsInXDir(drawables, Console.WindowWidth);
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();
            var receipts = DatabaseManagement.AccountManagement.GetAccountReceipts(_account);
            var receiptStrings = new List<string>();
            foreach (var receipt in receipts)
            {
                var addReceiptList = new List<string>
                {
                    "Start time: " + receipt.StartTime,
                    "\n",
                    "End time: " + receipt.EndTime,
                    "\n",
                    "Price: " + receipt.Price,
                    "\n",
                    "\n"
                };
                receiptStrings.AddRange(addReceiptList);
            }

            var maxY = drawables.Max(x => x.CoordinateY);
            var leftX = drawables.Min(x => x.CoordinateX);

            Console.SetCursorPosition(0, maxY + 3);

            foreach (var s in receiptStrings)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.CursorLeft = leftX + 27;
                Console.Write(s);
            }

            Console.ReadLine();
            return Option.Account;
        }
    }
}