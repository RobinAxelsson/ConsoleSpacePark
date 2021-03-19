using System;

namespace StarWarsTerminal.UI
{
    public interface IDrawable
    {
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public string Chars { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public bool IsDrawn { get; set; }
        public bool IsErased { get; set; }
        public bool IsSame(IDrawable drawable);
    }
}
