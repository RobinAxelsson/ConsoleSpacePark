using StarWarsTerminal.Main;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StarWarsTerminal.Main.Program;

namespace StarWarsTerminal.UI.Screen
{
    public static partial class Screen
    {
        public static Option HomePlanetScreen()
        {
            ConsoleWriter.ClearScreen();
            string[] lines = File.ReadAllLines(@"UI/TextFrames/7.homeplanet-info.txt");
            var drawables = TextEditor.Add.DrawablesAt(lines, 0);
            TextEditor.Center.ToScreen(drawables, Console.WindowWidth, Console.WindowHeight);

            var drawProps = drawables.FindAll(x => x.Chars == "¤");
            drawables = drawables.Except(drawProps).ToList();

            var props = drawProps.Select(x => (x.CoordinateX, x.CoordinateY)).ToList();
            var home = new LineData(props[0]);
            var rotation = new LineData(props[1]);
            var orbital = new LineData(props[2]);
            var diameter = new LineData(props[3]);
            var climate = new LineData(props[4]);
            var pop = new LineData(props[5]);

            var homeplanet = _account.User.Homeplanet;
            Console.ForegroundColor = ConsoleColor.Green;
            home.Update(homeplanet.Name);
            rotation.Update(homeplanet.RotationPeriod);
            orbital.Update(homeplanet.OrbitalPeriod);
            diameter.Update(homeplanet.Diameter);
            climate.Update(homeplanet.Climate);
            pop.Update(homeplanet.Population);

            ConsoleWriter.TryAppend(drawables);
            ConsoleWriter.Update();

            Console.ForegroundColor = ConsoleColor.Green;


           

            Console.ReadLine();
            return Option.GotoAccount;
        }
    }
}
