using System;

namespace StarWarsTerminal.UI
{
    public static class LineTools
    {
        public static void ClearAt((int X, int Y) XY, string entered)
        {
            SetCursor(XY);
            foreach (char chr in entered)
                Console.Write(" ");
            SetCursor(XY);
        }
        public static void ClearAt((int X, int Y) XY, int count)
        {
            SetCursor(XY);
            for (int i = 0; i < count; i++)
                Console.Write(" ");
            SetCursor(XY);
        }
        public static void SetCursor((int X, int Y) XY) => Console.SetCursorPosition(XY.X, XY.Y);
    }
}
