using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Buildings;
using DataTypes;
using Game;
using Help;
using Improvements;
using Libraries;
using MapActions;
using Players;
using reqs;
using Tools;
using Towns;
using UI;
using UnityEngine;


public class BuildingInfo : MapElementInfo
{
    public Building config;
    
    public void NextRound()
    {
        data.lastInfo = null;
        data.BuildingUpdate();
        Town t = Town();
        
        //under construction?
        if (IsUnderConstrution() && GetComponent<Construction>().RoundConstruct())
        {
            return;
        }
        
        //produce?
        if (!ReqHelper.Check(Player(),config.GenProduceReq(),this,X(),Y()))
        {
            SetLastInfo( ReqHelper.Desc(Player(), config.GenProduceReq(),
                this, X(), Y()));
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
        //TODO Vector3Int Z
        int buildtime = L.b.modifiers["build"].CalcModi(config.buildtime, TownMgmt.Get(town).Player(), new Vector3Int(x, y, 0));

        gameObject.AddComponent<Construction>();
        gameObject.GetComponent<Construction>().Init(data,config.GenCost(),this,buildtime);
        
        PlayerMgmt.Get(Town().playerId).fog.Clear(x,y);

        FinishInit();
        
    }

    private void FinishInit()
    {
        NextRound();
        gameObject.name = config.name;

        //show it
        GetComponent<Transform>().position = new Vector2(data.x,data.y);

        if (!string.IsNullOrEmpty(config.connected))
        {
            SetConnectedImage();
            BuildingMgmt.At(VectorHelper.Add(Pos(), 0, 1))?.SetConnectedImage();
            BuildingMgmt.At(VectorHelper.Add(Pos(), 1, 0))?.SetConnectedImage();
            BuildingMgmt.At(VectorHelper.Add(Pos(), 0, -1))?.SetConnectedImage();
            BuildingMgmt.At(VectorHelper.Add(Pos(), -1, 0))?.SetConnectedImage();
            return;
        }
        
        GetComponent<SpriteRenderer>().sprite = SpriteHelper.Load("Building/"+config.file);
    }

    public void SetConnectedImage()
    {
        if (config.connected == "wall")
        {
            bool north = BuildingMgmt.At(VectorHelper.Add(Pos(), 0, 1))?.config.connected == config.connected;
            bool east = BuildingMgmt.At(VectorHelper.Add(Pos(), 1, 0))?.config.connected == config.connected;
            bool south = BuildingMgmt.At(VectorHelper.Add(Pos(), 0, -1))?.config.connected == config.connected;
            bool west = BuildingMgmt.At(VectorHelper.Add(Pos(), -1, 0))?.config.connected == config.connected;

            string f = config.file.Replace("14", ImprovementHelper.GetId(north, east, south, west)+"");
            GetComponent<SpriteRenderer>().sprite = SpriteHelper.Load("Building/"+f);
        }
            
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
        
        //reset images?
        if (!string.IsNullOrEmpty(config.connected))
        {
            BuildingMgmt.At(VectorHelper.Add(Pos(), 0, 1))?.SetConnectedImage();
            BuildingMgmt.At(VectorHelper.Add(Pos(), 1, 0))?.SetConnectedImage();
            BuildingMgmt.At(VectorHelper.Add(Pos(), 0, -1))?.SetConnectedImage();
            BuildingMgmt.At(VectorHelper.Add(Pos(), -1, 0))?.SetConnectedImage();
        }
    }

    public override string UniversalImage()
    {
        return "b:" + config.id;
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
        Player().fog.Clear(data.x, data.y, L.b.modifiers["view"].CalcModiNotNull(config.visible,Player(),Pos()));
        
        //perform actions
        foreach (KeyValuePair<string,string> key in config.GetActionsOnce())
        {
            NLib.GetAction(key.Key).BackgroundRun(this, data.x, data.y, key.Value);
        }
    }

    public override WindowBuilderSplit ShowInfoWindow()
    {
        WindowBuilderSplit win = base.ShowInfoWindow();
        win.AddElement(new HelpSplitElement("building"), true);
        win.AddElement(new BuildingLexiconInfo(this), true);
        win.AddElement(new BuildingSplitInfo(this), true);
        win.Finish();
        return win;
    }

    public override void Upgrade(string type)
    {
        GameMgmt.Get().data.buildings.Remove(data);
        Init(data.townId,type,X(),Y());
        GameMgmt.Get().data.buildings.Add(GetComponent<BuildingInfo>().data);
    }

    class BuildingSplitInfo : SplitElement
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

    class BuildingLexiconInfo : SplitElement
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
