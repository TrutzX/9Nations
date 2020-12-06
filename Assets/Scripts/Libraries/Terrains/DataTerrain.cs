using System;
using System.Collections.Generic;
using Game;
using Libraries.Movements;
using MapElements;
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
        public Dictionary<string, int> movement;
        public Dictionary<string, string> res;

        public DataTerrain()
        {
            defaultTile = -1;
            modi = new Dictionary<string, string>();
            res = new Dictionary<string, string>();
            movement = new Dictionary<string, int>();
        }
        
        public override void ShowLexicon(PanelBuilder panel)
        {
            ShowLexicon(panel, null, null);
        }
        
        public override void ShowLexicon(PanelBuilder panel, MapElementInfo onMap, NVector pos)
        {
            base.ShowLexicon(panel);
            if (!string.IsNullOrEmpty(winter))
            {
                DataTerrain w = L.b.terrains[winter];
                panel.AddSubLabel(S.T("terrainPassableWinter"),w.Name(), w.Sprite());
            }

            panel.AddHeaderLabelT("move");
            foreach (Movement m in L.b.movements.Values())
            {
                int costO = MoveCost(m.id);
                int cost = GameMgmt.Get().newMap.PathFinding(pos.level).CostNode(S.ActPlayer(), m.id, pos);

                var mess = S.T("terrainPassable", cost==0?S.T("terrainPassableNot"):S.T("terrainPassableAP",cost), cost == costO ? "" : S.T("terrainPassableOrg",costO==0?S.T("terrainPassableNot"):S.T("terrainPassableAP",costO)));
                panel.AddSubLabel(m.Name(),mess,m.Icon);
            }
            //if (movement.Count == 0) panel.AddImageLabel(S.T("terrainPassableNot"),"no");
            panel.AddModi(modi);

            ShowRes(panel, S.ActPlayer(), pos);
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
            
            if (res.Count > 0)
            {
                panel.AddHeaderLabelT("resInclude");
                foreach (KeyValuePair<string, string> r in res)
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
            return movement.ContainsKey(moveTyp)?movement[moveTyp]:L.b.movements[moveTyp].def;
        }

        public (int min, int max) ResRange(string res)
        {
            if (!this.res.ContainsKey(res))
            {
                return (0, 0);
            }
            
            string[] c = SplitHelper.Separator(this.res[res])[0].Split('-');
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
            if (!this.res.ContainsKey(res))
            {
                return 0;
            }
            
            string[] s = SplitHelper.Separator(this.res[res]);

            if (s.Length >= 2)
            {
                return (int) ConvertHelper.Proc(s[1]);
            }

            return 0;
        }

        public bool Passable()
        {
            foreach (var move in L.b.movements.Values())
            {
                if ((movement.ContainsKey(move.id) ? movement[move.id]: move.def) > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}