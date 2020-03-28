using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using Buildings;
using Classes;
using GameMapLevels;
using Libraries;
using Libraries.Rounds;
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
        public GameRoundMgmt gameRound;
        public UnitMgmt unit;
        public BuildingMgmt building;
        
        [Obsolete("ss",true)] public XXGameMapMgmt map;
        
        public GameData data;
        public GameMap newMap;

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
            yield return PlayerMgmt.ActPlayer().FinishRound();
            yield return data.players.NextPlayer();
            load.FinishLoading();
        }
        
        void Start()
        {
            LClass.Init();
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
                yield return (StartScenario("endless",StartConfig["map"]));
            } 
            else
            {
                yield return (StartScenario(StartConfig["scenario"],L.b.scenarios[StartConfig["scenario"]].map));
            }

        }

        private void ConnectGameObjs()
        {
            gameRound = ScriptableObject.CreateInstance<GameRoundMgmt>();
            unit = GameObject.Find("UnitMgmt").GetComponent<UnitMgmt>();
            building = GameObject.Find("BuildingMgmt").GetComponent<BuildingMgmt>();
            newMap = FindObjectOfType<GameMap>();
            self = this;
        }

        public IEnumerator StartScenario(string id, string _map)
        {
            //load Map
            data.map.id = _map;
            data.name = L.b.scenarios[id].name;
            yield return newMap.CreateMap();
            
            yield return load.ShowSubMessage($"Loading players");
            try
            {
                LClass.s.scenarioRuns[id].Run();
            }
            catch (Exception e)
            {
                ExceptionHelper.ShowException(e);
                throw e;
            }
            
            yield return PlayerMgmt.Get().CreatingFog();
            PlayerMgmt.Get().FirstRound();
            yield return PlayerMgmt.Get().NextPlayer();
            NAudio.Play("startgame");
            load.FinishLoading();
        }

        IEnumerator LoadGame()
        {
            yield return load.ShowMessage("Loading game");
            yield return load.ShowSubMessage($"Loading library");
            L.b = ES3.Load<L>("lib",StartConfig["file"]+"lib.9n");
            yield return load.ShowSubMessage($"Loading game");
            data = ES3.Load<GameData>("game",StartConfig["file"]+"game.9n");
            
            ConnectGameObjs();
            
            //load Map
            yield return newMap.LoadMap();
            data.players.AfterLoad();
            yield return PlayerMgmt.Get().CreatingFog();

            //load buildings
            yield return load.ShowSubMessage($"Loading Buildings");
            foreach (BuildingUnitData bdata in data.buildings.ToArray())
            {
                building.Load(bdata);
            }
            

            //load units
            yield return load.ShowSubMessage($"Loading Units");
            foreach (BuildingUnitData udata in data.units)
            {
                unit.Load(udata);
            }
            
            //init data
            gameRound.Load();
            
            //init players
            Debug.Log(PlayerMgmt.ActPlayerID());
            PlayerMgmt.ActPlayer().StartRound();
            NAudio.Play("startgame");
            load.FinishLoading();
        }
    }
}