using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsTerminal.UI
{
    public class Screen
    {
        public List<IFrame> Frames { get; set; } = new List<IFrame>();
        public int GetFramesTotalHeight()
            => Frames.Select(x => x.GetHeight()).Sum();
        public Screen(List<IFrame> frames)
        {
            Frames = frames;
        }

        public void Clear()
        {
            foreach (var frame in Frames)
            {
                frame.EraseFrame();
            }
        }
    }
}
