using System.Collections.Generic;

namespace StarWarsTerminal.UI
{
    public interface IDrawContainer
    {
        public List<IDrawable> Drawables { get; set; }
        public void MoveDrawables(int X, int Y);
        public void CenterUnitsXDir(int screenWidth);
        public void CenterInXDirection(List<IDrawable> drawables, int width);
        public void CenterUnitsYDir(int height);
        public void Erase();
    }
}
