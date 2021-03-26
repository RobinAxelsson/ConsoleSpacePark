using System.Linq;
using Xunit;

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
    }
}