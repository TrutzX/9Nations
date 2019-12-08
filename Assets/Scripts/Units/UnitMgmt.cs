﻿using System;
using System.Drawing;
using System.Linq;
using Buildings;
using Game;
using Maps;
using NesScripts.Controls.PathFind;
using Players;
using UI;
using UnityEngine;

namespace Units
{
    public class UnitMgmt : MonoBehaviour
    {
        public GameObject unitPrefab;
    
        public static UnitMgmt Get()
        {
            return GameMgmt.Get().unit;
        }
    
        /// <summary>
        /// Return the unit, if exist, at this postion
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>the unit or null</returns>
        public static UnitInfo At(int x, int y)
        {
            UnitInfo go = Get().GetAll().SingleOrDefault(g => g.X() == x && g.Y() == y);
            return go == null ? null : go;
        }

        public UnitInfo[] GetAll()
        {
            return GetComponentsInChildren<UnitInfo>(true);
        }

        /// <summary>
        /// Returns true, if no unit is on the field
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsFree(int x, int y)
        {
            return At(x, y) == null;
        }
    
        public UnitInfo[] GetUnitPlayer(int pid)
        {
            return GetAll().Where(g => g.data.playerId == pid).ToArray();
        }
    
        public UnitInfo[] GetUnitPlayerType(int pid, string type)
        {
            return GetUnitPlayer(pid).Where(g => g.data.type == type).ToArray();
        }

        public UnitInfo Create(int player, string type, int x, int y)
        {
            //exist?
            if (!Data.unit.ContainsKey(type))
            {
                throw new MissingComponentException("Unit "+type+ "not exist");
            }

            if (!GameHelper.Valide(x, y))
            {
                throw new MissingComponentException("not a valid position");
            }
        
            UnitInfo unit = Instantiate(unitPrefab, GetComponent<Transform>()).GetComponent<UnitInfo>();
            unit.Init(type,player,x,y);
            GameMgmt.Get().data.units.Add(unit.data);
            return unit;
        }

        public UnitInfo Create(int player, string type, PPoint p)
        {
            return Create(player, type, p.x, p.y);
        }

        public GameObject Load(BuildingUnitData data)
        {
            //exist?
            if (!Data.unit.ContainsKey(data.type))
            {
                throw new MissingComponentException("Unit "+data.type+ "not exist");
            }
        
            GameObject unit = Instantiate(unitPrefab, GetComponent<Transform>());
            unit.GetComponent<UnitInfo>().Load(data);
            return unit;
        }

        public void ShowNextAvaibleUnitForPlayer()
        {
            //has an active unit?
            //Debug.LogWarning(FindObjectOfType<OnMapUI>());
            UnitInfo u = FindObjectOfType<OnMapUI>().unitUI.active;
            UnitInfo[] units = GetUnitPlayer(PlayerMgmt.ActPlayer().id);
            
            //get startpos?
            int s = u==null?0:Array.IndexOf(units, u.gameObject);
            
            //find next
            for (int i = 0; i < units.Length; i++)
            {
                UnitInfo ui = units[(i+s+1)%units.Length];
                
                //right unit?
                if (ui.GetData().ap > 0)
                {
                    FindObjectOfType<OnMapUI>().UpdatePanelXY(ui.GetData().x, ui.GetData().y);
                    CameraMove.Get().MoveTo(ui.GetData().x, ui.GetData().y);
                    return;
                }
            }
            
            //nothing found?
            NAudio.PlayBuzzer();
        }
    }
}