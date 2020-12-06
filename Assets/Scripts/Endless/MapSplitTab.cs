using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Libraries;
using Libraries.Maps;
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
            foreach (DataMap map in LSys.tem.maps.Values())
            {
                if (!map.req.Check(null))
                    continue;
                Add(new MapSelectSplitElement(map, this, startConfig));

                if (!startConfig.ContainsKey("map"))
                    startConfig["map"] = map.id;
            }
            
            Add(new MapMoreSplitElement());
        }
    }
    
    public class MapSelectSplitElement : SplitElement
    {
        private readonly DataMap _map;
        private readonly Dictionary<string, string> _startConfig;
        private readonly MapSplitTab _tab;
        
        public MapSelectSplitElement(DataMap map, MapSplitTab tab, Dictionary<string, string> startConfig) : base(map.Name(), map.Sprite())
        {
            _map = map;
            _startConfig = startConfig;
            _tab = tab;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            _map.ShowLexicon(panel);
        }

        public override void Perform()
        {
            _startConfig["map"] = _map.id;
            _tab.window.ShowTab(1);
        }
    }
    
    public class MapMoreSplitElement : SplitElement
    {
        public MapMoreSplitElement() : base("More maps...", "map") { }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabel("More maps");
            panel.AddLabel("You can find more maps in the mod section");
        }

        public override void Perform()
        {
            L.b.gameButtons["mod"].Call(null);
        }
    }
}