using StarWarsApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsTerminal.UI
{
    public class Selector
    {
        public int CurrentChoice { get; set; }
        public char SelectChar { get; set; }
        public List<int> Ys { get; set; }
        public int xColumn { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public Selector(ConsoleColor foregroundColor, char selectChar)
        {
            ForegroundColor = foregroundColor;
            SelectChar = selectChar;
        }

    }
}
