using StarWarsApi.Database;
using StarWarsTerminal.Main;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static StarWarsTerminal.Main.Program;

namespace StarWarsTerminal.UI.Screen
{
    public static partial class Screen
    {
        public static Option IdentificationScreen()
        {
            ConsoleWriter.ClearScreen();
            string[] lines = File.ReadAllLines(@"UI/TextFrames/3a.Identification.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();
            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.Green;

            Func<string, (int X, int Y)> GetCoord = (chr) =>
            {
                var drawable = drawables.Find(x => x.Chars == chr);
                return (drawable.CoordinateX, drawable.CoordinateY);
            };
            Action<string> Clear = (clear) =>
            {
                Console.CursorVisible = false;
                foreach (char c in clear) Console.Write(" ");
                Console.CursorVisible = true;
            };

            var fCoord = GetCoord("F");
            var nameEndCoord = GetCoord(":");

            LineTools.SetCursor((nameEndCoord.X + 2, nameEndCoord.Y));
            var username = Console.ReadLine();

            LineTools.ClearAt(fCoord, "First name: " + username);
            Console.Write("Security question loading...");

            Func<string, string> getSecurityAnswer = (securityQuestion) =>
            {
                LineTools.ClearAt(fCoord, "Security question loading...");
                Console.Write(securityQuestion + " ");
                return Console.ReadLine();
            };

            var userExists = DatabaseManagement.AccountManagement.IdentifyWithQuestion(username, getSecurityAnswer);

            LineTools.ClearAt(fCoord, "Security question loading... plus the long answer that i cleared now!");

            if (userExists == false)
            {
                Console.WriteLine("Wrong answer");
                Thread.Sleep(500);
                return Option.StartScreen;
            }
            else
            {
                var registrationExists = DatabaseManagement.AccountManagement.Exists(username, false);
                if (registrationExists == false)
                { 
                    return Option.RegistrationScreen;
                }
                else
                {
                    LineTools.SetCursor(fCoord);
                    Console.WriteLine("User is already registered");
                    Thread.Sleep(500);
                    return Option.Login;
                }
            }
        }
    }
}
