using System;
using StarWarsApi.Models;
using StarWarsTerminal.UI;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using StarWarsApi.Interfaces;
using StarWarsApi.Networking;
using Newtonsoft.Json;
using StarWarsApi.Database;
using System.Linq;

namespace StarWarsTerminal.Main
{
    static partial class Program
    {
        public static void WelcomeScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/1.welcome-screen.txt");
            var welcomeText = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.AllUnitsInXDir(welcomeText, Console.WindowWidth);
            TextEditor.Center.InYDir(welcomeText, Console.WindowHeight);
            ConsoleWriter.TryAppend(welcomeText);
            ConsoleWriter.Update();
        }
        public static StartMenuOptions StartScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/2.menu.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            var selectionList = new SelectionList<StartMenuOptions>(ForegroundColor, '$');
            selectionList.GetCharPositions(drawables);
            selectionList.AddSelections(new StartMenuOptions[] { StartMenuOptions.Login, StartMenuOptions.NewAccount, StartMenuOptions.Exit });
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();
            return selectionList.GetSelection();
        }
        public static void IdentificationScreen()
        {
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
            }
            else
            {
                ConsoleWriter.ClearScreen();

                bool registrationExists = DatabaseManagement.AccountManagement.IsRegistrated();
                if (registrationExists == false)
                {
                    ConsoleWriter.ClearScreen();
                    var ship = ShipScreen();
                    DatabaseManagement.AccountManagement.Register(ship, RegistrationScreen);
                    ConsoleWriter.ClearScreen();
                    //Takes user and ship

                }
                else
                {
                    LineTools.SetCursor(fCoord);
                    Console.WriteLine("User is already registered");
                    Thread.Sleep(500);
                    ConsoleWriter.ClearScreen();
                    LoginPasswordScreen();
                }
            }
        }
        public static (string AccountName, string Password) RegistrationScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/4a.register-account.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.AllUnitsInXDir(drawables, Console.WindowWidth);
            TextEditor.Center.InYDir(drawables, Console.WindowHeight);
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();

            Func<IDrawable, (int X, int Y)> getCoord = (drawable) => (drawable.CoordinateX + 2, drawable.CoordinateY);

            var colons = drawables.FindAll(x => x.Chars == ":");

            var nameCoord = getCoord(colons[0]);
            var pass1Coord = getCoord(colons[1]);
            var pass2Coord = getCoord(colons[2]);

            Console.ForegroundColor = ConsoleColor.Green;
            LineTools.SetCursor(nameCoord);

            var nameLine = new InputLine(nameCoord.X, nameCoord.Y, 30, ConsoleColor.Green);
            var pass1Line = new InputLine(pass1Coord.X, pass1Coord.Y, 30, ConsoleColor.Green);
            var pass2Line = new InputLine(pass2Coord.X, pass2Coord.Y, 30, ConsoleColor.Green);

            var accountName = "";
            var password1 = "";
            var password2 = "";

            do
            {
                accountName = nameLine.GetInputString(false);
                password1 = pass1Line.GetInputString(true);
                password2 = pass2Line.GetInputString(true);

                LineTools.ClearAt(nameCoord, accountName);
                LineTools.ClearAt(pass1Coord, password1);
                LineTools.ClearAt(pass2Coord, password2);

            } while (password1 != password2 || accountName.Length <= 5 || password1.Length <= 5);

            return (accountName, password2);
        }
        public static SpaceShip ShipScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/5a.choose-your-ship.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            int nextLine = drawables.Max(x => x.CoordinateY);
            var localShips = GetLocalShips();
            string[] shipLines = localShips.Select(x => "$ " + x.Name).ToArray();
            drawables.AddRange(TextEditor.Add.DrawablesAt(shipLines, nextLine + 3));
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);

            var selectionList = new SelectionList<SpaceShip>(ForegroundColor, '$');
            selectionList.GetCharPositions(drawables);
            selectionList.AddSelections(localShips);
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();

            return selectionList.GetSelection();
        }
        public static (string AccountName, string Password) LoginPasswordScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/3b.login.txt");
            var loginText = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.AllUnitsInXDir(loginText, Console.WindowWidth);
            TextEditor.Center.InYDir(loginText, Console.WindowHeight);
            ConsoleWriter.TryAppend(loginText);
            ConsoleWriter.Update();
            return GetNamePass(loginText);
        }
        public static LoginMenuOptions AccountScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/6.logged-in-menu.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            var parameterCoords = drawables.FindAll(x => x.Chars == "¤").ToList().Select(x => (x.CoordinateX, x.CoordinateY)).ToList();

            var nameCoord = parameterCoords[0];
            var shipCoord = parameterCoords[1];

            var selectionList = new SelectionList<LoginMenuOptions>(ForegroundColor, '$');
            selectionList.GetCharPositions(drawables);
            selectionList.AddSelections(new LoginMenuOptions[] { LoginMenuOptions.Park, LoginMenuOptions.CheckReceipts, LoginMenuOptions.ReRegisterShip, LoginMenuOptions.GoToHomeplanet, LoginMenuOptions.Exit });
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();

            Console.ForegroundColor = ConsoleColor.Green;
            LineTools.SetCursor(nameCoord);
            Console.Write("Test Name");
            LineTools.SetCursor(shipCoord);
            Console.Write("Test Ship");

            return selectionList.GetSelection();
        }
        public static ParkingMenuOptions ParkingScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/6.logged-in-menu.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            var selectionList = new SelectionList<ParkingMenuOptions>(ForegroundColor, '$');
            selectionList.GetCharPositions(drawables);
            selectionList.AddSelections(new ParkingMenuOptions[] { ParkingMenuOptions.PurchaseTicket, ParkingMenuOptions.ReEnterhours, ParkingMenuOptions.BackToLogin });
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();
            return selectionList.GetSelection();
        }
        public static void ExitScreen()
        {
            string[] lines = File.ReadAllLines(@"UI/TextFrames/8.exit-screen.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);
            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();
            Thread.Sleep(2000);
            Environment.Exit(0);
        }
    }
}