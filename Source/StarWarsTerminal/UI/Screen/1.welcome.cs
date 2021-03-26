using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsTerminal.UI.Screen
{
    public static partial class Screen
    {
        public static void Welcome()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/1.welcome-screen.txt");
            var welcomeText = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.AllUnitsInXDir(welcomeText, Console.WindowWidth);
            TextEditor.Center.InYDir(welcomeText, Console.WindowHeight);
            ConsoleWriter.TryAppend(welcomeText);
            ConsoleWriter.Update();
        }
    }
}
