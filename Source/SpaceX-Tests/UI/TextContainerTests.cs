using StarWarsTerminal.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace SpaceX_Tests.UI
{
    public class TextEditorTests
    {
    
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
