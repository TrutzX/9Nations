using System;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Buildings;
using Classes.Actions.Addons;

using Game;
using Libraries;
using Libraries.FActions;
using Libraries.Terrains;
using Libraries.Units;
using MapElements.Units;
using Players;
using reqs;
using Tools;
using UI;
using Units;
using UnityEngine;

namespace Classes.Actions
{
    public class ActionInteract : BaseActiveAction
    {
        public ActionInteract() : base("interact") { }

        public override void PreRun()
        {
            //calc size
            int diff = 1;
            //string moveTyp = ((UnitInfo) mapElementInfo).dataUnit.movement;

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
                    
                    //terrain ok?
                    //TODO not hardcoded
                    if (GameMgmt.Get().newMap.Terrain(dPos).category == "unknown")
                        continue;

                    //action possible?
                    foreach (var action in mapElementInfo.data.action.actions)
                    {
                        FDataAction data = action.DataAction();
                        if (!data.mapElement || data.field != "near")
                            continue;

                        if (!action.req.Check(S.ActPlayer(), mapElementInfo, dPos, true))
                        {
                            continue;
                        }
                        
                        //Debug.Log($"{data.name}: {dPos}");

                        // if (Points.Count(p => p.x == x && p.y == y) > 0)
                        // {
                        //     Points.RemoveAll(Points.Where(p => p.x == x && p.y == y));
                        // }
                        
                        if (!S.Unit().Free(dPos))
                        {
                            //TODO color own?
                            if (S.Unit().At(dPos).Owner(S.ActPlayerID()))
                            {
                                Color(dPos,2);
                            }
                            else
                            {
                                Color(dPos,1);
                            }
                        }
                        else
                        {
                            Color(dPos,3);
                        }

                        break;
                    }
                }
            }
            
            //nothing found?
            if (Points.Count == 0)
            {
                mapElementInfo.UI().ShowPanelMessageError("No interaction found.");
                OnMapUI.Get().SetActiveAction(null,mapElementInfo.IsBuilding());
                NAudio.PlayCancel();
            }
        }

        protected override void RemoveAfter()
        {
        }

        public override string PanelMessage()
        {
            return "Select the field, where you want to interact.";
        }

        protected override void ClickFirst()
        {
            //special field?
            if (!S.Unit().Free(LastClickPos))
            {
                UnitInfo unit = S.Unit().At(LastClickPos);
                if (unit.Owner(S.ActPlayerID()))
                {
                    mapElementInfo.UI().ShowPanelMessage($"You want to interact with {unit.name}? Click again!");
                }
                else
                {
                    mapElementInfo.UI().ShowPanelMessage($"You want to fight with {unit.name} from {unit.Player().name}? Click again!");
                }
                return;
            }

            DataTerrain terr = GameMgmt.Get().newMap.Terrain(LastClickPos);
            mapElementInfo.UI().ShowPanelMessage($"You want to interact with {terr.Name()}? Click again!");
        }

        protected override void ClickFirstCancel()
        {
            OnMapUI.Get().SetActiveAction(null,mapElementInfo.IsBuilding());
        }

        protected override void ClickSecond()
        {
            //load options
            WindowBuilderSplit b = WindowBuilderSplit.Create("Interaction","Perform");

            foreach(var action in mapElementInfo.data.action.actions)
            {
                FDataAction data = action.DataAction();
                if (!data.mapElement || data.field != "near")
                    continue;

                //found it?
                if (!action.req.Check(S.ActPlayer(), mapElementInfo, LastClickPos, true))
                {
                    continue;
                }
                
                
                ActionInteractSplitElement be = new ActionInteractSplitElement(mapElementInfo.data.action, action, mapElementInfo, LastClickPos);
                be.disabled = action.req.Desc(S.ActPlayer(), mapElementInfo, LastClickPos);
                be.audioPerform = data.sound;
                b.Add(be);
            }

            //is empty?
            if (b.Count() == 0)
            {
                mapElementInfo.UI().ShowPanelMessageError("No interaction with neighbors found.");
                b.CloseWindow();
                return;
            }
            
            b.Finish();
        }
    }
    
    
}