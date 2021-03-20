using System.Collections.Generic;

namespace StarWarsTerminal.UI
{
    public interface IDrawContainer
    {
        public List<IDrawable> Drawables { get; set; }
        public void CenterRows(int first, int last);
        public void MoveDrawables(int X, int Y);
        public void Erase();
    }
}
