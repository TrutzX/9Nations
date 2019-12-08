using System.Collections.Generic;
using System.Drawing;
using Buildings;
using Game;
using Help;
using Maps;
using NesScripts.Controls.PathFind;
using Players;
using UI;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Actions
{
    public abstract class ActiveAction : BaseAction
    {
        protected MapElementInfo MapElementInfo;
        protected int X, Y;
        protected Player Player;
        protected string Settings;
        protected List<PPoint> Points;

        protected int LastClickX, LastClickY;
        
        protected Tilemap TileMap;
        protected override void ButtonAction(Player player, MapElementInfo mapElementInfo, int x, int y, string settings)
        {
            //get tilemap
            if (GameMgmt.Get().map.activeAction == null)
            {
                GameMgmt.Get().map.activeAction = GameMgmt.Get().map.CreateLayer16("ActiveAction",GameMapMgmt.SortActiveAction).GetComponent<Tilemap>();
            }
            TileMap = GameMgmt.Get().map.activeAction;
            Points = new List<PPoint>();
            
            //save caller
            Player = player;
            MapElementInfo = mapElementInfo;
            X = x;
            Y = y;
            Settings = settings;

            LastClickX = -1;
            
            //set as active action
            OnMapUI.Get().SetActiveAction(this,mapElementInfo.GetComponent<BuildingInfo>()!=null);
            PreRun();
        }

        protected void Color(int x, int y, int tile)
        {
            Points.Add(new PPoint(x,y));
            TilemapHelper.SetTile(TileMap,x,y,GameMapMgmt.Get().selected[tile]);
        }
        
        public abstract void PreRun();

        public void Remove()
        {
            //clear tiles
            foreach (PPoint p in Points)
            {
                TilemapHelper.SetTile(TileMap,p.x,p.y,null);
            }
            
            RemoveAfter();
        }
        
        protected abstract void RemoveAfter();
        
        protected override void ButtonAction(Player player, string settings)
        {
            Debug.LogWarning("Not implemented");
        }

        public abstract string PanelMessage();

        public abstract void ClickFirst();

        public abstract void ClickSecond();

        public void Click(int x, int y)
        {
            //known click?
            if (LastClickX == x && LastClickY == y)
            {
                ClickSecond();
                OnMapUI.Get().SetActiveAction(null,false);
                return;
            }
            
            //new Click?
            LastClickX = x;
            LastClickY = y;
            ClickFirst();
        }
    }
}