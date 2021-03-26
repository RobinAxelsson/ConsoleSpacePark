using StarWarsTerminal.Main;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarWarsTerminal.UI.Screens;

namespace StarWarsTerminal.UI.Screens
{
    public static partial class Screen
    {
        public static Option Welcome()
        {
            ConsoleWriter.ClearScreen();
            string[] lines = Map.GetMap(Option.Welcome);
            var welcomeText = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(welcomeText, Console.WindowWidth, Console.WindowHeight);
            ConsoleWriter.TryAppend(welcomeText);
            ConsoleWriter.Update();

            return Option.Start;
        }
    }
}
