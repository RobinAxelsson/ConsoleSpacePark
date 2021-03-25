using StarWarsApi.Database;
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
        public static Option LoginPasswordScreen()
        {
            ConsoleWriter.ClearScreen();
            string[] lines = File.ReadAllLines(@"UI/TextFrames/3b.login.txt");
            var loginText = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.AllUnitsInXDir(loginText, Console.WindowWidth);
            TextEditor.Center.InYDir(loginText, Console.WindowHeight);
            ConsoleWriter.TryAppend(loginText);
            ConsoleWriter.Update();

            var colons = loginText.FindAll(x => x.Chars == ":");
            var nameLine = new InputLine(colons[0], 50, ForegroundColor);

            string accountName = nameLine.GetInputString(false);
            var passwordLine = new InputLine(colons[1], 50, ForegroundColor);
            string password = passwordLine.GetInputString(isPassword: true);

            LineTools.ClearAt((nameLine.X, nameLine.Y), accountName);
            LineTools.ClearAt((passwordLine.X, passwordLine.Y), password);

            var accountManagement = new DatabaseManagement.AccountManagement();

            _account = accountManagement.ValidateLogin(accountName, password);
            if (_account != null)
            {
                return Option.GotoAccount;
            }
            else
            {
                return Option.StartScreen;
            }
        }
    }
}
