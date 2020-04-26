using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Buildings;
using Help;
using Game;
using Libraries;
using Maps;
using Maps.GameMaps;
using Maps.TileMaps;
using Tools;
using UI;
using Units;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Players
{
    [Serializable]
    public class PlayerFog
    {
        [SerializeField] private List<bool[,]> visible;
        [NonSerialized] public List<TileMapConfig16> tileMap;
        [SerializeField] private bool noFog;

        /// <summary>
        /// Create all 
        /// </summary>
        public PlayerFog()
        {
            noFog = !S.Fog();
            if (noFog) return;
            
            visible = new List<bool[,]>();

            foreach (GameMapDataLevel l in GameMgmt.Get().data.map.levels)
            {
                visible.Add(new bool[GameMgmt.Get().data.map.width,GameMgmt.Get().data.map.height]);
            }
        }

        public IEnumerator CreatingFog(int pid)
        {
            
            if (noFog) yield break;
            
            tileMap = new List<TileMapConfig16>();
            TileBase fTile = GameMgmt.Get().newMap.prototypeFog;

            int width = visible[0].GetLength(0);
            int height = visible[0].GetLength(1);
            string max = "/" + width + "/" + GameMgmt.Get().data.map.levels.Count;

            for(int level=0;level<GameMgmt.Get().data.map.levels.Count;level++)
            {
                tileMap.Add(GameMgmt.Get().newMap[level].CreateNewLayer($"Fog of war {pid}",10));
                bool[,] v = visible[level];
                //paint everything?
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        bool d = v[x,y];
                        //skip?
                        if (d) continue;
                        
                        NVector pos = new NVector(x,y,level);
                        
                        //set it
                        SetTile(pos,fTile);
                    
                        //set border
                        if (x == 0)
                        {
                            SetTile(pos.DiffX(-1),fTile);
                        }
                        if (x == width-1)
                        {
                            SetTile(pos.DiffX(1),fTile);
                        }
                        if (y == 0)
                        {
                            SetTile(pos.DiffY(-1),fTile);
                        }
                        if (y == height-1)
                        {
                            SetTile(pos.DiffY(1),fTile);
                        }
                    }
                    yield return GameMgmt.Get().load.ShowSubMessage($"Create player fog {x}{max}");
                }
            
                //add points
                SetTile(new NVector(-1,-1,level),fTile);
                SetTile(new NVector(-1,height,level),fTile);
                SetTile(new NVector(width,-1,level),fTile);
                SetTile(new NVector(width,height,level),fTile);
            }

            FinishRound();
        }

        public void Clear(NVector pos)
        {
            //check koor
            if (noFog) return;
            if (!pos.Valid()) return;
            if (Visible(pos)) return;
            
            SetTile(pos);

            visible[pos.level][pos.x, pos.y] = true;
        }

        public bool Visible(NVector pos)
        {
            return noFog || visible[pos.level][pos.x, pos.y];
        }
        
        private void SetTile(NVector pos, TileBase tile = null)
        {
            //set it?
            if (tileMap == null) return;
            
            TilemapHelper.SetTile(tileMap[pos.level].GetComponent<Tilemap>(),pos.x,pos.y,tile);
        }

        public void Clear(NVector pos, int radius)
        {
            //Debug.Log($"Clear {x},{y} for {radius}");

            foreach (var p in CircleGenerator.Gen(pos, radius))
            {
                Clear(p);
            }
            
        }

        public void StartRound()
        {
            //has fog?
            if (noFog) return;
            
            foreach (TileMapConfig16 t in tileMap)
            {
                t.gameObject.SetActive(true);
            }

            ShowHideBuilding();
            ShowHideUnits();
        }

        private void ShowHideBuilding()
        {
            //hide / show all objects
            foreach (BuildingInfo b in GameMgmt.Get().building.GetAll())
            {
                b.gameObject.SetActive(Visible(b.Pos()));
            }
        }

        private void ShowHideUnits()
        {
            //hide / show all objects
            foreach (UnitInfo u in GameMgmt.Get().unit.GetAll())
            {
                u.gameObject.SetActive(Visible(u.Pos()));
            }
        }

        public void FinishRound()
        {
            //has fog?
            if (noFog) return;
            
            foreach (TileMapConfig16 t in tileMap)
            {
                t.gameObject.SetActive(false);
            }
        }
    }
}