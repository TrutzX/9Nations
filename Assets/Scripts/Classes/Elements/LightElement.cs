using Game;
using Libraries;
using Libraries.Elements;
using Libraries.Terrains;
using Maps;
using Maps.GameMaps;
using Players;
using Tools;
using UI;
using UnityEngine;

namespace Classes.Elements
{
    public class LightElement : BaseElementRun
    {
        public LightElement() : this ("light") {}
        public LightElement(string id) : base (id) {}
        public override void Develop(Player player)
        {
            //produce a new unit?
            GameMgmt.Get().unit.Create(player.id, id, CreateNewSpot()).SetActive();
        }

        protected virtual NVector CreateNewSpot()
        {
            GameMapData gmap = GameMgmt.Get().data.map;
            GameMapDataLevel gmdl = GameMgmt.Get().data.map.levels[gmap.standard];
            
            int i = 0;
            while (i < 1000)
            {
                i++;
                int x = Random.Range(0, gmap.width);
                int y = Random.Range(0, gmap.height);
                DataTerrain t = gmdl.Terrain(x, y);
            
                if (t.MoveCost("walk") == 0 || t.MoveCost("walk") > 10) continue; // can walk?
                if (!gmdl.TerrainNear(x, y, "water", 2)) continue; //near water?
                if (!S.Unit().Free(new NVector(x,y,gmap.standard))) continue; //near water?
                
                return new NVector(x,y,gmap.standard);
            }

            NVector pos = new NVector(Random.Range(0, gmap.width), Random.Range(0, gmap.height), gmap.standard);
            Debug.LogError($"Can not find a start position using {pos}");
            return pos;
        }
        
        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabel("Free unit on development");
            L.b.units[id].AddImageLabel(panel);
        }
    }
}