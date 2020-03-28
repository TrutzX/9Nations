using System.Collections.Generic;
using Buildings;
using Classes.Actions;
using Game;
using Help;
using Libraries.FActions.General;
using NesScripts.Controls.PathFind;
using Players;
using Tools;
using UI;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Libraries.FActions
{
    public abstract class ActiveAction : BasePerformAction
    {
        protected MapElementInfo mapElementInfo;
        protected NVector Pos;
        protected Player Player;
        protected ActionHolder action;
        protected List<PPoint> Points;

        protected int LastClickX, LastClickY;
        protected NVector LastClickPos;
        
        protected Tilemap tileMap;

        protected ActiveAction(string id) : base(id) { }
        
        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos, ActionHolder holder)
        {
            //get tilemap
            tileMap = GameMgmt.Get().newMap[pos.level].border.GetComponent<Tilemap>();
            Points = new List<PPoint>();
            
            //save caller
            Player = player;
            mapElementInfo = info;
            Pos = pos;
            action = holder;

            LastClickX = -1;
            
            //set as active action
            OnMapUI.Get().SetActiveAction(this,mapElementInfo.GetComponent<BuildingInfo>()!=null);
            PreRun();
        }

        protected void Color(NVector pos, int tile)
        {
            Points.Add(new PPoint(pos.x,pos.y));
            TilemapHelper.SetTile(tileMap,pos.x,pos.y,GameMgmt.Get().newMap.prototypeSelected[tile]);
        }
        
        public abstract void PreRun();

        public void Remove()
        {
            //clear tiles
            foreach (PPoint p in Points)
            {
                TilemapHelper.SetTile(tileMap,p.x,p.y,null);
            }
            
            RemoveAfter();
        }
        
        protected abstract void RemoveAfter();

        public abstract string PanelMessage();

        public abstract void ClickFirst();

        public abstract void ClickSecond();

        public void Click(NVector pos)
        {
            //known click?
            if (LastClickX == pos.x && LastClickY == pos.y)
            {
                ClickSecond();
                OnMapUI.Get().SetActiveAction(null,false);
                return;
            }
            
            //new Click?
            LastClickX = pos.x;
            LastClickY = pos.y;
            LastClickPos = pos;
            ClickFirst();
        }

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            throw new System.NotImplementedException();
        }
    }
}