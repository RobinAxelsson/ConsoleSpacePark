using StarWarsApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StarWarsTerminal.UI
{
    public class ListSelection
    {
        public int SelIndex { get; set; }
        public char SelectChar { get; set; }
        public List<IStarwarsItem> StarwarsItems { get; set; }
        public List<(int X, int Y)> XYs { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public ListSelection(ConsoleColor foregroundColor, char selectChar)
        {
            ForegroundColor = foregroundColor;
            SelectChar = selectChar;
        }
        public string[] ConvertIStarwarsItems(IStarwarsItem[] itemArray)
        {
            var starwarsItems = new List<IStarwarsItem>();
            foreach (var item in itemArray)
                if (item.Name.Length < 18)
                    starwarsItems.Add(item);

            starwarsItems = starwarsItems.OrderBy(x => x.Name).ToList();
            StarwarsItems = starwarsItems;

            var nameList = starwarsItems.Select(x => SelectChar + " " + x.Name).ToList();
            int spacings = nameList.Count - 1;
            return nameList.ToArray();
        }
        public void GetCharPositions(List<IDrawable> drawables)
        {
            var targetsXY = new List<(int X, int Y)>();
            var removeTargets = new List<IDrawable>();

            foreach (var item in drawables)
                if (item.Chars == SelectChar.ToString())
                    removeTargets.Add(item);

            targetsXY = removeTargets.Select((x, y) => (x.CoordinateX, x.CoordinateY)).ToList();
            foreach (var remove in removeTargets)
            {
                remove.IsDrawn = true;
            }
            XYs = targetsXY;
        }
        public IStarwarsItem GetSelection()
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ForegroundColor;
            int selIndex = 0;

            Console.SetCursorPosition(XYs[selIndex].X, XYs[selIndex].Y);
            Console.Write(SelectChar);
            bool isDrawn = true;
            var keyInfo = Console.ReadKey(true);

            while (keyInfo.Key != ConsoleKey.Enter)
            {
                if (keyInfo.Key == ConsoleKey.UpArrow && selIndex > 0)
                {
                    selIndex--;
                    isDrawn = false;
                }
                if (keyInfo.Key == ConsoleKey.DownArrow && selIndex < StarwarsItems.Count - 1)
                {
                    selIndex++;
                    isDrawn = false;
                }
                if(isDrawn == false)
                {
                    Console.CursorLeft--;
                    Console.Write(' ');
                    Console.SetCursorPosition(XYs[selIndex].X, XYs[selIndex].Y);
                    Console.Write(SelectChar);
                    isDrawn = true;
                }
                keyInfo = Console.ReadKey(true);
            }

            return StarwarsItems[selIndex];
        }

    }
}
