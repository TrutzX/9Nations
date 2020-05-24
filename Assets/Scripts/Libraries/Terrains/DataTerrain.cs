using System;
using System.Collections.Generic;
using Game;
using Libraries.Movements;
using Players;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Libraries.Terrains
{
    [Serializable]
    public class DataTerrain : BaseData
    {
        public int defaultTile;
        public string winter;
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
            this.ShowField(panel, null, null);
        }
        
        public void ShowField(PanelBuilder panel, Player player, NVector pos)
        {
            base.ShowLexicon(panel);
            if (!string.IsNullOrEmpty(winter))
            {
                DataTerrain w = L.b.terrains[winter];
                panel.AddImageLabel($"Winter: {w.Name()}", w.Sprite());
            }

            panel.AddHeaderLabel("Movement cost");
            foreach (Movement m in L.b.movements.Values())
            {
                panel.AddSubLabel(m.Name(),(MoveCost(m.id)==0?"Not passable":$"{MoveCost(m.id)} AP"),m.Icon);
            }
            if (Movement.Count == 0) panel.AddImageLabel("Not passable","no");
            panel.AddModi(modi);

            ShowRes(panel, player, pos);
        }

        public void ShowRes(PanelBuilder panel, Player player, NVector pos)
        {
            //addHeader
            if (pos != null && GameMgmt.Get().data.map.levels[pos.level].ResGenKey(pos.x, pos.y) != null && player != null && player.overlay.Get("res",pos)==1)
            {
                panel.AddHeaderLabelT("res");
                foreach (string key in GameMgmt.Get().data.map.levels[pos.level].ResGenKey(pos.x, pos.y))
                {
                    if (S.Debug())
                    {
                        panel.AddHeaderLabel(key);
                        panel.AddInput(key, GameMgmt.Get().data.map.ResGen(pos, key),
                            val => GameMgmt.Get().data.map.ResGenAdd(pos, key, val-GameMgmt.Get().data.map.ResGen(pos, key)));
                        continue;
                    }
                    L.b.res[key].AddImageLabel(panel, L.b.terrains.GenDesc(GameMgmt.Get().data.map.ResGen(pos,key)));
                }
                return;
            } 
            
            if (Res.Count > 0)
            {
                panel.AddHeaderLabelT("resInclude");
                foreach (KeyValuePair<string, string> r in Res)
                {
                    int chanc = ResChance(r.Key);
                    string chance = chanc >= 1 ? $"{chanc}% chance: " : "";
                    var c = ResRange(r.Key);
                    L.b.res[r.Key].AddImageLabel(panel, chance+$"{L.b.terrains.GenDesc(c.min)}-{L.b.terrains.GenDesc(c.max)}x {L.b.res[r.Key].Name()}");
                }
            }
        }
        
        public int MoveCost(string moveTyp)
        {
            return Movement.ContainsKey(moveTyp)?Movement[moveTyp]:L.b.movements[moveTyp].def;
        }

        public (int min, int max) ResRange(string res)
        {
            if (!Res.ContainsKey(res))
            {
                return (0, 0);
            }
            
            string[] c = SplitHelper.Separator(Res[res])[0].Split('-');
            return (ConvertHelper.Int(c[0]), ConvertHelper.Int(c[1]));
        }

        public Tile Tile(int id, Color color)
        {
            var t = GameMgmt.Get().newMap.tools.GetTile(Icon.Replace("4",id.ToString()), color.ToString());
            t.color = color;
            return t;
        }
        
        public int ResChance(string res)
        {
            if (!Res.ContainsKey(res))
            {
                return 0;
            }
            
            string[] s = SplitHelper.Separator(Res[res]);

            if (s.Length >= 2)
            {
                return (int) ConvertHelper.Proc(s[1]);
            }

            return 0;
        }
    }
}