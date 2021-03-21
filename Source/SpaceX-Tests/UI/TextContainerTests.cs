using StarWarsTerminal.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace SpaceX_Tests.UI
{
    public class TextContainerTests
    {
        [Fact]
        public void GroupByY_ExpectTrue()
        {
            string[] testLines = new[]
            {
                "---",
                "---",
                "   ",
                "---",
                "---"
            };
            var cont = new TextContainer(testLines, ConsoleColor.Green);
            var grouped = cont.Drawables.GroupBy(x => x.CoordinateY);
            var groupList = grouped.ToList();
            int counted = groupList.Count;

            Assert.True(counted == 4);
        }
        [Fact]
        public void GroupByX_ExpectTrue()
        {
            string[] testLines = new[]
            {
                "---",
                "---",
                "   ",
                "---",
                "---"
            };
            var cont = new TextContainer(testLines, ConsoleColor.Green);
            var grouped = cont.Drawables.GroupBy(x => x.CoordinateX);
            var groupList = grouped.ToList();
            int counted = groupList.Count;

            Assert.True(counted == 3);
        }
        [Fact]
        public void GroupBySelect_ExpectTrue()
        {
            string[] testLines = new[]
            {
                "-",
                "---",
                "   ",
                "---",
                "---"
            };
            var cont = new TextContainer(testLines, ConsoleColor.Green);
            var grouped = cont.Drawables.GroupBy(x => x.CoordinateY);
            var groupList = grouped.ToList();
            groupList = groupList.Where(x => x.Key == 0).ToList();
            int counted = groupList.Count;
            
            Assert.True(counted == 1);
        }
        [Fact]
        public void GroupWhereY_ExpectTrue()
        {
            string[] testLines = new[]
            {
                "-",
                "---",
                "   ",
                "---",
                "---"
            };
            var cont = new TextContainer(testLines, ConsoleColor.Green);
            var units = cont.Drawables.Where(x => x.CoordinateY >= 0 && x.CoordinateY <= 1).ToList();
            int counted = units.Count;

            Assert.True(counted == 4);
        }
        [Fact]
        public void xMax_ExpectTrue()
        {
            string[] testLines = new[]
            {
                "-",
                "---",
                "   ",
                "----",
                "---"
            };
            var cont = new TextContainer(testLines, ConsoleColor.Green);

            int xMax = cont.Drawables.Max(x => x.CoordinateX);
            Assert.True(xMax == 3);
        }
        [Fact]
        public void xRightStep_ExpectTrue()
        {
            int xMax = 10;
            int rightStep = (15 - xMax) / 2;
            
            Assert.True(rightStep == 2);
        }
        [Fact]
        public void xRightStep2_ExpectTrue()
        {
            string[] testLines = new[]
            {
                "-",
                "---",
                "   ",
                "----",
                "---"
            };
            var textContainer= new TextContainer(testLines, ConsoleColor.Green);

            textContainer.CenterInXDirection(textContainer.Drawables, 10);
            int x = textContainer.Drawables[0].CoordinateX;
            Assert.True(x == 3);
        }

        [Fact]
        public void xRightStep3_center_ExpectTrue()
        {
            string[] testLines = new[]
            {
                "o--",
                "o--"
            };
            var textContainer = new TextContainer(testLines, ConsoleColor.Green);

            textContainer.CenterInXDirection(textContainer.Drawables, 5);
            var drawable = textContainer.Drawables.Find(x => x.Chars == "o");
            int x = drawable.CoordinateX;
            Assert.True(x == 1);
        }
        [Fact]
        public void xRightStep_TestFree_ExpectTrue()
        {
            string[] testLines = new[]
            {
                "o-",
                "   ",
                "p---",
                "q---",
                "  ",
                "  ",
                "l--"
            };
            var textContainer = new TextContainer(testLines, ConsoleColor.Green);
            textContainer.CenterUnitsXDir(6);
            var q = textContainer.Drawables.Find(x => x.Chars == "q");
            int x = q.CoordinateX;
            Assert.True(x == 1);
        }
        [Fact]
        public void xRightStep_CenterUnits_ExpectTrue()
        {
            string[] testLines = new[]
            {
                "o-",
                "   ",
                "p---",
                "q---",
                "  ",
                "  ",
                "l---"
            };
            var textContainer = new TextContainer(testLines, ConsoleColor.Green);
            textContainer.CenterUnitsXDir(screenWidth: 6);
            var l = textContainer.Drawables.Find(x => x.Chars == "l");
            int x = l.CoordinateX;
            Assert.True(x == 1);
        }
        [Fact]
        public void Add_Spacing_ExpectTrue()
        {
            string[] testLines = new[]
            {
                "o-",
                //
                "p---",
                //
                "q---",
                //
                "l---"
            };
            int spacings = testLines.Length - 1;
            var list = testLines.ToList();
            int insert = 1;
            for (int i = 0; i < spacings; i++)
            {
                list.Insert(insert, " ");
                insert += 2;
            }
            Assert.True(list.Count == 7);
        }
    }
}
