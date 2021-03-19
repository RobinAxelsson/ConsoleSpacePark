using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsTerminal.UI
{
    public class TextFrame : IFrame
    {
        public TextFrame(string textPath, ConsoleColor foreGroundColor)
        {
            var textLines = File.ReadAllLines(textPath);

            int x = 0;
            int y = 0;
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
                            ForegroundColor = foreGroundColor,
                            Chars = chr.ToString()
                        };
                        Drawables.Add(drawable);
                    }
                    x++;
                }
                y++;
                x = 0;
            }
        }
        public List<IDrawable> Drawables { get; set; } = new List<IDrawable>();
        public int GetWidth() => Drawables.Select(x => x.CoordinateX).Max();
        public int GetHeight() => Drawables.Select(x => x.CoordinateY).Max();
        public bool IsDrawn() => Drawables.Select(x => x.IsDrawn).ToList().TrueForAll(x => x == true);
        public bool IsErased() => Drawables.Select(x => x.IsErased).ToList().TrueForAll(x => x == true);
        public (int X, int Y) DrawFrame(int firstRow = 0, int firstColumn = 0)
        {
            (int X, int Y) lastRow = (0,0);

            for (int i = 0; i < Drawables.Count; i++)
            {
                Drawables[i].CoordinateY += firstRow;
                Drawables[i].CoordinateX += firstColumn;
                ConsoleWriter.Write(Drawables[i]);
                if (i >= Drawables.Count - 1)
                    lastRow = (Drawables[i].CoordinateX, Drawables[i].CoordinateY);
            }

            return lastRow;
        }
        public (int X, int Y) DrawCentered(int firstRow, int maxX)
        {
            (int X, int Y) lastRow = (0, 0);
            var firstColumn = (maxX - GetWidth()) / 2;

            for (int i = 0; i < Drawables.Count; i++)
            {
                Drawables[i].CoordinateY += firstRow;
                Drawables[i].CoordinateX += firstColumn;
                ConsoleWriter.Write(Drawables[i]);
                if (i >= Drawables.Count - 1)
                    lastRow = (Drawables[i].CoordinateX, Drawables[i].CoordinateY);
            }

            return lastRow;
        }
        public void EraseFrame()
        {
            foreach (var drawable in Drawables)
            {
                ConsoleWriter.Erase(drawable);
            }
        }
    }
}
