using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsApi.Interfaces
{
    public interface IStarwarsItem
    {
        public string Name { get; set; }
        public string URL { get; set; }
    }
}
