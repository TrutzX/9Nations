using System;
using System.Collections.Generic;
using Game;
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
            panel.AddHeaderLabelT("general");
            panel.AddImageLabel($"Home terrain: {terr.Name()}", terr.Sprite());
            panel.AddModi(Modi);
            if (elements.Count > 0)
            {
                panel.AddHeaderLabel(S.T(L.b.elements.Id(),elements.Count));
                foreach (var element in elements)
                {
                    L.b.elements[element].AddImageLabel(panel);
                }
            }
        }
    }
}
