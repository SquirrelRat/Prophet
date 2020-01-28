using System.Windows.Forms;
using System.Collections.Generic;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using ExileCore.Shared.Attributes;
using SharpDX;

namespace Prophet
{
    public class ProphetSettings : ISettings
    {

        public ProphetSettings()
        {
            Enable = new ToggleNode(false);

            ColorGood = new ColorBGRA(0, 255, 0, 255);

            ColorTrash = new ColorBGRA(0, 0, 255, 255);
        }

        [Menu("Menu Settings", 100)]
        public EmptyNode Settings { get; set; }

        [Menu("Enable", parentIndex = 100, Tooltip = "Enable Prophet")]
        public ToggleNode Enable { get; set; } 

        [Menu("Color good prophecy", parentIndex = 100, Tooltip = "Display color of border good prophecy")]
        public ColorNode ColorGood { get; set; }

        [Menu("Color trash prophecy", parentIndex = 100, Tooltip = "Display color of border trash prophecy")]
        public ColorNode ColorTrash { get; set; }


    }
}
