using System;

namespace StarWarsTerminal.UI
{
    public class TextDrawable : IDrawable
    {
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public string Chars { get; set; }
        public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;
        public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.White;
        public bool IsDrawn { get; set; }
        public bool IsErased { get; set; }

        public bool IsSame(IDrawable drawable) { return false; }
    }
}
