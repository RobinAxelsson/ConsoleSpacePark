using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using StarWarsTerminal.UI.Screens;

namespace StarWarsTerminal.UI.Screens
{
    public static partial class Screen
    {
        public static void Exit()
        {
            ConsoleWriter.ClearScreen();
            string[] lines = Map.GetMap(Option.Exit);
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();
            Thread.Sleep(2000);
            Environment.Exit(0);
        }
    }
}
