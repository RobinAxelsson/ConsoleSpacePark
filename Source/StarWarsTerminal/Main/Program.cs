using System;
using StarWarsApi.Models;
using StarWarsApi.Repository;
using StarWarsApi.Database;
using StarWarsTerminal.UI;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading;

namespace StarWarsTerminal.Main
{
    class Program
    {

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static readonly IntPtr ThisConsole = GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;

        private static void Main(string[] args)
        {
            ShowWindow(ThisConsole, MAXIMIZE);
            Thread.Sleep(500);

            var welcomeScreen = SetUpWelcomeScreen();
            Thread.Sleep(2000);
            welcomeScreen.Clear();

            var credScreen = CredentialsScreen();
            var dollar1 = credScreen.Frames[2].Drawables.Find(x => x.Chars == ":");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            if (dollar1 != null) Console.SetCursorPosition(dollar1.CoordinateX + 3, dollar1.CoordinateY);
            Console.ReadLine();
            var dollar2 = credScreen.Frames[2].Drawables.FindLast(x => x.Chars == ":");
            if (dollar2 != null) Console.SetCursorPosition(dollar2.CoordinateX + 3, dollar2.CoordinateY);

            var keyInfo = Console.ReadKey(true);
            string password = "";
            while(keyInfo.Key != ConsoleKey.Enter)
            {
                Console.CursorVisible = false;
                keyInfo = Console.ReadKey(true);

                if(keyInfo.Key == ConsoleKey.Backspace)
                {
                    if(password.Length > 0)
                    {
                        password = password.Remove(password.Length - 1);
                        int x = Console.CursorLeft;
                        Console.CursorLeft = x - 1;
                        Console.Write(" ");
                        Console.CursorLeft = x - 1;
                    }
                    continue;
                }

                if (password.Length >= 43) continue;
                password += keyInfo.KeyChar;
                Console.Write("*");
            }
            Console.WriteLine(password);
            Console.ReadLine();
        }

        private static Screen CredentialsScreen()
        {
            var credFrame = new TextFrame(@"UI/TextFrames/2.1_enter.txt", ConsoleColor.DarkYellow);
            var credFrame2 = new TextFrame(@"UI/TextFrames/2.2_credentials.txt", ConsoleColor.DarkYellow);
            var credLines = new TextFrame(@"UI/TextFrames/2.2_Form.txt", ConsoleColor.DarkYellow);
            var credScreen = new Screen(new List<IFrame> { credFrame, credFrame2, credLines });
            int width = Console.WindowWidth;

            int totalHeight = credScreen.GetFramesTotalHeight();
            int consoleHeight = Console.WindowHeight;
            int startRow = consoleHeight/8;
            var lastRow = credFrame.DrawCentered(startRow, width);
            lastRow = credFrame2.DrawCentered(firstRow: lastRow.Y + 2, width);
            lastRow = credLines.DrawCentered(firstRow: lastRow.Y + 5, width);

            return credScreen;
        }

        private static Screen SetUpWelcomeScreen()
        {
            var welcomeFrame = new TextFrame(@"UI/TextFrames/1.1_welcome-text.txt", ConsoleColor.DarkYellow);
            var welcomeFrame2 = new TextFrame(@"UI/TextFrames/1.2_welcome-text.txt", ConsoleColor.DarkYellow);
            var welcomeFrame3 = new TextFrame(@"UI/TextFrames/1.3_welcome-text.txt", ConsoleColor.DarkYellow);

            var welcomeScreen = new Screen(new List<IFrame> {welcomeFrame, welcomeFrame2, welcomeFrame3 });

            int width = Console.WindowWidth;

            int totalHeight = welcomeScreen.GetFramesTotalHeight();
            int consoleHeight = Console.WindowHeight;
            int startRow = consoleHeight - (totalHeight + 4);
            startRow /= 2;
            var lastRow = welcomeFrame.DrawCentered(startRow, width);
            lastRow = welcomeFrame2.DrawCentered(firstRow: lastRow.Y + 2, width);
            welcomeFrame3.DrawCentered(firstRow: lastRow.Y + 2, width);

            return welcomeScreen;
        }
    }
}