﻿using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using Libraries;
using Players;
using Players.Infos;
using Tools;
using UnityEngine;

namespace Buildings
{
    public class BuildingMgmt : MonoBehaviour
    {
        public BuildingInfo buildPrefab;
        public List<BuildingInfo> buildings = new List<BuildingInfo>();
        
        [Obsolete]
        public static BuildingMgmt Get()
        {
            return GameMgmt.Get().building;
        }
    
        /// <summary>
        /// Return the building, if exist, at this postion
        /// </summary>
        /// <param name="pos"></param>
        /// <returns>the unit or null</returns>
        public BuildingInfo At(NVector pos)
        {
            return GameMgmt.Get().building.GetAll().SingleOrDefault(g => g.Pos().Equals(pos));
        }

        /// <summary>
        /// Returns true, if no building is on the field
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool Free(NVector pos)
        {
            return At(pos) == null;
        }

        public BuildingInfo[] GetAll()
        {
            return buildings.ToArray();
        }

        public BuildingInfo[] GetByTown(int tid)
        {
            return GetAll().Where(g => g.data.townId == tid).ToArray();
        }

        public BuildingInfo[] GetByTownType(int tid, string type)
        {
            return GetByTown(tid).Where(g => g.data.type == type).ToArray();
        }

        public BuildingInfo[] GetByPlayer(int pid)
        {
            return GetAll().Where(g => g.data.playerId == pid).ToArray();
        }

        public BuildingInfo[] GetByPlayerType(int pid, string type)
        {
            return GetByPlayer(pid).Where(g => g.data.type == type).ToArray();
        }

        public BuildingInfo Create(int town, string type, NVector pos)
        {
            //exist?
            if (!L.b.buildings.ContainsKey(type))
            {
                throw new MissingComponentException($"Building {type} not exist");
            }
        
            BuildingInfo bi = Instantiate(buildPrefab, GameMgmt.Get().newMap.levels[pos.level].buildings.transform);
            bi.Init(town,type,pos);
            GameMgmt.Get().data.buildings.Add(bi.data);
            buildings.Add(bi);
            return bi;
        }

        public BuildingInfo Load(BuildingUnitData data)
        {
            //exist?
            if (!L.b.buildings.ContainsKey(data.type))
            {
                Debug.LogError($"Building {data.type} ({data.pos}) not exist");
                PlayerMgmt.Get(data.playerId).info.Add(new Info($"Building {data.type} ({data.pos}) not exist","no"));
                return null;
            }
        
            BuildingInfo bi = Instantiate(buildPrefab, GameMgmt.Get().newMap.levels[data.pos.level].buildings.transform);
            bi.Load(data);
            buildings.Add(bi);
            return bi;
        }
    }
}
