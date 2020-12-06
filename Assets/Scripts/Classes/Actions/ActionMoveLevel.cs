using System;
using Buildings;
using Game;
using Libraries.FActions;
using Libraries.FActions.General;
using MapElements;
using MapElements.Buildings;
using MapElements.Units;
using Players;
using Tools;
using UI;
using Units;
using UnityEngine;

namespace Classes.Actions
{
    public class ActionMoveLevel : BasePerformAction
    {
        public ActionMoveLevel() : base("moveLevel"){}

        protected override void Perform(ActionEvent evt, Player player, MapElementInfo info, NVector pos,
            ActionHolder holder)
        {
            if (!info.IsBuilding())
            {
                MoveUnit(info, pos);
                return;
            }
            
            MoveBuilding(info ,pos, holder);
        }

        private void MoveBuilding(MapElementInfo info, NVector pos, ActionHolder holder)
        {
            BuildingInfo ui = (BuildingInfo) info;
            //check status
            if (!info.data.data.ContainsKey("level"))
            {
                //build stairs
                int l = GameMgmt.Get().data.map.levels.Count;
                //blocked by buildings?
                bool up = pos.level+1 < l && S.Building().Free(pos.DiffLevel(1));
                bool down = pos.level > 0 && S.Building().Free(pos.DiffLevel(-1));

                if (!up && !down)
                {
                    OnMapUI.Get().unitUI.ShowPanelMessageError($"{info.name} can not work between the levels, there is no second level or it is blocked.");
                    return;
                }

                //ask?
                if (up && down)
                {
                    WindowPanelBuilder wpb = WindowPanelBuilder.Create("Where do you want to build your stairs?");
                    wpb.panel.AddButton($"Build stairs up to {GameMgmt.Get().data.map.levels[pos.level + 1].name}", () =>
                    {
                        Build(true, info, pos, holder);
                        wpb.Close();
                    });
                    wpb.panel.AddButton($"Build stairs down to {GameMgmt.Get().data.map.levels[pos.level - 1].name}", () =>
                    {
                        Build(false, info, pos, holder);
                        wpb.Close();
                    });
                    wpb.AddClose();
                    wpb.Finish();
                    return;
                }

                Build(up, info, pos, holder);
            }
            
            //move unit
            int diff = ConvertHelper.Int(info.data.data["level"]);
            TryMoveUnit(S.Unit().At(pos), pos.DiffLevel(diff));
        }

        private void Build(bool up, MapElementInfo info, NVector pos, ActionHolder holder)
        {
            //Debug.Log($"Old {pos}");
            //Debug.Log($"Old {info.Pos()}");
            
            if (up)
            {
                Debug.Log($"New {pos.DiffLevel(1)}");
                BuildingInfo n = S.Building().Create(info.Town().id, "stair", pos.DiffLevel(1));
                n.FinishConstruct();
                n.SetSprite(holder.data["stair"]);
                n.data.data["level"] = "-1";
            }
            else
            {
                Debug.Log($"New {pos.DiffLevel(-1)}");
                info.SetSprite(holder.data["stair"]);
                BuildingInfo n = S.Building().Create(info.Town().id, info.data.type, pos.DiffLevel(-1));
                n.FinishConstruct();
                n.data.data["level"] = "1";
            }

            info.data.data["level"] = up ? "1" : "-1";
        }

        private void TryMoveUnit(UnitInfo info, NVector pos)
        {
            //is free?
            if (!S.Unit().Free(pos))
            {
                OnMapUI.Get().unitUI.ShowPanelMessageError($"{info.name} can not move between the levels, the destination is blocked.");
                return;
            }
            
            //have enough ap?
            int cost = GameMgmt.Get().newMap[pos.level].PathFinding().Cost(info.Player(), info.dataUnit.movement, pos, pos);
            cost = Math.Max(cost, 10); // cost at least 10 ap

            if (cost > info.data.ap)
            {
                OnMapUI.Get().unitUI.ShowPanelMessageError($"{info.name} can not move between the levels. {info.name} need {cost} AP, but only have {info.data.ap} AP.");
                return;
            }

            info.data.ap -= cost;
            info.MoveTo(pos);
        }
        
        private void MoveUnit(MapElementInfo info, NVector pos)
        {
            UnitInfo ui = (UnitInfo) info;
            int l = GameMgmt.Get().data.map.levels.Count;
            
            //TODO check if go up/down possible?
            
            //how many level i have?
            if (l == 1)
            {
                OnMapUI.Get().unitUI.ShowPanelMessageError($"{info.name} can not move between the levels, there is no second level");
                return;
            }
            
            if (l == 2)
            {
                //switch it
                TryMoveUnit(ui, new NVector(pos.x,pos.y,(pos.level + 1) % 2));
                return;
            }

            if (pos.level == 0)
            {
                //go up
                TryMoveUnit(ui, pos.DiffLevel(+ 1));
                return;
            }

            if (pos.level+1 == l)
            {
                //go down
                TryMoveUnit(ui, pos.DiffLevel(- 1));
                return;
            }
            
            //ask
            WindowPanelBuilder wpb = WindowPanelBuilder.Create("Where do you want to go?");
            wpb.panel.AddButton($"Go up to {GameMgmt.Get().data.map.levels[pos.level + 1].name}", () =>
            {
                TryMoveUnit(ui, pos.DiffLevel(+ 1));
                wpb.Close();
            });
            wpb.panel.AddButton($"Go down to {GameMgmt.Get().data.map.levels[pos.level - 1].name}", () =>
            {
                TryMoveUnit(ui, pos.DiffLevel(- 1));
                wpb.Close();
            });
            wpb.AddClose();
            wpb.Finish();
        }
        
        protected override void Perform(ActionEvent evt, Player player, ActionHolder holder)
        {
            throw new System.NotImplementedException();
        }

        public override ActionHolder Create(string setting)
        {
            ActionHolder conf = base.Create(setting);
            conf.trigger = ActionEvent.Direct;
            conf.req.Add("notEmpty", "unit");
            if (!string.IsNullOrEmpty(setting))
                conf.data["stair"] = setting;
            return conf;
        }
    }
}