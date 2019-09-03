using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Actions;
using Buildings;
using DataTypes;
using DefaultNamespace;
using DigitalRuby.Tween;
using Game;
using Players;
using reqs;
using Towns;
using UI;
using Units;
using UnityEngine;

public class UnitInfo : MonoBehaviour, IMapElement
{
    public UnitData data;
    public Unit config;

    public void NextRound()
    {
        data.lastError = null;
        
        //under construction?
        if (IsUnderConstrution() && GetComponent<Construction>().RoundConstruct())
        {
            return;
        }

        int x = (int) transform.position.x;
        int y = (int) transform.position.y;
        Town t = TownMgmt.Get().NearstTown(PlayerMgmt.Get(data.player), x, y, false);
        
        //has a town?
        if (t == null)
        {
            data.ap = config.ap;
            return;
        }
        
        //produce?
        if (!ReqHelper.Check(PlayerMgmt.Get(t.playerID),config.GenProduceReq(),gameObject,x,y))
        {
            data.lastError = ReqHelper.Desc(PlayerMgmt.Get(t.playerID), config.GenProduceReq(), gameObject, x, y);
            return;
        }
        
        foreach (KeyValuePair<string, int> ress in config.GenProduce())
        {
            //give ressources
            t.AddRess(ress.Key,ress.Value);
        }
        
        data.ap = config.ap;
        
    }

    public void Init(string configType, int player, int x, int y)
    {
        Debug.Log($"Create unit {configType} at {x},{y}");
        
        data = new UnitData();
        data.Init(configType, -1, player);
        data.x = x;
        data.y = y;
        config = Data.unit[configType];
        
        //has a town?
        Town t = TownMgmt.Get().NearstTown(PlayerMgmt.Get(player), x, y, false);
        if (t != null)
        {
            data.town = t.id;
            gameObject.AddComponent<Construction>();
            gameObject.GetComponent<Construction>().Init(data,config.GenCost(),this,config.buildtime,t.id);
            PlayerMgmt.Get(player).fog.Clear(x,y);
        }
        else
        {
            PlayerMgmt.Get(player).fog.Clear(x,y,config.visible);
        }
        

        FinishInit();
    }

    private void FinishInit()
    {
        NextRound();
        gameObject.name = config.name;

        //show it
        GetComponent<SpriteRenderer>().sprite = config.GetIcon();
        GetComponent<Transform>().position = new Vector2(data.x+0.5f,data.y);
    }

    public void Load(UnitData data)
    {
        this.data = data;
        config = Data.unit[data.type];
        
        if (data.construction != null)
        {
            gameObject.AddComponent<Construction>();
            gameObject.GetComponent<Construction>().Load(data);
        }
        
        FinishInit();
    }

    public void MoveTo(int x, int y)
    {
        data.x += x;
        data.y += y;

        int dX = (int) GetComponent<Transform>().position.x + x;
        int dY = (int) GetComponent<Transform>().position.y + y;
        NTerrain land = MapMgmt.Get().GetTerrain(dX, dY);

        //check terrain
        int cost = TerrainHelper.GetMoveCost(land, config.movetyp, PlayerMgmt.ActPlayer().nation);
        if (cost == 0)
        {
            OnMapUI.Get().unitUI.SetPanelMessage($"Can not move in {land.name}, because it is not passable.");
            return;
        }

        //visible?
        if (!PlayerMgmt.Get(data.player).fog.visible[dX, dY])
        {
            OnMapUI.Get().unitUI.SetPanelMessage($"Can not move, the land is not explored.");
            return;
        }

        //another unit?
        if (UnitMgmt.At(dX, dY) != null)
        {
            OnMapUI.Get().unitUI
                .SetPanelMessage($"Can not move in {land.name}, because {UnitMgmt.At(dX, dY).name} standing their.");
            return;
        }

        //can walk
        if (cost > data.ap)
        {
            OnMapUI.Get().unitUI
                .SetPanelMessage($"Can not move in {land.name}, because you need {cost - data.ap} more ap.");
            return;
        }

        //move it
        Action<ITween<Vector3>> update = (t) => { gameObject.transform.position = t.CurrentValue; };

        Action<ITween<Vector3>> completed = (t) =>
        {
            OnMapUI.Get().UpdatePanelXY(dX, dY);
            //show it
            PlayerMgmt.Get(data.player).fog.Clear(dX, dY, config.visible);
        };

        //rotate
        if (x > 0)
        {
            GetComponent<SpriteRenderer>().sprite = config.GetIcon(7);
        }
        else if (x < 0)
        {
            GetComponent<SpriteRenderer>().sprite = config.GetIcon(4);
        } 
        else if (y < 0)
        {
            GetComponent<SpriteRenderer>().sprite = config.GetIcon(11);
        } 
        else 
        {
            GetComponent<SpriteRenderer>().sprite = config.GetIcon(1);
        }
    

    // completion defaults to null if not passed in
        gameObject.Tween("MoveUnit", gameObject.transform.position, new Vector2(dX+0.5f,dY), 1, TweenScaleFunctions.Linear, update, completed);
        data.ap -= cost;
    }
    
    public string GetStatus()
    {
        if (IsUnderConstrution())
        {
            return $"{gameObject.name} under construction ({(int) GetComponent<Construction>().GetConstructionProcent()}%) {data.lastError}";
        }
        return $"{gameObject.name} AP:{data.ap}/{config.ap} {data.lastError}";
    }

    public bool IsUnderConstrution()
    {
        return GetComponent<Construction>() != null;
    }

    /// <summary>
    /// Destroy this unit
    /// </summary>
    public void Kill()
    {
        GameMgmt.Get().data.units.Remove(data);
        
        Destroy(gameObject);
    }

    public void FinishConstruct()
    {
        
    }

    public Town GetTown()
    {
        return TownMgmt.Get().NearstTown(PlayerMgmt.Get(data.player), (int) GetComponent<Transform>().position.x,
            (int) GetComponent<Transform>().position.y, false);
    }
    
    public void ShowInfoWindow()
    {
        WindowBuilderSplit win = WindowBuilderSplit.Create(gameObject.name,null);
        win.AddElement(new UnitSplitInfo(this));
        win.AddElement(new UnitLexiconInfo(this));
        win.Finish();
    }

    class UnitSplitInfo : WindowBuilderSplit.SplitElement
    {
        private readonly UnitInfo _unit;
        
        public UnitSplitInfo(UnitInfo unit) : base(unit.gameObject.name,unit.config.GetIcon())
        {
            this._unit = unit;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabel("Information");
            panel.AddImageLabel($"HP: {_unit.data.hp}/{_unit.config.hp}","stats:hp");
            panel.AddImageLabel($"AP: {_unit.data.ap}/{_unit.config.ap}","stats:ap");
            
            Construction con = _unit.GetComponent<Construction>();
            if (con != null)
            {
                panel.AddRess("Under Construction",_unit.data.construction.ToDictionary(entry => entry.Key,entry => entry.Value));
                panel.AddLabel("Missing resources");
            }
        }

        public override void Perform()
        {
        }
    }

    class UnitLexiconInfo : WindowBuilderSplit.SplitElement
    {
        private readonly UnitInfo _unit;
        
        public UnitLexiconInfo(UnitInfo unit) : base("Lexicon",SpriteHelper.LoadIcon("magic:lexicon"))
        {
            this._unit = unit;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            _unit.config.ShowInfo(panel);
        }

        public override void Perform()
        {
        }
    }
}
