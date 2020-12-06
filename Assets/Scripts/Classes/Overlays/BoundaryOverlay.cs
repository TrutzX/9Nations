using System;
using System.Linq;
using Game;
using Libraries;
using Libraries.Terrains;
using Maps.GameMaps;
using Maps.TileMaps;
using Players;
using Tools;
using Towns;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Classes.Overlays
{
    public class BoundaryOverlay : BaseOverlay, IDataLevel
    {
        private DataTerrain _frontier;
        private NVector _pos;
        
        public override void Run(Player player, TileMapConfig16 tile)
        {
            _frontier = L.b.terrains["frontier"];
            base.Run(player, tile);
        }
        protected override void SetTile(TileMapConfig16 tile, NVector pos)
        {
            this._pos = pos;
            //how has it?
            Town town = S.Towns().OverlayHighest(ID(), pos);
            if (town == null) return;
            
            tile.SetTile(this, new Vector3Int(pos.x, pos.y, 0), _frontier, false, town.Coat().color);
        }

        public override string ID()
        {
            return "boundary";
        }

        public int At(Vector3Int pos, bool winter = false)
        {
            Town town = S.Towns().OverlayHighest(ID(), new NVector(pos.x, pos.y, _pos.level));
            return town?.id ?? -1;
        }
    }
}