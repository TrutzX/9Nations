using Game;
using Maps.TileMaps;
using Players;
using Tools;
using UnityEngine;

namespace Classes.Overlays
{
    public abstract class BaseOverlay
    {
        public virtual void Run(Player player, TileMapConfig16 tile)
        {
            int level = S.Map().view.ActiveLevel;
            for (int x = 0; x < GameMgmt.Get().data.map.width; x++)
            {
                for (int y = 0; y < GameMgmt.Get().data.map.height; y++)
                {
                    NVector nv = new NVector(x, y, level);
                    if (player.fog.Visible(nv))
                        SetTile(tile, nv);
                }
            }
        }

        protected abstract void SetTile(TileMapConfig16 tile, NVector pos);
        public abstract string ID();
    }
}