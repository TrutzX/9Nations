using System;
using System.Collections.Generic;
using Libraries;
using Terrains;

namespace Nations
{
    [Serializable]
    public class Nation : BaseData
    {
        public string Leader;
        public string Townhall;
        public string ResearchElement;
        public string Terrain;
        public string Ethos;
        public string TownNameGenerator;
        public List<string> TownNameLevel;

        public Dictionary<string, string> Modi;
        // Start is called before the first frame update
        public Nation()
        {
            Modi = new Dictionary<string, string>();
            TownNameLevel = new List<string>();
        }
        
        public override void ShowDetail(PanelBuilder panel)
        {
            BTerrain terr = L.b.terrain[Terrain];
            base.ShowDetail(panel);
            panel.AddHeaderLabel("General");
            panel.AddImageLabel($"Home terrain: {terr.Name}", terr.Sprite());
            panel.AddModi("Modifiers",Modi);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
