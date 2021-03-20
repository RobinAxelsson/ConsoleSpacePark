using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StarWarsTerminal.UI
{
    public static class ConsoleWriter
    {
        private static List<IDrawable> ScreenMemory = new List<IDrawable>();
        public static void TryAdd(IDrawable drawable) { }
        public static void TryAdd(List<IDrawable> drawables) { }
        public static void TryAdd(IDrawContainer container) { }
        public static void Update()
        {
            foreach (var drawable in ScreenMemory)
            {
                if(drawable.IsErased == false && drawable.IsDrawn == false)
                    Write(drawable);
                else if(drawable.IsErased == true)
                {
                    Erase(drawable);
                    ScreenMemory.Remove(drawable);
                }
            }
        }
        private static void Write(IDrawable drawable)
        {
            Console.ForegroundColor = drawable.ForegroundColor;
            Console.BackgroundColor = drawable.BackgroundColor;
            Console.SetCursorPosition(drawable.CoordinateX, drawable.CoordinateY);
            Console.Write(drawable.Chars);
            drawable.IsDrawn = true;
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void Erase(IDrawable drawable)
        {
            if (drawable.IsDrawn == false) return;

            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(drawable.CoordinateX, drawable.CoordinateY);
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
