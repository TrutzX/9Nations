using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Buildings;
using DataTypes;
using Game;
using Help;
using Players;
using reqs;
using Towns;
using UI;
using UnityEngine;


public class BuildingInfo : MapElementInfo
{
    public Building config;

    public void NextRound()
    {
        data.lastError = null;
        data.BuildingUpdate();
        Town t = Town();
        
        //under construction?
        if (IsUnderConstrution() && GetComponent<Construction>().RoundConstruct())
        {
            return;
        }
        
        //produce?
        if (!ReqHelper.Check(Player(),config.GenProduceReq(),gameObject,X(),Y()))
        {
            data.lastError = ReqHelper.Desc(Player(), config.GenProduceReq(),
                gameObject, X(), Y());
            return;
        }
        
        foreach (KeyValuePair<string, int> ress in config.GenProduce())
        {
            //give ressources
            t.AddRes(ress.Key,ress.Value);
        }
    }

    

    public void Init(int town, string configType, int x, int y)
    {
        Debug.Log($"Create building {configType} at {x},{y} for town {town}");
        data = new BuildingUnitData();
        data.BuildingInit(configType, town);
        data.x = x;
        data.y = y;
        config = Data.building[configType];

        gameObject.AddComponent<Construction>();
        gameObject.GetComponent<Construction>().Init(data,config.GenCost(),this,config.buildtime,town);
        
        PlayerMgmt.Get(Town().playerId).fog.Clear(x,y);

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

    public override void Load(BuildingUnitData data)
    {
        config = Data.building[data.type];
        base.Load(data);
        
        FinishInit();
    }
    

    /// <summary>
    /// Destroy it
    /// </summary>
    public override void Kill()
    {
        GameMgmt.Get().data.buildings.Remove(data);
        
        //todo get ress back
        Destroy(gameObject);
        
        
    }

    public override void FinishConstruct()
    {
        Town t = Town();
        foreach (KeyValuePair<string, int> ress in config.GenProduceOnce())
        {
            //give ressources
            t.AddRes(ress.Key,ress.Value);
        }
        
        //show it
        t.Player().fog.Clear(data.x, data.y, config.visible);
        
        //perform actions
        foreach (KeyValuePair<string,string> key in config.GetActionsOnce())
        {
            NLib.GetAction(key.Key).BackgroundRun(this, data.x, data.y, key.Value);
        }
    }

    public override WindowBuilderSplit ShowInfoWindow()
    {
        WindowBuilderSplit win = base.ShowInfoWindow();
        win.AddElement(new BuildingSplitInfo(this));
        win.AddElement(new BuildingLexiconInfo(this));
        win.AddElement(new HelpSplitElement("building"));
        win.Finish();
        return win;
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
            
            //diff unit?
            if (_building.Town().playerId != PlayerMgmt.ActPlayerID())
            {
                panel.AddLabel($"Owner: {_building.Town().Player().name}");
                panel.AddImageLabel($"HP: ??/{_building.config.hp}","stats:hp");
                panel.AddImageLabel($"AP: ??/{_building.config.ap}","stats:ap");
                return;
            }
            
            panel.AddImageLabel($"HP: {_building.data.hp}/{_building.data.hpMax}","stats:hp");
            panel.AddImageLabel($"AP: {_building.data.ap}/{_building.data.apMax}","stats:ap");
            
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
}
