using System;
using System.Linq;
using Buildings;
using GameMapLevels;
using InputActions;
using Libraries;
using Libraries.Rounds;
using Players;
using Tools;
using Towns;
using Units;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public static class S
    {
        public static PlayerMgmt Players()
        {
            return GameMgmt.Get().data.players;
        }

        public static Player Player(int id)
        {
            return Players().Get(id);
        }
        
        public static Player ActPlayer()
        {
            return Players().Get(Players().ActPlayer);
        }

        public static int ActPlayerID()
        {
            return Players().ActPlayer;
        }
        public static TownMgmt Towns()
        {
            return GameMgmt.Get().data.towns;
        }

        public static Town Town(int tid)
        {
            return GameMgmt.Get().data.towns.Get(tid);
        }
        
        public static GameRoundMgmt Round()
        {
            return GameMgmt.Get().gameRound;
        }
        
        public static BuildingMgmt Building()
        {
            return GameMgmt.Get().building;
        }
        
        public static UnitMgmt Unit()
        {
            return GameMgmt.Get().unit;
        }
        
        public static GameMgmt Game()
        {
            return GameMgmt.Get();
        }
        
        public static GameMap Map()
        {
            return GameMgmt.Get().newMap;
        }

        public static InputAction InputAction()
        {
            return GameMgmt.Get().inputAction;
        }

        public static CameraMove CameraMove()
        {
            return GameMgmt.Get().cameraMove;
        }

        public static bool Debug()
        {
            return LSys.tem.options["debug"].Bool();
        }

        public static bool Advanced()
        {
            return LSys.tem.options["advanced"].Bool();
        }

        public static bool Fog()
        {
            return L.b.gameOptions["fog"].Bool();
        }

        public static string T(string key, object p0=null, object p1=null)
        {
            if (p0 != null)
                return LSys.tem.translations.Translate(key, p0, p1);
            return LSys.tem.translations.Translate(key);
        }

        public static bool Valid(int x, int y)
        {
            return !(y < 0 || x < 0 || y >= GameMgmt.Get().data.map.height || x >= GameMgmt.Get().data.map.width);
        }

        public static bool Valid(NVector pos)
        {
            return Valid(pos.x, pos.y);
        }

        public static bool IsGame()
        {
            return SceneManager.GetActiveScene().buildIndex == 2;
        }
    }
}