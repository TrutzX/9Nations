using Game;
using Libraries;
using Maps.TileMaps;
using Players;
using Tools;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Classes.Overlays
{
    public class ResOverlay : BaseOverlay
    {
        protected override void SetTile(TileMapConfig16 tile, NVector pos)
        {
            if (PlayerMgmt.ActPlayer().overlay.Get("res", pos)==0)
                return;
            
            int align = 0;
            var rgk = GameMgmt.Get().data.map.levels[pos.level].ResGenKey(pos.x, pos.y);
            
            if (rgk == null)
                return;
            
            foreach (string key in rgk)
            {
                int act = GameMgmt.Get().data.map.ResGen(pos, key);
                var res = L.b.res[key];
                
                //nothing to display?
                if (res.overlay.Count == 0 || act == 0)
                    continue;

                string found = null;
                //find id
                foreach (var o in res.overlay)
                {
                    var sp = SplitHelper.DelimiterInt(o);
                    if (sp.value > act)
                        break;

                    found = sp.key;
                }

                //show it
                Tile t = GameMgmt.Get().newMap.tools.GetTile(found);
                tile.GetComponent<Tilemap>().SetTile(new Vector3Int(pos.x * 2 + (align%2), pos.y * 2 + (align/2), 0), t);
                
                align++;

                //full?
                if (align >= 4)
                {
                    return;
                }
            }
        }

        public override string ID()
        {
            return "res";
        }
        
    }
}