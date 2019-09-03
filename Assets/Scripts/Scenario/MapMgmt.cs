using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using DataTypes;
using DefaultNamespace;
using Game;
using Scenario;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapMgmt : MonoBehaviour
{
    private List<Tilemap> layers;

    public GameObject mapLayerPrototyp;
    public TileBase[] tiles;

    public TileBase fog;
    public TileBase border;
    
    /// <summary>
    /// Get it
    /// </summary>
    /// <returns></returns>
    public static MapMgmt Get()
    {
        return GameMgmt.Get().map;
    }

    public static Vector2 GetMouseMapXY()
    {
        return Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
    }

    public void LoadMap()
    {
        //can load?
        if (GameMgmt.Get().data.mapfile == null)
        {
            return;
        }
        
        //load Map
        LoadMap(ScenarioMgmt.GetAllMaps().Find(m => m.name==GameMgmt.Get().data.mapfile));
        
    }
    
    // Update is called once per frame
    public void LoadMap(Map map)
    {
        //set size
        string[][] l = CSV.Read(map.Layer(0));
        GameMgmt.Get().data.mapwidth = l.Length;
        GameMgmt.Get().data.mapheight = l[0].Length;
        
        //load layer
        layers = new List<Tilemap>();
        AddNewLayer(transform.childCount,CSV.Convert(l));
        AddNewLayer(transform.childCount,CSV.Convert(CSV.Read(map.Layer(1))));

    }

    private void AddNewLayer(int level, int[][] data)
    {
        Tilemap t = CreateLayer($"Layer {level}");
        layers.Insert(0,t);
        
        for (int x = 0; x < data.Length; x++)
        {
            for (int y = 0; y < data[0].Length; y++)
            {
                int d = data[x][y];
                //skip?
                if (d == -1)
                {
                    continue;
                }

                try
                {
                    Set(t, x, y, tiles[d]);

                    //add border?
                    if (x == 0)
                    {
                        Set(t, x-1, y, tiles[d]);
                    }
                    if (x == GameMgmt.Get().data.mapwidth-1)
                    {
                        Set(t, x+1, y, tiles[d]);
                    }
                    if (y == 0)
                    {
                        Set(t, x, y-1, tiles[d]);
                    }
                    if (y == GameMgmt.Get().data.mapheight-1)
                    {
                        Set(t, x, y+1, tiles[d]);
                    }
                }
                catch (IndexOutOfRangeException i)
                {
                    Debug.LogException(new InvalidDataException($"Field type {d} does not exist",i));
                    break;
                }
            }
        }
    }

    private void AddBorder()
    {
        Tilemap t = CreateLayer("Border");

        for (int x = -1; x <= GameMgmt.Get().data.mapwidth; x++)
        {
            Set(t,x,-1,border);
            Set(t,x,GameMgmt.Get().data.mapheight,border);
        }

        for (int y = 0; y < GameMgmt.Get().data.mapheight; y++)
        {
            Set(t,-1,y,border);
            Set(t,GameMgmt.Get().data.mapwidth,y,border);
        }
    }
    
    private void Set(Tilemap t, int x, int y, TileBase tile)
    {
        //set it
        t.SetTile(new Vector3Int(x * 2, y * 2, 0), tile);
        t.SetTile(new Vector3Int(x * 2 + 1, y * 2, 0), tile);
        t.SetTile(new Vector3Int(x * 2, y * 2 + 1, 0), tile);
        t.SetTile(new Vector3Int(x * 2 + 1, y * 2 + 1, 0), tile);
    }

    public Tilemap CreateLayer(string name)
    {
        GameObject g = Instantiate(mapLayerPrototyp, GetComponent<Transform>());
        g.name = name;
        return g.GetComponent<Tilemap>();
    }

    public NTerrain GetTerrain(int x, int y)
    {
        Vector3Int v = new Vector3Int(x * 2, y * 2, 0);
        foreach (Tilemap layer in layers)
        {
            if (layer.HasTile(v))
            {
                return Data.nTerrain[layer.GetTile(v).name];
            }
        }

        return Data.nTerrain["unknown"];
    }

    /// <summary>
    /// Get a start position or an exception
    /// </summary>
    /// <param name="nation"></param>
    /// <returns></returns>
    /// <exception cref="MissingMemberException"></exception>
    public Point GetStartPos(string nation)
    {
        Nation n = Data.nation[nation];
        int i = 0;
        while (i < 1000)
        {
            int x = Random.Range(0, GameMgmt.Get().data.mapwidth);
            int y = Random.Range(0, GameMgmt.Get().data.mapheight);
            NTerrain t = GetTerrain(x, y);
            
            //right terrain?
            if (t.walk == 5 || t.name == n.hometerrain)
            {
                //has a unit?
                if (UnitMgmt.At(x,y) == null)
                    return new Point(x,y);
            }

            i++;
        }
        
        throw new MissingMemberException($"Can not find a start position for {nation}");
    }

    public static bool Valide(int x, int y)
    {
        return 0 <= x && x < GameMgmt.Get().data.mapwidth && 0 <= y && y < GameMgmt.Get().data.mapheight;
    }

    public void FinishStart()
    {
        AddBorder();
    }
}
