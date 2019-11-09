using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using Buildings;
using LoadSave;
using Maps;
using Players;
using Tools;
using Towns;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Color = UnityEngine.Color;

namespace Game
{
    public class GameMgmt : MonoBehaviour
    {
        public RoundMgmt round;
        public UnitMgmt unit;
        public BuildingMgmt building;
        public MapMgmt map;
        public GameData data;

        private static GameMgmt self;

        public static Dictionary<string, string> startConfig;

        public GameObject loadingScreen;
        public Text mainText;
        public Text subText;
        
        private int loadScene = 0;

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
            PlayerMgmt.ActPlayer().FinishRound();
            data.players.NextPlayer();
        }
        
        void Update()
        {
            // If the player has pressed the space bar and a new scene is not loading yet...
            if (loadScene == 0) {

                // ...set the loadScene boolean to true to prevent loading a new scene more than once...
                loadScene = 1;

                // ...change the instruction text to read "Loading..."
                ShowLoadingScreenText("Loading");
                
                // ...and start a coroutine that will load the desired scene.
                StartCoroutine(Start2());

            }

            // If the new scene has started loading...
            if (loadScene == 1) {
                // ...then pulse the transparency of the loading text to let the player know that the computer is still working.
                mainText.color = new Color(mainText.color.r, mainText.color.g, mainText.color.b, Mathf.PingPong(Time.time, 1));
                subText.color = new Color(subText.color.r, subText.color.g, subText.color.b, Mathf.PingPong(Time.time, 1));
            }
        }

        public IEnumerator ShowLoadingScreenText(string text)
        {
            Debug.Log(text);
            loadingScreen.SetActive(true);
            mainText.text = text;
            yield return new WaitForSeconds(0.5f);
        }

        public IEnumerator ShowLoadingScreenSecond(string text)
        {
            subText.text = text;
            yield return new WaitForFixedUpdate();
        }
        
        IEnumerator Start2()
        {
            data = new GameData();
            data.players = new PlayerMgmt();
            data.towns = new TownMgmt();
            data.buildings = new List<BuildingUnitData>();
            data.units = new List<BuildingUnitData>();
            data.features = new Dictionary<string, string>();
            round = ScriptableObject.CreateInstance<RoundMgmt>();
            unit = GameObject.Find("UnitMgmt").GetComponent<UnitMgmt>();
            building = GameObject.Find("BuildingMgmt").GetComponent<BuildingMgmt>();
            map = GameObject.Find("MapMgmt").GetComponent<MapMgmt>();
            self = this;
            
            if (startConfig == null)
            {
                startConfig = new Dictionary<string, string>();
                startConfig["name"] = "debug";
                startConfig["type"] = "scenario";
                startConfig["scenario"] = "debug";
                startConfig["scenario"] = "tutorialbasic";
                
            }
            
            yield return ShowLoadingScreenText("Loading "+startConfig["name"]);
            
            switch (startConfig["type"])
            {
                case "endless":
                    yield return (StartScenario(Data.scenario.endless.id,startConfig["map"]));
                    break;
                case "load":
                    yield return StartLoad();
                    break;
                default:
                    yield return (StartScenario(startConfig["scenario"],Data.scenario[startConfig["scenario"]].map));
                    break;
            }

            loadScene = 2;
            loadingScreen.SetActive(false);
        }

        public IEnumerator StartScenario(string id, string _map)
        {
            //load Map
            data.mapfile = _map;
            yield return StartCoroutine(MapMgmt.Get().LoadMap());
            
            yield return ShowLoadingScreenSecond($"Loading players");
            try
            {
                NLib.Get().ScenarioRuns[id].Run();
                FinishStart();
            }
            catch (Exception e)
            {
                ExceptionHelper.ShowException(e);
                throw e;
            }
            
            yield return null;
        }

        IEnumerator StartLoad()
        {
            yield return ShowLoadingScreenSecond($"Loading File");
            data = ES3.Load<GameData>("game",startConfig["file"]);
            data.players.AfterLoad();
            
            //load Map
            yield return map.LoadMap();

            //load buildings
            yield return ShowLoadingScreenSecond($"Loading Buildings");
            foreach (BuildingUnitData bdata in data.buildings.ToArray())
            {
                BuildingMgmt.Get().Load(bdata);
            }
            

            //load units
            yield return ShowLoadingScreenSecond($"Loading Units");
            foreach (BuildingUnitData udata in data.units)
            {
                UnitMgmt.Get().Load(udata);
            }
            
            //init data
            round.Load();
            
            //init players
            map.FinishStart();
            PlayerMgmt.ActPlayer().StartRound();
            NAudio.Play("startgame");
        }

        void FinishStart()
        {
            map.FinishStart();
            PlayerMgmt.Get().FirstRound();
            PlayerMgmt.Get().NextPlayer();
            NAudio.Play("startgame");
        }
        
    }
}