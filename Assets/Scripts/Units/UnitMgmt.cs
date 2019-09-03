using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Game;
using Units;
using UnityEngine;

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
        GameObject go = Get().GetAll().SingleOrDefault(g => g != null && (int) g.transform.position.x == x && (int) g.transform.position.y == y);
        return go == null ? null : go.GetComponent<UnitInfo>();
    }

    public GameObject[] GetAll()
    {
        return GameObject.FindGameObjectsWithTag("Unit");
    }
    
    public GameObject[] GetUnitPlayer(int pid)
    {
        return GetAll().Where(g => g.GetComponent<UnitInfo>().data.player == pid).ToArray();
    }
    
    public GameObject[] GetUnitPlayerType(int pid, string type)
    {
        return GetUnitPlayer(pid).Where(g => g.GetComponent<UnitInfo>().data.type == type).ToArray();
    }

    public GameObject Create(int player, string type, int x, int y)
    {
        //exist?
        if (!Data.unit.ContainsKey(type))
        {
            throw new MissingComponentException("Unit "+type+ "not exist");
        }
        
        GameObject unit = Instantiate(unitPrefab, GetComponent<Transform>());
        unit.GetComponent<UnitInfo>().Init(type,player,x,y);
        GameMgmt.Get().data.units.Add(unit.GetComponent<UnitInfo>().data);
        return unit;
    }

    public GameObject Create(int player, string type, Point p)
    {
        return Create(player, type, p.X, p.Y);
    }

    public GameObject Load(UnitData data)
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
    
    
}
