using System;
using System.Collections.Generic;
using Buildings;
using Game;
using Help;
using Libraries.FActions;
using NesScripts.Controls.PathFind;
using Players;
using Tools;
using Units;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Classes.Actions
{
    public abstract class BaseActiveAction : BasePerformAction
    {
        protected MapElementInfo mapElementInfo;
        protected NVector initPos;
        protected Player player;
        protected ActionHolder action;
        protected List<PPoint> Points;
        
        protected NVector LastClickPos;
        
        protected Tilemap tileMap;

        protected BaseActiveAction(string id) : base(id) { }
        
        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos, ActionHolder holder)
        {
            //get tilemap
            tileMap = GameMgmt.Get().newMap[pos.level].border.GetComponent<Tilemap>();
            Points = new List<PPoint>();
            
            //save caller
            this.player = player;
            mapElementInfo = info;
            this.initPos = pos;
            action = holder;
            
            //set as active action
            OnMapUI.Get().SetActiveAction(this,mapElementInfo.IsBuilding());
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
            if (pos.Equals(LastClickPos))
            {
                ClickSecond();
                OnMapUI.Get().SetActiveAction(null,false);
                return;
            }
            
            //new Click?
            LastClickPos = pos;
            ClickFirst();
        }

        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            throw new System.NotImplementedException();
        }
    }
}