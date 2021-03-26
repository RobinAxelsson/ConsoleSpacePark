﻿using StarWarsApi.Database;
using System;
using static StarWarsTerminal.Main.Program;

namespace StarWarsTerminal.UI.Screens
{
    public static partial class Screen
    {
        public static Option Login()
        {
            ConsoleWriter.ClearScreen();
            var lines = Map.GetMap(Option.Login);
            var loginText = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.AllUnitsInXDir(loginText, Console.WindowWidth);
            TextEditor.Center.InYDir(loginText, Console.WindowHeight);
            ConsoleWriter.TryAppend(loginText);
            ConsoleWriter.Update();

            var colons = loginText.FindAll(x => x.Chars == ":");
            var nameLine = new InputLine(colons[0], 50, ForegroundColor);

            var accountName = nameLine.GetInputString(false);
            var passwordLine = new InputLine(colons[1], 50, ForegroundColor);
            var password = passwordLine.GetInputString(true);

            LineTools.ClearAt((nameLine.X, nameLine.Y), accountName);
            LineTools.ClearAt((passwordLine.X, passwordLine.Y), password);
            ConsoleWriter.ClearScreen();
            Console.SetCursorPosition(nameLine.X, nameLine.Y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Validating login...");
            _account = DatabaseManagement.AccountManagement.ValidateLogin(accountName, password);
            return _account != null ? Option.Account : Option.Start;
        }
    }
}