using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Buildings;
using Game;
using InputActions;
using Libraries;
using Maps;
using NesScripts.Controls.PathFind;
using Players;
using Players.Infos;
using Tools;
using UI;
using UnityEngine;

namespace Units
{
    public class UnitMgmt : MonoBehaviour
    {
        public UnitInfo unitPrefab;
        public List<UnitInfo> units = new List<UnitInfo>();
    
        /// <summary>
        /// Return the building, if exist, at this postion
        /// </summary>
        /// <param name="pos"></param>
        /// <returns>the unit or null</returns>
        public UnitInfo At(NVector pos)
        {
            return GameMgmt.Get().unit.GetAll().SingleOrDefault(g => g.Pos().Equals(pos));
        }

        public UnitInfo[] GetAll()
        {
            return units.ToArray();
        }

        /// <summary>
        /// Returns true, if no unit is on the field
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool Free(NVector pos)
        {
            return At(pos) == null;
        }
    
        public UnitInfo[] GetByPlayer(int pid)
        {
            return GetAll().Where(g => g.data.playerId == pid).ToArray();
        }
    
        public UnitInfo[] GetByPlayerType(int pid, string type)
        {
            return GetByPlayer(pid).Where(g => g.data.type == type).ToArray();
        }

        public UnitInfo Create(int player, string type, NVector pos, Dictionary<string, int> cost=null)
        {
            //exist?
            if (!L.b.units.ContainsKey(type))
            {
                throw new MissingComponentException("Unit "+type+ "not exist");
            }

            if (!pos.Valid())
            {
                throw new MissingComponentException("not a valid position");
            }

            if (At(pos) != null)
            {
                throw new MissingComponentException($"field {pos} is blocked");
            }
        
            UnitInfo ui = Instantiate(unitPrefab, GameMgmt.Get().newMap.levels[pos.level].units.transform);
            ui.Init(type,player, pos, cost??new Dictionary<string, int>(L.b.units[type].cost));
            ui.NextRound();
            units.Add(ui);
            GameMgmt.Get().data.units.Add(ui.data);
            return ui;
        }

        public UnitInfo Load(BuildingUnitData data)
        {
            //exist?
            if (!L.b.units.ContainsKey(data.type))
            {
                Debug.LogError($"Unit {data.type} ({data.pos}) not exist");
                S.Player(data.playerId).info.Add(new Info($"Unit {data.type} ({data.pos}) not exist","no"));
                return null;
            }
        
            UnitInfo ui = Instantiate(unitPrefab, GameMgmt.Get().newMap.levels[data.pos.level].units.transform);
            ui.Load(data);
            units.Add(ui);
            return ui;
        }

        public void ShowNextAvailableUnitForPlayer()
        {
            //has an active unit?
            //Debug.LogWarning(FindObjectOfType<OnMapUI>());
            UnitInfo u = FindObjectOfType<OnMapUI>().unitUI.active;
            UnitInfo[] unitP = GetByPlayer(S.ActPlayer().id);
            
            //has units?
            if (unitP.Length == 0) return;
            
            //get startpos?
            int s = u==null?0:Array.IndexOf(unitP, u);
            
            //find next
            for (int i = 0; i < unitP.Length; i++)
            {
                UnitInfo ui = unitP[(i+s+1)%unitP.Length];
                
                //right unit?
                if (!ui.IsUnderConstruction() && ui.data.ap > 0)
                {
                    FindObjectOfType<OnMapUI>().UpdatePanel(ui.GetData().pos);
                    S.CameraMove().MoveTo(ui.GetData().pos);
                    return;
                }
            }
            
            //nothing found?
            OnMapUI.Get().ShowPanelMessageError("No available unit found.");
        }
    }
}
