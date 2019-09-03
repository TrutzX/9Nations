using System;
using System.IO;
using DefaultNamespace;
using Game;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Players
{
    [Serializable]
    public class PlayerFog
    {
        public bool[,] visible;
        [NonSerialized] public Tilemap tilemap;

        /// <summary>
        /// Create all 
        /// </summary>
        /// <param name="pid"></param>
        public void Init(int pid)
        {
            visible = new bool[GameMgmt.Get().data.mapwidth,GameMgmt.Get().data.mapheight];

            AfterLoad(pid);
        }

        public void AfterLoad(int pid)
        {
            tilemap = MapMgmt.Get().CreateLayer($"Fog of war {pid}");
            
            //paint everything
            for (int x = 0; x < visible.GetLength(0); x++)
            {
                for (int y = 0; y < visible.GetLength(1); y++)
                {
                    bool d = visible[x,y];
                    //skip?
                    if (d)
                    {
                        continue;
                    }
                    
                    //set it
                    setTile(x,y,MapMgmt.Get().fog);
                    
                    //set border
                    if (x == 0)
                    {
                        setTile(-1,y,MapMgmt.Get().fog);
                    }
                    if (x == visible.GetLength(0))
                    {
                        setTile(visible.GetLength(0),y,MapMgmt.Get().fog);
                    }
                    if (y == 0)
                    {
                        setTile(x,-1,MapMgmt.Get().fog);
                    }
                    if (y == visible.GetLength(1))
                    {
                        setTile(x,visible.GetLength(1),MapMgmt.Get().fog);
                    }
                }
            }
            
            FinishRound();
        }

        public void Clear(int x, int y)
        {
            //check koor
            if (!MapMgmt.Valide(x, y))
            {
                return;
            }

            if (visible[x, y])
            {
                return;
            }
            
            setTile(x, y);

            visible[x, y] = true;
        }

        private void setTile(int x, int y, TileBase tile = null)
        {
            //set it
            tilemap.SetTile(new Vector3Int(x * 2, y * 2, 0), tile);
            tilemap.SetTile(new Vector3Int(x * 2 + 1, y * 2, 0), tile);
            tilemap.SetTile(new Vector3Int(x * 2, y * 2 + 1, 0), tile);
            tilemap.SetTile(new Vector3Int(x * 2 + 1, y * 2 + 1, 0), tile);
        }

        public void Clear(int x, int y, int radius)
        {
            radius = TerrainHelper.GetViewRadius(x, y, radius);
            //Debug.Log($"Clear {x},{y} for {radius}");
            
            if (radius >= 0)
            {
                Clear(x,y);
            }
            
            if (radius >= 1)
            {
                Clear(x-1,y);
                Clear(x+1,y);
                Clear(x,y-1);
                Clear(x,y+1);
            }
            
            if (radius >= 2)
            {
                Clear(x-1,y-1);
                Clear(x+1,y-1);
                Clear(x-1,y+1);
                Clear(x+1,y+1);
            }
            
            if (radius >= 3)
            {
                Clear(x-2,y);
                Clear(x+2,y);
                Clear(x,y-2);
                Clear(x,y+2);
            }
            
            if (radius >= 4)
            {
                Clear(x-2,y-1);
                Clear(x-2,y+1);
                Clear(x+2,y-1);
                Clear(x+2,y+1);
                Clear(x-1,y+2);
                Clear(x+1,y+2);
                Clear(x-1,y-2);
                Clear(x+1,y-2);
            }
            
            if (radius >= 5)
            {
                Clear(x-3,y);
                Clear(x+3,y);
                Clear(x,y-3);
                Clear(x,y+3);
            }
            
        }

        public void StartRound()
        {
            tilemap.gameObject.SetActive(true);
        }

        public void FinishRound()
        {
            tilemap.gameObject.SetActive(false);
        }
    }
}