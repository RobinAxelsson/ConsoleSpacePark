using System;

namespace StarWarsTerminal.UI.Screens
{
    public static partial class Screen
    {
        public static Option Welcome()
        {
            ConsoleWriter.ClearScreen();
            var lines = Map.GetMap(Option.Welcome);
            var welcomeText = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(welcomeText, Console.WindowWidth, Console.WindowHeight);
            ConsoleWriter.TryAppend(welcomeText);
            ConsoleWriter.Update();

            return Option.Start;
        }
    }
}