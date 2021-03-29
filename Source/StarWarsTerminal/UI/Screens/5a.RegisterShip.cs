using Newtonsoft.Json;
using StarWarsApi.Database;
using StarWarsApi.Models;
using System;
using System.IO;
using System.Linq;
using StarWarsApi.Networking;
using static StarWarsTerminal.Main.Program;

namespace StarWarsTerminal.UI.Screens
{
    public static partial class Screen
    {
        public static Option RegisterShip(bool reRegister = false)
        {
            ConsoleWriter.ClearScreen();
            var lines = File.ReadAllLines(@"UI/maps/5a.RegisterShip.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            var nextLine = drawables.Max(x => x.CoordinateY);
            var ships = APICollector.ReturnShipsAsync().Where(s => double.Parse(s.ShipLength) <= 150).ToArray();
            var shipLines = ships.Select(x => "$ " + x.Model).ToArray();
            drawables.AddRange(TextEditor.Add.DrawablesAt(shipLines, nextLine + 3));
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);

            var selectionList = new SelectionList<SpaceShip>(ForegroundColor, '$');
            selectionList.GetCharPositions(drawables);
            selectionList.AddSelections(ships);
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();

            var ship = selectionList.GetSelection();

            if (reRegister)
            {
                DatabaseManagement.AccountManagement.ReRegisterShip(_account, ship);
                return Option.Account;
            }

            DatabaseManagement.AccountManagement.Register(_account.User, ship, _namepass.accountName,
                _namepass.password);
            return Option.Login;
        }
    }
}