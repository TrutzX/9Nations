using Buildings;
using GameMapLevels;
using InputActions;
using Libraries;
using Libraries.Rounds;
using Towns;
using Units;
using UnityEngine;

namespace Game
{
    public static class S
    {
        public static TownMgmt Towns()
        {
            return GameMgmt.Get().data.towns;
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
            return GameObject.Find("InputAction").GetComponent<InputAction>();
        }

        public static bool Debug()
        {
            return LSys.tem.options["debug"].Bool();
        }

        public static bool Fog()
        {
            return L.b.gameOptions["fog"].Bool();
        }
    }
}