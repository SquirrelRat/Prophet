using System.Windows.Forms;
using System.Collections.Generic;
using Prophet.Stashie;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using ExileCore.Shared.Attributes;

namespace Prophet
{
    public class ProphetSettings : ISettings
    {
        public Dictionary<string, ListIndexNode> CustomFilterOptions = new Dictionary<string, ListIndexNode>();
        public List<string> AllStashNames = new List<string>();


        [Menu("General", 101)]
        public EmptyNode General { get; set; } = new EmptyNode();

        [Menu("Enable Prophet", "yololo", 1001, 101)]
        public ToggleNode Enable { get; set; } = new ToggleNode(false);

        [Menu("Main Key", "start/stop everything", 1002, 101)]
        public HotkeyNode StartProphetKey { get; set; } = Keys.Pause;



       

    }
}
