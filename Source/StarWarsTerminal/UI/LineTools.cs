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
    public class LineData
    {
        private string LastWritten { get; set; } = "";
        private (int X, int Y) XY { get; set; }
        public LineData((int X, int Y) xy)
        {
            XY = xy;
        }
        public void Update(string newString)
        {
            LineTools.SetCursor(XY);
            if (LastWritten.Length > newString.Length)
            {
                Console.Write(newString);
                for (int i = 0; i < (LastWritten.Length - newString.Length); i++)
                    Console.Write(" ");
            }
            else
                Console.Write(newString);
            
            LastWritten = newString;
        }
    }
}
