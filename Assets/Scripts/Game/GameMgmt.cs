using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using Buildings;
using LoadSave;
using Players;
using Scenario;
using Towns;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        
        void Start()
        {
            ShowLoadingScreen("Loading");
            StartCoroutine("Start2");
        }

        public void ShowLoadingScreen(string text)
        {
            loadingScreen.SetActive(true);
            loadingScreen.GetComponentInChildren<Text>().text = text;
        }
        
        IEnumerator Start2()
        {
            data = new GameData();
            data.players = new PlayerMgmt();
            data.towns = new TownMgmt();
            data.buildings = new List<BuildingData>();
            data.units = new List<UnitData>();
            round = ScriptableObject.CreateInstance<RoundMgmt>();
            unit = GameObject.Find("UnitMgmt").GetComponent<UnitMgmt>();
            building = GameObject.Find("BuildingMgmt").GetComponent<BuildingMgmt>();
            map = GameObject.Find("MapMgmt").GetComponent<MapMgmt>();
            self = this;

            yield return null;
            
            if (startConfig == null)
            {
                StartDebug();
            } else
            switch (startConfig["type"])
            {
                case "endless":
                    StartEndless();
                    break;
                case "load":
                    StartLoad();
                    break;
            }

            loadingScreen.SetActive(false);
        }

        void StartEndless()
        {
            ShowLoadingScreen("Loading endless game");
            
            data.mapfile = startConfig["map"];
            
            //load Map
            MapMgmt.Get().LoadMap();
            
            //add player
            int id = 0;
            while (startConfig.ContainsKey(id + "name"))
            {
                //add player
                int pid = PlayerMgmt.Get().CreatePlayer(startConfig[id+"name"], startConfig[id+"nation"]);
                Player p = PlayerMgmt.Get(pid);
                UnitMgmt.Get().Create(pid,Data.nation[startConfig[id+"nation"]].leader, MapMgmt.Get().GetStartPos(startConfig[id + "nation"]));
            
                //add quests
                if (Boolean.Parse(startConfig[id+"winGold"]))
                {
                    p.quests.AddQuest(QuestHelper.Win().AddReq("ressMin","gold,1000"));
                }
            
                //add quests
                if (Boolean.Parse(startConfig[id+"loseKing"]))
                {
                    p.quests.AddQuest(QuestHelper.Lose().AddReq("maxUnitPlayer",Data.nation[startConfig[id+"nation"]].leader+",0"));
                }

                id++;
            }
            
            FinishStart();
        }

        void StartLoad()
        {
            ShowLoadingScreen("Loading "+startConfig["file"]);

            data = ES3.Load<GameData>("game",startConfig["file"]);
            data.players.AfterLoad();

            //load buildings
            foreach (BuildingData bdata in data.buildings.ToArray())
            {
                BuildingMgmt.Get().Load(bdata);
            }

            //load units
            foreach (UnitData udata in data.units)
            {
                UnitMgmt.Get().Load(udata);
            }
            
            //load Map
            MapMgmt.Get().LoadMap();
            
            //init data
            round.Load();
            
        }
        
        void StartDebug()
        {
            ShowLoadingScreen("Loading debug game");
            
            data.mapfile = "field6";
            
            //load Map
            MapMgmt.Get().LoadMap();
            
            int pid = PlayerMgmt.Get().CreatePlayer("userx", "north");
            UnitMgmt.Get().Create(pid,"nking", 5,5);
            UnitMgmt.Get().Create(pid,"nsoldier", MapMgmt.Get().GetStartPos("north"));
            UnitMgmt.Get().Create(pid,"nworker", MapMgmt.Get().GetStartPos("north"));
            int tid = TownMgmt.Get().Create(NGenTown.GetTownName("north"), pid, 6, 6);
            BuildingMgmt.Get().Create(tid, "ntemple",6, 5);
            
            PlayerMgmt.Get(pid).quests.AddQuest(QuestHelper.Win().AddReq("season","summer"));
            PlayerMgmt.Get(pid).quests.AddQuest(QuestHelper.Lose().AddReq("season","summer"));
            
            FinishStart();

            //LoadSaveMgmt.UpdateSave("autosave");
            //LoadSaveMgmt.LoadSave("autosave");

        }

        void FinishStart()
        {
            MapMgmt.Get().FinishStart();
            PlayerMgmt.Get().NextPlayer();
            NAudio.Play("startgame");
        }
        
    }
}