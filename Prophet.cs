using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Reflection;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExileCore.PoEMemory.Elements.InventoryElements;
using SharpDX;
using SharpDX.Direct3D;
using ExileCore.PoEMemory;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.Elements;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.Shared;
using ExileCore.Shared.Enums;
using ExileCore.Shared.Helpers;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using ExileCore;
using ImGuiNET;
using Util;
using Vector4 = System.Numerics.Vector4;
using Stack = ExileCore.PoEMemory.Components.Stack;

namespace Prophet
{


    public class Prophet : BaseSettingsPlugin<ProphetSettings>
    {
        private Vector2 _windowOffset;
        private const int PixelBorder = 3;
        Random _rnd = new Random();
        static string logfile = "";
        private const string MainDirectory = "prophetfiles";  //ordner
        private string Prophecies => DirectoryFullName + "\\" + "prophecies.txt";
        static int tickcounter = 0;
        static List<string> PropheciesList = new List<string>();




        //Prophet
        static bool started = false;
        static bool prophetstarted = false;





        public override bool Initialise()
        {
            ReadProphecies();

            return true;
        }




        public override void Render()
        {
            main();
        }




        public void main()
        {
                     

            if (!Keyboard.IsKeyToggled(Settings.StartProphetKey.Value))
            {
                //tickcounter = 0;

                if (started == true) //beenden
                {
                    started = false;
                }
                if (prophetstarted == true) //beenden
                {
                    prophetstarted = false;
                    LogMessage("Stopped Prophet", 5);
                }
                
                return;   //A toggelt jetzt an aus
            }

            //SniperMainLoop();
            ProphetMainLoop();


        }


        private void ProphetMainLoop()
        {
            if (prophetstarted == false)
            {
                prophetstarted = true;
                LogMessage("Prothet started", 5);

            }
            //

            ProphMainLoop();

        }
            


        private void ProphMainLoop()
        {
            GotProphecies();
            
            
        }

        private bool GotProphecies()
        {
            //LogMessage("runs", 1);
            var leftpanelopen = GameController.Game.IngameState.IngameUi.OpenLeftPanel.IsVisible;

            if (!leftpanelopen) return false;

            var ProphecyPanel = GameController.Game.IngameState.IngameUi.OpenLeftPanel.GetChildAtIndex(2)?.GetChildAtIndex(0)?.GetChildAtIndex(1)?.GetChildAtIndex(1)?.GetChildAtIndex(32)?.GetChildAtIndex(0)?.GetChildAtIndex(0);

            if ((ProphecyPanel == null) || (!ProphecyPanel.IsVisible)) return false;

            if (ProphecyPanel.ChildCount <= 0) return false;

            var foundprophs = new List<RectangleF>();


            foreach (Element element in ProphecyPanel.Children)
            {


                if (element == null)
                {
                    continue;
                }

                var textelement = element.GetChildAtIndex(0).GetChildAtIndex(1);
                var text = element.GetChildAtIndex(0).GetChildAtIndex(1).Text;

                if (PropheciesList.Contains(text))
                {

                    var drawRect = textelement.GetClientRect();
                    drawRect.Left -= 5;
                    drawRect.Right += 5;
                    drawRect.Top -= 5;
                    drawRect.Bottom += 5;
                    drawRect.X -= 5;
                    drawRect.Y -= 5;
                    foundprophs.Add(drawRect);
                }
                //if (element.ChildCount != 3) continue;  //kein invite wenn nicht 3 children; andrer nachricht - left party, etc


            }

            if (foundprophs.Count > 0)
            {
                DrawFrame(foundprophs, 1);
                return true;
            }
            else return false;
                       
        }

        private bool InHO()
        {
            if (GameController.Game.IngameState.Data.CurrentWorldArea.ToString().ToLower().Contains("hideout"))
            {
                return true;
            }
            else return false;
        }

        private void DrawFrame(List<RectangleF> goodProphs, int color)
        {
            Color farbe;
            if (color == 1) farbe = Color.Yellow;
            else farbe = Color.Yellow;

            foreach (var position in goodProphs)

            {
                RectangleF border = new RectangleF { X = position.X + 8, Y = position.Y + 8, Width = position.Width - 6, Height = position.Height - 6 };
                Graphics.DrawFrame(border, farbe, 3);
            }
        }

        private void ReadProphecies()
        {
            PropheciesList.Clear();

            if (File.Exists(Prophecies))
            {
                var lines = File.ReadAllLines(Prophecies);
                PropheciesList.AddRange(lines);
                LogMessage("Prophecies geladen", 5);
            }
            else
            {
                //die datei erstellen
                LogMessage("keine prophecies file da", 5);
            }

        }



    }
}
