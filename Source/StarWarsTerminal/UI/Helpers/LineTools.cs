using System;

namespace StarWarsTerminal.UI
{
    public static class LineTools
    {
        public static void ClearAt((int X, int Y) XY, string entered)
        {
            SetCursor(XY);
            foreach (var chr in entered)
                Console.Write(" ");
            SetCursor(XY);
        }

        public static void ClearAt((int X, int Y) XY, int count)
        {
            SetCursor(XY);
            for (var i = 0; i < count; i++)
                Console.Write(" ");
            SetCursor(XY);
        }

        public static void SetCursor((int X, int Y) XY)
        {
            Console.SetCursorPosition(XY.X, XY.Y);
        }
    }

    public class LineData
    {
        public LineData((int X, int Y) xy)
        {
            XY = xy;
        }

        private string LastWritten { get; set; } = "";
        private (int X, int Y) XY { get; }

        public void Update(string newString)
        {
            LineTools.SetCursor(XY);
            if (LastWritten.Length > newString.Length)
            {
                Console.Write(newString);
                for (var i = 0; i < LastWritten.Length - newString.Length; i++)
                    Console.Write(" ");
            }
            else
            {
                Console.Write(newString);
            }

            LastWritten = newString;
        }
    }
}