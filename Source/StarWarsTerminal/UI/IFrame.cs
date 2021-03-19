using System.Collections.Generic;

namespace StarWarsTerminal.UI
{
    public interface IFrame
    {
        public List<IDrawable> Drawables { get; set; }
        public int GetWidth();
        public int GetHeight();
        public bool IsDrawn();
        public bool IsErased();
        public (int X, int Y) DrawFrame(int firstRow = 0, int firstColumn = 0);
        public void EraseFrame();
    }
}
