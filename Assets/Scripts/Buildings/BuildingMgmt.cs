using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Buildings;
using DataTypes;
using Game;
using UI;
using UnityEngine;

public class BuildingMgmt : MonoBehaviour
{
    public GameObject buildPrefab;

    public static BuildingMgmt Get()
    {
        return GameMgmt.Get().building;
    }
    
    /// <summary>
    /// Return the building, if exist, at this postion
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>the unit or null</returns>
    public static BuildingInfo At(int x, int y)
    {
        BuildingInfo b = Get().GetAll().SingleOrDefault(g => g.X() == x && g.Y() == y);
        return b;
    }
    
    /// <summary>
    /// Return the building, if exist, at this postion
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>the unit or null</returns>
    public static BuildingInfo At(Vector3Int pos)
    {
        return GameHelper.Valide(pos)?At(pos.x, pos.y):null;
    }

    public BuildingInfo[] GetAll()
    {
        return GetComponentsInChildren<BuildingInfo>(true);
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

    public BuildingInfo Create(int town, string type, int x, int y)
    {
        //exist?
        if (!Data.building.ContainsKey(type))
        {
            throw new MissingComponentException($"Building {type} not exist");
        }
        
        GameObject building = Instantiate(buildPrefab, GetComponent<Transform>());
        BuildingInfo bi = building.GetComponent<BuildingInfo>();
        bi.Init(town,type,x,y);
        GameMgmt.Get().data.buildings.Add(bi.data);
        return bi;
    }

    public GameObject Load(BuildingUnitData data)
    {
        //exist?
        if (!Data.building.ContainsKey(data.type))
        {
            throw new MissingComponentException("Building "+data.type+ "not exist");
        }
        
        GameObject building = Instantiate(buildPrefab, GetComponent<Transform>());
        building.GetComponent<BuildingInfo>().Load(data);
        return building;
    }
}
