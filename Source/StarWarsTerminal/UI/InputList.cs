using System;

namespace StarWarsTerminal.UI
{
    public class InputList
    {
        public int X { get; set; }
        public int Y { get; set; }
        private string[] InputStrings { get; set; }
        private ConsoleColor ForegroundColor { get; set; }
        public InputList(string[] inputStrings, ConsoleColor foregroundColor)
        {
            InputStrings = InputStrings;
            ForegroundColor = foregroundColor;
        }
        public string GetListChoice()
        {
            return "";
        }
        public void Clear()
        {
         
        }

    }
}
