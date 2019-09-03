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
    /// Return the unit, if exist, at this postion
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>the unit or null</returns>
    public static BuildingInfo At(int x, int y)
    {
        GameObject b = Get().GetAll().SingleOrDefault(g => g != null && (int) g.transform.position.x == x && (int) g.transform.position.y == y);
        return b == null ? null : b.GetComponent<BuildingInfo>();
    }

    public GameObject[] GetAll()
    {
        return GameObject.FindGameObjectsWithTag("Building");
    }

    public GameObject Create(int town, string type, int x, int y)
    {
        //exist?
        if (!Data.building.ContainsKey(type))
        {
            throw new MissingComponentException("Building "+type+ "not exist");
        }
        
        GameObject building = Instantiate(buildPrefab, GetComponent<Transform>());
        building.GetComponent<BuildingInfo>().Init(town,type,x,y);
        GameMgmt.Get().data.buildings.Add(building.GetComponent<BuildingInfo>().data);
        return building;
    }

    public GameObject Load(BuildingData data)
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
