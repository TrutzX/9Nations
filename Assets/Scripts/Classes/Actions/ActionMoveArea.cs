using System;
using System.Linq;
using Audio;
using Game;
using Libraries.FActions;
using Players;
using Tools;
using Units;
using UnityEngine;

namespace Classes.Actions
{
    public class ActionMoveArea : BaseActiveAction
    {
        public ActionMoveArea() : base("moveArea") { }

        public override void PreRun()
        {
            Color(initPos, 0);
            //calc size
            int diff = mapElementInfo.data.ap / 5;
            string moveTyp = ((UnitInfo) mapElementInfo).dataUnit.movement;

            //collect move
            for (int x = Math.Max(0,initPos.x-diff); x <= Math.Min(GameMgmt.Get().data.map.width-1,initPos.x+diff); x++)
            {
                for (int y = Math.Max(0,initPos.y-diff); y <= Math.Min(GameMgmt.Get().data.map.height-1,initPos.y+diff); y++)
                {
                    //check enemy and fog
                    //TODO combine with unit check
                    NVector dPos = new NVector(x, y, initPos.level);
                    if (!player.fog.Visible(dPos))
                    {
                        continue;
                    }
                    
                    //Debug.LogWarning("Check "+x+" "+y+$" has {X}/{Y}");
                    
                    //own unit?
                    if (x == initPos.x && y == initPos.y)
                    {
                        continue;
                    }

                    //free?
                    if (!S.Unit().Free(dPos))
                    {
                        continue;
                    }
                    
                    //can move their?
                    int cost = GameMgmt.Get().newMap.PathFinding(mapElementInfo.Pos().level).Cost(player, moveTyp, mapElementInfo.Pos(), dPos);
                    if (cost > 0 && mapElementInfo.data.ap >= cost)
                    {
                        Color(dPos,0);
                    }
                }
            }
            
            
        }

        protected override void RemoveAfter()
        {
        }

        public override string PanelMessage()
        {
            return "Select the field, where you want to move.";
        }

        public override void ClickFirst()
        {
            //was selected?
            if (Points.Count(p => p.x == LastClickPos.x && p.y == LastClickPos.y) == 0)
            {
                OnMapUI.Get().SetActiveAction(null,false);
                NAudio.PlayCancel();
                return;
            }
            
            string moveTyp = ((UnitInfo)mapElementInfo).dataUnit.movement;
            int cost = GameMgmt.Get().newMap.PathFinding(mapElementInfo.Pos().level).Cost(player, moveTyp, mapElementInfo.Pos(), new NVector(LastClickPos.x, LastClickPos.y, mapElementInfo.Pos().level));
            OnMapUI.Get().unitUI.ShowPanelMessage($"You want to move to this field for {cost} AP? Click again!");
        }

        public override void ClickSecond()
        {
            ((UnitInfo)mapElementInfo).MoveTo(LastClickPos);
        }
    }
}