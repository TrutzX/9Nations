using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Buildings;
using Classes;
using GameMapLevels;
using InputActions;
using Libraries;
using Libraries.Rounds;
using Loading;
using MapElements;
using Maps;
using Options;
using Players;
using Tools;
using Towns;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameMgmt : MonoBehaviour
    {
        public GameRoundMgmt gameRound;
        public UnitMgmt unit;
        public BuildingMgmt building;
        
        public CameraMove cameraMove;
        public InputAction inputAction;
        
        public GameData data;
        public GameMap newMap;

        private static GameMgmt self;

        public static Dictionary<string, string> startConfig;

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
            yield return S.ActPlayer().FinishRound();
            yield return data.players.NextPlayer();
            load.FinishLoading();
        }
        
        void Start()
        {
            LClass.Init();
            load = GameObject.Find("UICanvas").GetComponentInChildren<LoadingScreen>(true);
            if (startConfig != null && startConfig["type"] == "load")
            {
                StartCoroutine(LoadGame());
                return;
            }
            StartCoroutine(BeginNewGame());
        }

        public void ReloadGameLib()
        {
            StartCoroutine(L.b.Reload());
        }
        
        IEnumerator BeginNewGame()
        {
            yield return load.ShowMessage("Loading");

            if (LSys.tem == null)
                yield return LSys.Init(load);
            else
                LSys.tem.Load = load;
            //Loading data
            yield return L.Init();
            data = new GameData();
            data.players = new PlayerMgmt();
            data.towns = new TownMgmt();
            data.buildings = new List<BuildingUnitData>();
            data.units = new List<BuildingUnitData>();
            data.features = new Dictionary<string, string>();
            data.map = new GameMapData();
            data.modi = new Dictionary<string, string>();
            
            //show it
            ConnectGameObjs();
            OptionHelper.RunStartOptions();
            data.players.Init();
 
            if (startConfig == null)
            {
                startConfig = new Dictionary<string, string>();
                startConfig["name"] = "debug";
                startConfig["type"] = "scenario";
                startConfig["scenario"] = "tutorial3";
                //startConfig["scenario"] = "screenshot";
                startConfig["scenario"] = "debug";
            }
            
            yield return load.ShowMessage("Loading "+startConfig["name"]);

            if (startConfig["type"] == "endless")
            {
                yield return (StartScenario("endless",startConfig["map"]));
            } 
            else
            {
                yield return (StartScenario(startConfig["scenario"],LSys.tem.scenarios[startConfig["scenario"]].map));
            }

        }

        private void ConnectGameObjs()
        {
            gameRound = ScriptableObject.CreateInstance<GameRoundMgmt>();
            //unit = GameObject.Find("UnitMgmt").GetComponent<UnitMgmt>();
            //building = GameObject.Find("BuildingMgmt").GetComponent<BuildingMgmt>();
            //newMap = FindObjectOfType<GameMap>();
            //cameraMove = FindObjectOfType<CameraMove>();
            self = this;
        }

        public IEnumerator StartScenario(string id, string map)
        {
            //load Map
            data.map.id = map;
            data.name = LSys.tem.scenarios[id].Name();
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
            
            //add modis
            foreach (var option in L.b.gameOptions.GetAllByCategory("modi"))
            {
                if (!startConfig.ContainsKey(option.id)) continue;
                
                var val = Convert.ToInt32(startConfig[option.id]);
                if (val == 0) continue;
                
                data.modi[option.id] = val+"%";
                Debug.Log(option.id+":"+val+"%");
            }
            
            yield return data.players.GameStart();
            yield return data.players.GameBegin();
            yield return data.players.NextPlayer();
            NAudio.Play("startgame");
            load.FinishLoading();
        }

        IEnumerator LoadGame()
        {
            yield return load.ShowMessage("Loading "+startConfig["file"]);
            yield return load.ShowSubMessage($"Loading library");
            L.b = ES3.Load<L>("lib",startConfig["file"]+"lib.9n");
            yield return load.ShowSubMessage($"Loading file");
            data = ES3.Load<GameData>("game",startConfig["file"]+"game.9n");
            
            ConnectGameObjs();
            
            //load Map
            yield return newMap.LoadMap();
            yield return load.ShowSubMessage($"Loading players");
            yield return data.players.GameLoaded();
            yield return data.players.GameBegin();

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
            S.ActPlayer().StartRound();
            NAudio.Play("startgame");
            load.FinishLoading();
        }
    }
}