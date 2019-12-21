using System.Collections.Generic;
using Libraries;
using Maps;
using UI;
using UI.Show;
using UnityEngine;

namespace Endless
{
    public class MapSplitTab : SplitElementTab
    {
        public MapSplitTab(Dictionary<string, string> startConfig) : base("Map", "map", "Next & set")
        {
            foreach (NMap map in L.b.maps.Values())
            {
                Add(new MapSelectSplitElement(map, this, startConfig));
            }
            
            Add(new MapMoreSplitElement());
        }
    }
    
    public class MapSelectSplitElement : SplitElement
    {
        private NMap _map;
        private Dictionary<string, string> _startConfig;
        private MapSplitTab _tab;
        
        public MapSelectSplitElement(NMap map, MapSplitTab tab, Dictionary<string, string> startConfig) : base(map.Name, map.Sprite())
        {
            _map = map;
            _startConfig = startConfig;
            _tab = tab;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            _map.ShowDetail(panel);
        }

        public override void Perform()
        {
            _startConfig["map"] = _map.Id;
            _tab.window.ShowTab(1);
        }
    }
    
    public class MapMoreSplitElement : SplitElement
    {
        public MapMoreSplitElement() : base("More maps...", "map")
        {
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabel("More maps");
            panel.AddLabel("You can find more maps in the mod section");
        }

        public override void Perform()
        {
            GameButtonHelper.Call(Data.gameButton.mod.id,null);
        }
    }
}