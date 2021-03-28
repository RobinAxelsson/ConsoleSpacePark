using StarWarsApi.Database;
using StarWarsApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static StarWarsTerminal.Main.Program;

namespace StarWarsTerminal.UI.Screens
{
    public static partial class Screen
    {
        public static Option Receipts()
        {
            ConsoleWriter.ClearScreen();
            var lines = File.ReadAllLines(@"UI/maps/7.Receipts.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.AllUnitsInXDir(drawables, Console.WindowWidth);
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();
            var receipts = DatabaseManagement.AccountManagement.GetAccountReceipts(_account);
            
            var receiptStrings = new List<string[]>();

            foreach (var receipt in receipts)
            {
                var addReceiptList = new string[]
                {
                    "____________________________",
                    "",
                    "Start time: " + receipt.StartTime,
                    "",
                    "End time: " + receipt.EndTime,
                    "",
                    "Price: " + receipt.Price,
                    "",
                    "",
                };
                receiptStrings.Add(addReceiptList);
            }

            var boxes = new List<BoxData>();
            var maxY = drawables.Max(x => x.CoordinateY);

            Console.ForegroundColor = ConsoleColor.Green;

            int leftX =
                receiptStrings.Count > 4 ? Console.WindowWidth / 6 :
                receiptStrings.Count > 3 ? Console.WindowWidth / 5 :
                receiptStrings.Count > 2 ? Console.WindowWidth / 4 :
                receiptStrings.Count > 0 ? Console.WindowWidth / 3 : 0;
            
            int x = 0;
            int y = 0;
            foreach (var r in receiptStrings)
            {
                var newBox = new BoxData((leftX + x * 40, maxY + 5 + y * 10));
                newBox.Update(r);
                x++;
                y = x == 4 ? y + 1 : y;
                x = x == 4 ? 0 : x;
            }

            Console.ReadLine();
            return Option.Account;
        }
    }
}