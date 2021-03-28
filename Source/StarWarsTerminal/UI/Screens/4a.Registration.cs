using StarWarsApi.Database;
using System;
using System.IO;
using static StarWarsTerminal.Main.Program;

namespace StarWarsTerminal.UI.Screens
{
    public static partial class Screen
    {
        public static Option Registration()
        {
            ConsoleWriter.ClearScreen();
            var lines = File.ReadAllLines(@"UI/maps/4a.Registration.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.AllUnitsInXDir(drawables, Console.WindowWidth);
            TextEditor.Center.InYDir(drawables, Console.WindowHeight);
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();

            Func<IDrawable, (int X, int Y)> getCoord = drawable => (drawable.CoordinateX + 2, drawable.CoordinateY);

            var colons = drawables.FindAll(x => x.Chars == ":");

            var nameCoord = getCoord(colons[0]);
            var pass1Coord = getCoord(colons[1]);
            var pass2Coord = getCoord(colons[2]);

            Console.ForegroundColor = ConsoleColor.Green;
            LineTools.SetCursor(nameCoord);

            var nameLine = new InputLine(nameCoord.X, nameCoord.Y, 30, ConsoleColor.Green);
            var pass1Line = new InputLine(pass1Coord.X, pass1Coord.Y, 30, ConsoleColor.Green);
            var pass2Line = new InputLine(pass2Coord.X, pass2Coord.Y, 30, ConsoleColor.Green);

            var accountName = "";
            var password1 = "";
            var password2 = "";

            do
            {
                accountName = nameLine.GetInputString(false);
                password1 = pass1Line.GetInputString(true);
                password2 = pass2Line.GetInputString(true);

                LineTools.ClearAt(nameCoord, accountName);
                LineTools.ClearAt(pass1Coord, password1);
                LineTools.ClearAt(pass2Coord, password2);
            } while (password1 != password2 || accountName.Length <= 5 || password1.Length <= 5);

            _namepass = (accountName, password1);
            if (DatabaseManagement.AccountManagement.Exists(accountName, true))
            {
                ConsoleWriter.ClearScreen();
                LineTools.SetCursor(nameCoord);
                Console.Write("Account name is already taken!");
                Console.ReadLine();
                return Option.Start;
            }

            return Option.RegisterShip;
        }
    }
}