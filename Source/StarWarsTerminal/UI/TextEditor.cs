using StarWarsApi.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StarWarsTerminal.UI
{
    public static class TextEditor
    {
        private static ConsoleColor ForegroundColor { get; set; } = ConsoleColor.Green;
        public static List<int> GetBlankRows(List<IDrawable> drawables)
        {
            var blanks = new List<int>();

            int yMin = drawables.Select(x => x.CoordinateY).Min();
            int yMAx = drawables.Select(x => x.CoordinateY).Max();

            for (int y = yMin; y <= yMAx; y++)
            {
                bool yExist = drawables.Exists(x => x.CoordinateY == y);
                if (yExist == false)
                    blanks.Add(y);
            }
            return blanks;
        }

        public static List<IDrawable> DrawablesAt(string[] textLines, int firstRow)
        {
            var drawables = new List<IDrawable>();

            int x = 0;
            int y = firstRow;
            
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
                            ForegroundColor = ForegroundColor,
                            Chars = chr.ToString()
                        };
                        drawables.Add(drawable);
                    }
                    x++;
                }
                y++;
                x = 0;
            }

            return drawables;
        }

        public static void AddWithSpacing(List<IDrawable> drawables, string[] textLines, int spacing)
        {
            var Ymax = drawables.Select(x => x.CoordinateY).Max();
            var newDrawables = DrawablesAt(textLines, Ymax + spacing);
            drawables.AddRange(newDrawables);
        }
        public static List<IDrawable> GetRowUnitAt(List<IDrawable> drawables, int unitIndex) => GetRowUnits(drawables)[unitIndex];
        public static List<List<IDrawable>> GetRowUnits(List<IDrawable> drawables)
        {
            var unitDrawables = new List<List<IDrawable>>();
            var blankRows = GetBlankRows(drawables);
            int yMin = drawables.Select(x => x.CoordinateY).Min();
            int yMAx = drawables.Select(x => x.CoordinateY).Max();

            blankRows.Insert(0, yMin);
            blankRows.Add(yMAx);

            for (int i = 0; i < blankRows.Count - 1; i++)
            {
                var unit = drawables.FindAll(x => x.CoordinateY >= blankRows[i] && x.CoordinateY <= blankRows[i + 1]);
                if(unit.Count != 0)
                    unitDrawables.Add(unit);
            }
            return unitDrawables;

        }
        public static void CenterInXDir(List<IDrawable> drawables, int screenWidth)
        {
            int xMax = drawables.Max(x => x.CoordinateX);
            int xMin = drawables.Min(x => x.CoordinateX);
            int xDiff = xMax - xMin;
            int leftColumnPosition = (screenWidth - xDiff) / 2;
            int move = leftColumnPosition - xMin;
            drawables.ForEach(x => x.CoordinateX += move);
        }      
        public static void CenterAllUnitsInXDir(List<IDrawable> drawables, int screenWidth)
        {
            var listOfLists = GetRowUnits(drawables);
            foreach (var list in listOfLists)
            {
                CenterInXDir(list, screenWidth);
            }
        }
        public static void CenterInYDir(List<IDrawable> drawables, int screenHeight)
        {
            int yMax = drawables.Max(x => x.CoordinateY);
            int yMin = drawables.Min(x => x.CoordinateY);
            int yDiff = yMax - yMin;
            int topRowPosition = (screenHeight - yDiff) / 2;
            int move = topRowPosition - yMin;
            drawables.ForEach(x => x.CoordinateY += move);
        }
    }
}
