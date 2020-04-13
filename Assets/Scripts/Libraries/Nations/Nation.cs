using System;
using System.Collections.Generic;
using Libraries.Terrains;
using UI;

namespace Libraries.Nations
{
    [Serializable]
    public class Nation : BaseData
    {
        public string Terrain;
        public string Ethos;
        public string TownNameGenerator;
        public List<string> TownNameLevel;
        public List<string> elements;
        public int maxElement;
        public Dictionary<string, string> Modi;
        // Start is called before the first frame update
        public Nation()
        {
            Modi = new Dictionary<string, string>();
            TownNameLevel = new List<string>();
            elements = new List<string>();
        }
        
        public override void ShowLexicon(PanelBuilder panel)
        {
            DataTerrain terr = L.b.terrains[Terrain];
            base.ShowLexicon(panel);
            panel.AddHeaderLabel("General");
            panel.AddImageLabel($"Home terrain: {terr.name}", terr.Sprite());
            panel.AddModi("Modifiers",Modi);
            if (elements.Count > 0)
            {
                panel.AddHeaderLabel("Elements");
                foreach (var element in elements)
                {
                    L.b.elements[element].AddImageLabel(panel);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
