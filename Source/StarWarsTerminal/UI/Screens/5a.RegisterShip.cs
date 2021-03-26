using Newtonsoft.Json;
using StarWarsApi.Database;
using StarWarsApi.Models;
using StarWarsTerminal.Main;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarWarsTerminal.UI.Screens;
using static StarWarsTerminal.Main.Program;

namespace StarWarsTerminal.UI.Screens
{
    public static partial class Screen
    {
        public static Option RegisterShip(bool reRegister = false)
        {
            ConsoleWriter.ClearScreen();
            var lines = Map.GetMap(Option.RegisterShip);
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            var nextLine = drawables.Max(x => x.CoordinateY);
            var jsonString = File.ReadAllText(@"UI/json/small-ships.json");
            var localShips = JsonConvert.DeserializeObject<SpaceShip[]>(jsonString);
            var shipLines = localShips.Select(x => "$ " + x.Model).ToArray();
            drawables.AddRange(TextEditor.Add.DrawablesAt(shipLines, nextLine + 3));
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);

            var selectionList = new SelectionList<SpaceShip>(ForegroundColor, '$');
            selectionList.GetCharPositions(drawables);
            selectionList.AddSelections(localShips);
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();

            var ship = selectionList.GetSelection();

            if(reRegister == true)
            {
                DatabaseManagement.AccountManagement.ReRegisterShip(_account, ship);
                return Option.Account;
            }
            else
            {
                DatabaseManagement.AccountManagement.Register(_account.User, ship, _namepass.accountName, _namepass.password);
                return Option.Login;
            }

        }
    }
}
