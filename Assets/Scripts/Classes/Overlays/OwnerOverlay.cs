using Game;
using Maps.TileMaps;
using Players;
using Tools;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Classes.Overlays
{
    public class OwnerOverlay : BaseOverlay
    {
        private Tile Tile(Player player)
        {
            return GameMgmt.Get().newMap.tools.GetTile(player.Coat().flag16);
        }

        protected override void SetTile(TileMapConfig16 tile, NVector pos)
        {
            var b = S.Building().At(pos);

            if (b == null)
            {
                return;
            }
            
            //todo performance?
            tile.GetComponent<Tilemap>().SetTile(new Vector3Int(pos.x * 2 + 1, pos.y * 2, 0), Tile(b.Player()));
        }

        public override string ID()
        {
            return "owner";
        }
        
    }
}