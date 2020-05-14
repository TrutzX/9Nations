using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using Audio;
using Buildings;
using Classes;
using GameMapLevels;
using InputActions;
using Libraries;
using Libraries.Rounds;
using Loading;
using LoadSave;
using Maps;
using Options;
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
        
        public CameraMove cameraMove;
        public InputAction inputAction;
        
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
            
            //show it
            ConnectGameObjs();
            OptionHelper.RunStartOptions();

            if (StartConfig == null)
            {
                StartConfig = new Dictionary<string, string>();
                StartConfig["name"] = "debug";
                StartConfig["type"] = "scenario";
                StartConfig["scenario"] = "debug";
                //StartConfig["scenario"] = "pantheon";
            }
            
            yield return load.ShowMessage("Loading "+StartConfig["name"]);

            if (StartConfig["type"] == "endless")
            {
                yield return (StartScenario("endless",StartConfig["map"]));
            } 
            else
            {
                yield return (StartScenario(StartConfig["scenario"],LSys.tem.scenarios[StartConfig["scenario"]].map));
            }

        }

        public MapElementInfo At(NVector pos)
        {
            MapElementInfo info = S.Unit().At(pos);
            return info != null ? info : S.Building().At(pos);
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

        public IEnumerator StartScenario(string id, string _map)
        {
            //load Map
            data.map.id = _map;
            data.name = LSys.tem.scenarios[id].name;
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
            
            yield return S.Player().CreatingFog();
            S.Player().FirstRound();
            yield return S.Player().NextPlayer();
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
            yield return S.Player().CreatingFog();

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
            PlayerMgmt.ActPlayer().StartRound();
            NAudio.Play("startgame");
            load.FinishLoading();
        }
    }
}