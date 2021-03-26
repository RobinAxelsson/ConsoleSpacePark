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
        public static Option Start()
        {
            ConsoleWriter.ClearScreen();
            string[] lines = Map.GetMap(Option.Start);
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            var selectionList = new SelectionList<Option>(ConsoleColor.Green, '$');
            selectionList.GetCharPositions(drawables);
            selectionList.AddSelections(new Option[] { Option.Login, Option.Identification, Option.Exit });
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();

            return selectionList.GetSelection();
        }
    }
}
