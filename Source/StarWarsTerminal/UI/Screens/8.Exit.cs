using System;
using System.Threading;

namespace StarWarsTerminal.UI.Screens
{
    public static partial class Screen
    {
        public static void Exit()
        {
            ConsoleWriter.ClearScreen();
            var lines = Map.GetMap(Option.Exit);
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();
            Thread.Sleep(2000);
            Environment.Exit(0);
        }
    }
}