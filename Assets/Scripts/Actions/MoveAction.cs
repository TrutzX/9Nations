using System;
using System.Drawing;
using System.Linq;
using Buildings;
using Game;
using MapActions;
using Maps;
using Players;
using UI;
using Units;
using UnityEngine;

namespace Actions
{
    public class MoveAction : ActiveAction
    {
        public override void PreRun()
        {
            Color(X, Y, 0);
            //calc size
            int diff = MapElementInfo.data.ap / 5;
            string nation = Player.nation;
            string moveTyp = ((UnitInfo) MapElementInfo).config.movetyp;

            for (int x = Math.Max(0,X-diff); x <= Math.Min(GameMgmt.Get().data.mapwidth-1,X+diff); x++)
            {
                for (int y = Math.Max(0,Y-diff); y < Math.Min(GameMgmt.Get().data.mapheight-1,Y+diff); y++)
                {
                    //check enemy and fog
                    //TODO combine with unit check
                    if (!Player.fog.visible[x, y])
                    {
                        continue;
                    }
                    
                    //Debug.LogWarning("Check "+x+" "+y+$" has {X}/{Y}");
                    
                    //own unit?
                    if (x == X && y == Y)
                    {
                        Debug.LogWarning("continue");
                        continue;
                    }

                    if (!UnitMgmt.Get().IsFree(x, y))
                    {
                        //TODO color own?
                        if (UnitMgmt.At(x, y).Owner(PlayerMgmt.ActPlayerID()))
                        {
                            Color(x,y,2);
                        }
                        else
                        {
                            Color(x,y,1);
                        }
                        continue;
                    }

                    int cost = MapMgmt.Get().PathFinding.Cost(nation, moveTyp, X, Y, x, y);
                    if (cost>0 && MapElementInfo.data.ap >= cost)
                        Color(x,y,0);
                    
                    //Debug.LogWarning($"{x},{y}: {MapMgmt.Get().PathFinding.Cost(nation, moveTyp,X,Y, x, y)}");
                }
            }
        }

        protected override void RemoveAfter()
        {
        }

        public override string PanelMessage()
        {
            return "Select the field, where you want to move or interact.";
        }

        public override void ClickFirst()
        {
            //was selected?
            if (Points.Count(p => p.x == LastClickX && p.y == LastClickY) == 0)
            {
                OnMapUI.Get().SetActiveAction(null,false);
                NAudio.PlayCancel();
                return;
            }
            
            //special field?
            if (!UnitMgmt.Get().IsFree(LastClickX, LastClickY))
            {
                UnitInfo unit = UnitMgmt.At(LastClickX, LastClickY);
                if (unit.Owner(PlayerMgmt.ActPlayerID()))
                {
                    OnMapUI.Get().unitUI.SetPanelMessage($"You want to interact with {unit.name}? Click again!");
                }
                else
                {
                    OnMapUI.Get().unitUI.SetPanelMessage($"You want to fight with {unit.name} from {unit.Player().name}? Click again!");
                }
                return;
            }
            
            string nation = Player.nation;
            string moveTyp = ((UnitInfo)MapElementInfo).config.movetyp;
            int cost = MapMgmt.Get().PathFinding.Cost(nation, moveTyp, X, Y, LastClickX, LastClickY);
            OnMapUI.Get().unitUI.SetPanelMessage($"You want to move to this field for {cost} AP? Click again!");
        }

        public override void ClickSecond()
        {
            //unit?
            if (!UnitMgmt.Get().IsFree(LastClickX, LastClickY))
            {
                InteractionHelper.ShowWindow(MapElementInfo,UnitMgmt.At(LastClickX, LastClickY));
                return;
            }
            
            ((UnitInfo)MapElementInfo).MoveTo(LastClickX,LastClickY);
        }
    }
}