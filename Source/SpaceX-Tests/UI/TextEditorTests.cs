using System;
using System.Linq;
using Xunit;
using StarWarsTerminal.UI;

namespace SpaceX_Tests.UI
{
    public class TextEditorTests
    {
        [Fact]
        public void Add_Spacing_ExpectTrue()
        {
            string[] testLines =
            {
                "o-",
                //
                "p---",
                //
                "q---",
                //
                "l---"
            };
            var spacings = testLines.Length - 1;
            var list = testLines.ToList();
            var insert = 1;
            for (var i = 0; i < spacings; i++)
            {
                list.Insert(insert, " ");
                insert += 2;
            }

            Assert.True(list.Count == 7);
        }
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

            var cont = TextEditor.Add.DrawablesAt(testLines, 0);
            var groups = TextEditor.Units.GetRowUnits(cont);

            Assert.True(groups.Count == 2);
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
            var cont = TextEditor.Add.DrawablesAt(testLines, 0);
            var blankRows = TextEditor.Units.GetBlankRows(cont);

            Assert.True(blankRows[0] == 2);
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
            var cont = TextEditor.Add.DrawablesAt(testLines, 0);
            var drawables = cont.FindAll(x => x.CoordinateY == 0);

            Assert.True(drawables.Count == 1);
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
            var cont = TextEditor.Add.DrawablesAt(testLines, 0);
            var drawables = cont.FindAll(x => x.CoordinateY == 3 || x.CoordinateY== 4);

            Assert.True(drawables.Count == 6);
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
            var cont = TextEditor.Add.DrawablesAt(testLines, 0);
            int xMax = cont.Max(x => x.CoordinateX);
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
            var cont = TextEditor.Add.DrawablesAt(testLines, 0);

            TextEditor.Center.InXDir(cont, 10);
            int x = cont[0].CoordinateX;
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
            var cont = TextEditor.Add.DrawablesAt(testLines, 0);

            TextEditor.Center.InXDir(cont, 5);
            var drawable = cont.Find(x => x.Chars == "o");
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
            var cont = TextEditor.Add.DrawablesAt(testLines, 0);
            TextEditor.Center.InXDir(cont, 6);
            var q = cont.Find(x => x.Chars == "q");
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
            var cont = TextEditor.Add.DrawablesAt(testLines, 0);
            TextEditor.Center.InXDir(cont, 6);
            var l = cont.Find(x => x.Chars == "l");
            int x = l.CoordinateX;
            Assert.True(x == 1);
        }
        [Fact]
        public void xRightStep_CenterAllUnits_ExpectTrue()
        {
            string[] testLines = new[]
            {
                "o-",
                "   ",
                "p-",
                "q-------",
                "  ",
                "  ",
                "l------"
            };
            var cont = TextEditor.Add.DrawablesAt(testLines, 0);
            TextEditor.Center.AllUnitsInXDir(cont, 8);
            var q = cont.Find(x => x.Chars == "q");
            int x = q.CoordinateX;
            Assert.True(x == 0);
        }
        [Fact]
        public void xRightStep_CenterAllUnits_p_ExpectTrue()
        {
            string[] testLines = new[]
            {
                "o-",
                "   ",
                "p-",
                "q-------",
                "  ",
                "  ",
                "l------"
            };
            var cont = TextEditor.Add.DrawablesAt(testLines, 0);
            TextEditor.Center.AllUnitsInXDir(cont, 8);
            var p = cont.Find(x => x.Chars == "p");
            int x = p.CoordinateX;
            Assert.True(x == 0);
        }
        [Fact]
        public void xRightStep_CenterAllUnits_o_ExpectTrue()
        {
            string[] testLines = new[]
            {
                "o-",
                "   ",
                "p-",
                "q-------",
                "  ",
                "  ",
                "l------"
            };
            var cont = TextEditor.Add.DrawablesAt(testLines, 0);
            TextEditor.Center.AllUnitsInXDir(cont, 8);
            var o = cont.Find(x => x.Chars == "o");
            int x = o.CoordinateX;
            Assert.True(x == 3);
        }
    }
}