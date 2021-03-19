using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StarWarsTerminal.UI
{
    public static class ConsoleWriter
    {
        public static void Write(IDrawable drawable)
        {
            //if (drawable.IsDrawn == true) return;
            Console.ForegroundColor = drawable.ForegroundColor;
            Console.BackgroundColor = drawable.BackgroundColor;
            Console.SetCursorPosition(drawable.CoordinateX, drawable.CoordinateY);
            Console.Write(drawable.Chars);
            drawable.IsDrawn = true;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Erase(IDrawable drawable)
        {
            if (drawable.IsDrawn == false) return;

            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(drawable.CoordinateX, drawable.CoordinateY);
            Console.Write(" ");
            drawable.IsErased = true;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
