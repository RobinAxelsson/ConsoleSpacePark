using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StarWarsTerminal.UI
{
    public class TextContainer : IDrawContainer
    {
        public List<IDrawable> Drawables { get; set; } = new List<IDrawable>();
        private List<int> BlankRows { get; set; } = new List<int>();
        private ConsoleColor ForegroundColor { get; set; }

        public TextContainer(string[] textLines, ConsoleColor foreGroundColor)
        {
            ForegroundColor = foreGroundColor;
            AddLines(textLines, 0);
        }
        public void AddLines(string[] textLines, int spacing)
        {
            int x = 0;
            int y = 0;
            if (Drawables.Count > 0)
                y = Drawables.Select(x => x.CoordinateY).Max() + 1 + spacing;

            foreach (var line in textLines)
            {
                foreach (char chr in line)
                {
                    if (chr != ' ')
                    {
                        var drawable = new TextDrawable()
                        {
                            CoordinateX = x,
                            CoordinateY = y,
                            ForegroundColor = this.ForegroundColor,
                            Chars = chr.ToString()
                        };
                        Drawables.Add(drawable);
                    }
                    x++;
                }
                if (Drawables.Select(x => x.CoordinateY).Contains(y) == false)
                    BlankRows.Add(y);
                y++;
                x = 0;
            }
        }
        public void CenterUnitsXDir(int screenWidth)
        {
            var blankRows = BlankRows;
            if (BlankRows[0] != 0)
                BlankRows.Insert(0, 0);

            var Ymax = Drawables.Select(x => x.CoordinateY).Max();
            BlankRows.Add(Ymax +1);

            for (int i = 0; i < BlankRows.Count-1; i++)
            {
                var rowsCombined = Drawables.Where(x => x.CoordinateY >= BlankRows[i] && x.CoordinateY <= BlankRows[i+1]).ToList();
                if(rowsCombined.Count != 0)
                    CenterInXDirection(rowsCombined, screenWidth);
            }            
        }
        public void CenterInXDirection(List<IDrawable> drawables, int width)
        {
            int xMax = drawables.Max(x => x.CoordinateX);
            int rightStep = (width - xMax) / 2;
            drawables.ForEach(x => x.CoordinateX += rightStep);
        }
        public void CenterUnitsYDir(int height)
        {
            int Ymax = Drawables.Select(x => x.CoordinateY).Max();
            int move = (height - Ymax) / 2;
            Drawables.ForEach(x => x.CoordinateY += move);
        }
        public void MoveDrawables(int X, int Y) { }
        public void Erase() => Drawables.ForEach(x => x.Erase = true);
    }
}
