using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using Libraries;
using MapElements;
using MapElements.Buildings;
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

        public BuildingInfo[] GetByTownType(int tid, string type, bool finish = false)
        {
            return GetByTown(tid).Where(g => g.data.type == type && (!finish || !g.IsUnderConstruction())).ToArray();
        }

        public BuildingInfo[] GetByPlayer(int pid)
        {
            return GetAll().Where(g => g.data.playerId == pid).ToArray();
        }

        public BuildingInfo[] GetByPlayerType(int pid, string type, bool finish = false)
        {
            return GetByPlayer(pid).Where(g => g.data.type == type && (!finish || !g.IsUnderConstruction())).ToArray();
        }

        public BuildingInfo Create(int town, string type, NVector pos, Dictionary<string, int> cost=null)
        {
            //exist?
            if (!L.b.buildings.ContainsKey(type))
            {
                throw new MissingComponentException($"Building {type} not exist");
            }

            if (!pos.Valid())
            {
                throw new MissingComponentException("not a valid position");
            }

            if (At(pos) != null)
            {
                throw new MissingComponentException($"field {pos} is blocked");
            }

            BuildingInfo bi = Instantiate(buildPrefab, GameMgmt.Get().newMap.levels[pos.level].buildings.transform);
            bi.Init(town,type,pos,cost??new Dictionary<string, int>(L.b.buildings[type].cost));
            bi.NextRound();
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
                S.Player(data.playerId).info.Add(new Info($"Building {data.type} ({data.pos}) not exist","no"));
                return null;
            }
        
            BuildingInfo bi = Instantiate(buildPrefab, GameMgmt.Get().newMap.levels[data.pos.level].buildings.transform);
            bi.Load(data);
            buildings.Add(bi);
            return bi;
        }
    }
}
