using System;
using System.Linq;
using Game;
using Libraries;
using Libraries.Terrains;
using Maps.GameMaps;
using Maps.TileMaps;
using Players;
using Tools;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Classes.Overlays
{
    public class FrontierOverlay : BaseOverlay, IDataLevel
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
            Player player = S.Player().OverlayHighest(ID(), pos);
            if (player == null) return;
            
            tile.SetTile(this, new Vector3Int(pos.x, pos.y, 0), _frontier, false, player.Coat().color);
            
            
        }

        public override string ID()
        {
            return "frontier";
        }

        public int At(Vector3Int pos, bool winter = false)
        {
            Player player = S.Player().OverlayHighest(ID(), new NVector(pos.x, pos.y, _pos.level));
            return player?.id ?? -1;
        }
    }
}