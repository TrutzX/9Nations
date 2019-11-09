using System.Collections.Generic;
using System.Drawing;
using Buildings;
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
            if (MapMgmt.Get().activeAction == null)
            {
                MapMgmt.Get().activeAction = MapMgmt.Get().CreateLayer("ActiveAction");
            }
            TileMap = MapMgmt.Get().activeAction;
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
            TilemapHelper.setTile(TileMap,x,y,MapMgmt.Get().selected[tile]);
        }
        
        public abstract void PreRun();

        public void Remove()
        {
            //clear tiles
            foreach (PPoint p in Points)
            {
                TilemapHelper.setTile(TileMap,p.x,p.y,null);
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