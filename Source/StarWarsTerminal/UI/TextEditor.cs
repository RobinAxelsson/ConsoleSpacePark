using StarWarsApi.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StarWarsTerminal.UI
{
    public static class TextEditor
    {
        public static string[] ConvertIStarwarsItem(IStarwarsItem[] items)
        {
            var nameList = items.Select(x => '¤' + x.Name).ToList();
            int spacings = nameList.Count - 1;
            int insert = 1;
            for (int i = 0; i < spacings; i++)
            {
                nameList.Insert(insert, " ");
                insert += 2;
            }
            return nameList.ToArray();
        }
    }
}
