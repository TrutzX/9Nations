using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using Buildings;
using Libraries;
using Loading;
using LoadSave;
using Maps;
using Players;
using Tools;
using Towns;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Color = UnityEngine.Color;

namespace Game
{
    public class GameMgmt : MonoBehaviour
    {
        public RoundMgmt round;
        public UnitMgmt unit;
        public BuildingMgmt building;
        public GameMapMgmt map;
        public GameData data;

        private static GameMgmt self;

        public static Dictionary<string, string> StartConfig;

        public LoadingScreen load;

        public static void Init()
        {
            //in the right scene?
            if (SceneManager.GetActiveScene().buildIndex != 2)
            {
                SceneManager.LoadScene(2);
            }
        }

        public static GameMgmt Get()
        {
            return self;
        }

        public void NextPlayer()
        {
            StartCoroutine(NextPlayerCo());
        }

        IEnumerator NextPlayerCo()
        {
            yield return load.ShowMessage("Finish turn");
            PlayerMgmt.ActPlayer().FinishRound();
            yield return data.players.NextPlayer();
            load.FinishLoading();
        }
        
        void Start()
        {
            load = GameObject.Find("UICanvas").GetComponentInChildren<LoadingScreen>(true);
            if (StartConfig != null && StartConfig["type"] == "load")
            {
                StartCoroutine(LoadGame());
                return;
            }
            StartCoroutine(BeginNewGame());
        }
        
        IEnumerator BeginNewGame()
        {
            yield return load.ShowMessage("Loading");

            //Loading data
            yield return L.Init(load);
            data = new GameData();
            data.players = new PlayerMgmt();
            data.towns = new TownMgmt();
            data.buildings = new List<BuildingUnitData>();
            data.units = new List<BuildingUnitData>();
            data.features = new Dictionary<string, string>();
            data.map = new GameMapData();
            
            ConnectGameObjs();

            if (StartConfig == null)
            {
                StartConfig = new Dictionary<string, string>();
                StartConfig["name"] = "debug";
                StartConfig["type"] = "scenario";
                StartConfig["scenario"] = "debug";
                //StartConfig["scenario"] = "tutorialbasic";
            }
            
            yield return load.ShowMessage("Loading "+StartConfig["name"]);

            if (StartConfig["type"] == "endless")
            {
                yield return (StartScenario(Data.scenario.endless.id,StartConfig["map"]));
            } 
            else
            {
                yield return (StartScenario(StartConfig["scenario"],Data.scenario[StartConfig["scenario"]].map));
            }

        }

        private void ConnectGameObjs()
        {
            round = ScriptableObject.CreateInstance<RoundMgmt>();
            unit = GameObject.Find("UnitMgmt").GetComponent<UnitMgmt>();
            building = GameObject.Find("BuildingMgmt").GetComponent<BuildingMgmt>();
            map = GameObject.Find("MapMgmt").GetComponent<GameMapMgmt>();
            self = this;
        }

        public IEnumerator StartScenario(string id, string _map)
        {
            //load Map
            data.map.id = _map;
            yield return map.CreateMap();
            
            yield return load.ShowSubMessage($"Loading players");
            try
            {
                NLib.Get().ScenarioRuns[id].Run();
                
            }
            catch (Exception e)
            {
                ExceptionHelper.ShowException(e);
                throw e;
            }
            
            PlayerMgmt.Get().FirstRound();
            yield return PlayerMgmt.Get().NextPlayer();
            NAudio.Play("startgame");
            load.FinishLoading();
        }

        IEnumerator LoadGame()
        {
            yield return load.ShowMessage("Loading game");
            yield return load.ShowSubMessage($"Loading library");
            L.b = ES3.Load<L>("lib",StartConfig["file"]);
            yield return load.ShowSubMessage($"Loading game");
            data = ES3.Load<GameData>("game",StartConfig["file"]);
            
            ConnectGameObjs();
            
            data.players.AfterLoad();
            
            //load Map
            yield return map.LoadMap();

            //load buildings
            yield return load.ShowSubMessage($"Loading Buildings");
            foreach (BuildingUnitData bdata in data.buildings.ToArray())
            {
                BuildingMgmt.Get().Load(bdata);
            }
            

            //load units
            yield return load.ShowSubMessage($"Loading Units");
            foreach (BuildingUnitData udata in data.units)
            {
                UnitMgmt.Get().Load(udata);
            }
            
            //init data
            round.Load();
            
            //init players
            PlayerMgmt.ActPlayer().StartRound();
            NAudio.Play("startgame");
            load.FinishLoading();
        }
    }
}