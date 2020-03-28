using System;
using System.Linq;
using Game;
using Libraries.FActions;
using MapActions;
using Players;
using Tools;
using Units;

namespace Classes.Actions
{
    public class ActionMove : ActiveAction
    {
        public ActionMove() : base("move") { }

        public override void PreRun()
        {
            Color(Pos, 0);
            //calc size
            int diff = mapElementInfo.data.ap / 5;
            string moveTyp = ((UnitInfo) mapElementInfo).dataUnit.movement;

            for (int x = Math.Max(0,Pos.x-diff); x <= Math.Min(GameMgmt.Get().data.map.width-1,Pos.x+diff); x++)
            {
                for (int y = Math.Max(0,Pos.y-diff); y < Math.Min(GameMgmt.Get().data.map.height-1,Pos.y+diff); y++)
                {
                    //check enemy and fog
                    //TODO combine with unit check
                    NVector dPos = new NVector(x, y, Pos.level);
                    if (!Player.fog.Visible(dPos))
                    {
                        continue;
                    }
                    
                    //Debug.LogWarning("Check "+x+" "+y+$" has {X}/{Y}");
                    
                    //own unit?
                    if (x == Pos.x && y == Pos.y)
                    {
                        continue;
                    }

                    if (!S.Unit().Free(dPos))
                    {
                        //TODO color own?
                        if (S.Unit().At(dPos).Owner(PlayerMgmt.ActPlayerID()))
                        {
                            Color(dPos,2);
                        }
                        else
                        {
                            Color(dPos,1);
                        }
                        continue;
                    }

                    int cost = GameMgmt.Get().newMap.PathFinding(mapElementInfo.Pos().level).Cost(Player, moveTyp, mapElementInfo.Pos(), dPos);
                    if (cost>0 && mapElementInfo.data.ap >= cost)
                        Color(dPos,0);
                    
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
            if (!S.Unit().Free(LastClickPos))
            {
                UnitInfo unit = S.Unit().At(LastClickPos);
                if (unit.Owner(PlayerMgmt.ActPlayerID()))
                {
                    OnMapUI.Get().unitUI.ShowPanelMessage($"You want to interact with {unit.name}? Click again!");
                }
                else
                {
                    OnMapUI.Get().unitUI.ShowPanelMessage($"You want to fight with {unit.name} from {unit.Player().name}? Click again!");
                }
                return;
            }
            
            string moveTyp = ((UnitInfo)mapElementInfo).dataUnit.movement;
            int cost = GameMgmt.Get().newMap.PathFinding(mapElementInfo.Pos().level).Cost(Player, moveTyp, mapElementInfo.Pos(), new NVector(LastClickX, LastClickY, mapElementInfo.Pos().level));
            OnMapUI.Get().unitUI.ShowPanelMessage($"You want to move to this field for {cost} AP? Click again!");
        }

        public override void ClickSecond()
        {
            //unit?
            if (!S.Unit().Free(LastClickPos))
            {
                InteractionHelper.ShowWindow(mapElementInfo,S.Unit().At(LastClickPos));
                return;
            }
            
            ((UnitInfo)mapElementInfo).MoveTo(LastClickX,LastClickY);
        }
    }
}