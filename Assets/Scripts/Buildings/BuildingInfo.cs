using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Buildings;
using DataTypes;
using Game;
using Players;
using reqs;
using Towns;
using UI;
using UnityEngine;


public class BuildingInfo : MonoBehaviour, IMapElement
{

    public Building config;

    private String lastError;

    public BuildingData data;

    public void NextRound()
    {
        lastError = null;
        Town t = GetTown();
        
        //under construction?
        if (IsUnderConstrution() && GetComponent<Construction>().RoundConstruct())
        {
            return;
        }
        
        //produce?
        if (!ReqHelper.Check(PlayerMgmt.Get(t.playerID),config.GenProduceReq(),gameObject,(int)transform.position.x,(int)transform.position.y))
        {
            lastError = ReqHelper.Desc(PlayerMgmt.Get(t.playerID), config.GenProduceReq(),
                gameObject, (int) transform.position.x, (int) transform.position.y);
            return;
        }
        
        foreach (KeyValuePair<string, int> ress in config.GenProduce())
        {
            //give ressources
            t.AddRess(ress.Key,ress.Value);
        }
    }

    

    public void Init(int town, string configType, int x, int y)
    {
        Debug.Log($"Create building {configType} at {x},{y} for town {town}");
        data = new BuildingData();
        data.Init(configType, town);
        data.x = x;
        data.y = y;
        config = Data.building[configType];

        gameObject.AddComponent<Construction>();
        gameObject.GetComponent<Construction>().Init(data,config.GenCost(),this,config.buildtime,town);
        
        PlayerMgmt.Get(GetTown().playerID).fog.Clear(x,y);

        FinishInit();
        
    }

    private void FinishInit()
    {
        NextRound();
        gameObject.name = config.name;

        //show it
        GetComponent<SpriteRenderer>().sprite = SpriteHelper.Load("Building/"+config.file);
        GetComponent<Transform>().position = new Vector2(data.x,data.y);
    }

    public void Load(BuildingData data)
    {
        this.data = data;
        config = Data.building[data.type];

        if (data.construction != null)
        {
            gameObject.AddComponent<Construction>();
            gameObject.GetComponent<Construction>().Load(data);
        }
        
        FinishInit();
    }
    
    public string GetStatus()
    {
        if (IsUnderConstrution())
        {
            return $"{gameObject.name} under construction ({(int) GetComponent<Construction>().GetConstructionProcent()}%) {lastError}";
        }
        return $"{gameObject.name} {lastError}";
    }

    public bool IsUnderConstrution()
    {
        return GetComponent<Construction>() != null;
    }

    /// <summary>
    /// Destroy it
    /// </summary>
    public void Kill()
    {
        GameMgmt.Get().data.buildings.Remove(data);
        
        //todo get ress back
        Destroy(gameObject);
        
        
    }

    public void FinishConstruct()
    {
        Town t = GetTown();
        foreach (KeyValuePair<string, int> ress in config.GenProduceOnce())
        {
            //give ressources
            t.AddRess(ress.Key,ress.Value);
        }
        
        //show it
        t.Player().fog.Clear(data.x, data.y, config.visible);
    }

    public Town GetTown()
    {
        return TownMgmt.Get(data.town);
    }

    public void ShowInfoWindow()
    {
        WindowBuilderSplit win = WindowBuilderSplit.Create(gameObject.name,null);
        win.AddElement(new BuildingSplitInfo(this));
        win.AddElement(new BuildingLexiconInfo(this));
        win.Finish();
    }

    class BuildingSplitInfo : WindowBuilderSplit.SplitElement
    {
        private readonly BuildingInfo _building;
        
        public BuildingSplitInfo(BuildingInfo unit) : base(unit.gameObject.name,unit.config.GetIcon())
        {
            this._building = unit;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabel("Information");
            panel.AddImageLabel($"HP: {_building.data.hp}/{_building.config.hp}","stats:hp");
            panel.AddImageLabel($"AP: {_building.data.ap}/{_building.config.ap}","stats:ap");
            
            Construction con = _building.GetComponent<Construction>();
            if (con != null)
            {
                panel.AddRess("Under Construction",_building.data.construction.ToDictionary(entry => entry.Key,entry => entry.Value));
                panel.AddLabel("Missing resources");
            }
        }

        public override void Perform()
        {
        }
    }

    class BuildingLexiconInfo : WindowBuilderSplit.SplitElement
    {
        private readonly BuildingInfo _unit;
        
        public BuildingLexiconInfo(BuildingInfo unit) : base("Lexicon",SpriteHelper.LoadIcon("magic:lexicon"))
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

    class BuildingDebugInfo : WindowBuilderSplit.SplitElement
    {
        private readonly BuildingInfo _unit;
        
        public BuildingDebugInfo(BuildingInfo unit) : base("Debug",SpriteHelper.LoadIcon("ui:debug"))
        {
            this._unit = unit;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            Construction con = _unit.GetComponent<Construction>();

            if (con != null)
            {
                
            }
            else
            {
                panel.AddLabel("Under construction: none");
            }
        }

        public override void Perform()
        {
        }
    }
}
