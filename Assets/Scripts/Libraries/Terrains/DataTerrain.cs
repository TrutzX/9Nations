using System;
using System.Collections.Generic;
using Game;
using Libraries.Movements;
using Tools;
using UI;

namespace Libraries.Terrains
{
    [Serializable]
    public class DataTerrain : BaseData
    {
        public int defaultTile;
        public string winter;
        public string category;
        public Dictionary<string, string> modi;
        public Dictionary<string, int> Movement;
        public Dictionary<string, string> Res;

        public DataTerrain()
        {
            defaultTile = -1;
            modi = new Dictionary<string, string>();
            Res = new Dictionary<string, string>();
            Movement = new Dictionary<string, int>();
        }
        
        public override void ShowLexicon(PanelBuilder panel)
        {
            base.ShowLexicon(panel);
            if (!string.IsNullOrEmpty(winter))
            {
                DataTerrain w = L.b.terrain[winter];
                panel.AddImageLabel($"Winter: {w.name}", w.Sprite());
            }

            panel.AddHeaderLabel("Movement cost");
            foreach (Movement m in L.b.movements.Values())
            {
                panel.AddSubLabel(m.name,(MoveCost(m.id)==0?"Not passable":$"{MoveCost(m.id)} AP"),m.Icon);
            }
            if (Movement.Count == 0) panel.AddImageLabel("Not passable","no");
            panel.AddModi("Modifiers",modi);
        }
        
        public void ShowField(PanelBuilder panel, NVector pos)
        {
            ShowLexicon(panel);
            
            //addHeader
            if (pos != null && GameMgmt.Get().data.map.levels[pos.level].ResGenKey(pos.x, pos.y) != null)
            {
                panel.AddHeaderLabel("Resources");
                foreach (string key in GameMgmt.Get().data.map.levels[pos.level].ResGenKey(pos.x, pos.y))
                {
                    panel.AddImageLabel($"{L.b.terrain.GenDesc(GameMgmt.Get().data.map.ResGen(pos,key))}x {L.b.res[key].name}", L.b.res[key].Icon);
                }
                
            } else if (Res.Count > 0)
            {
                panel.AddHeaderLabel("Generate Resources");
                foreach (KeyValuePair<string, string> r in Res)
                {
                    string[] c = r.Value.Split('-');
                    panel.AddImageLabel($"{L.b.terrain.GenDesc(Int32.Parse(c[0]))}-{L.b.terrain.GenDesc(Int32.Parse(c[1]))}x {L.b.res[r.Key].name}", L.b.res[r.Key].Icon);
                }
            }
        }
        
        public int MoveCost(string moveTyp)
        {
            return Movement.ContainsKey(moveTyp)?Movement[moveTyp]:L.b.movements[moveTyp].def;
        }
    }
}