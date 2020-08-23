using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using Game;
using LoadSave;
using Players;
using Towns;
using Units;
using UnityEngine;

namespace Libraries.Rounds
{
    public class GameRoundMgmt : ScriptableObject
    {

        private int _round;

        public int Round => _round;

        /// <summary>
        /// Get it
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public static GameRoundMgmt Get()
        {
            return GameMgmt.Get().gameRound;
        }

        public void Load()
        {
            _round = GameMgmt.Get().data.round;
        }
    
        public IEnumerator NextRound()
        {
            _round++;
            GameMgmt.Get().data.round = _round;
            yield return GameMgmt.Get().load.ShowMessage($"Prepare {GetRoundString()}");

            Town[] ts = GameMgmt.Get().data.towns.GetAll();
            int c = 0;
            int m = ts.Length;
            
            foreach (Town t in ts)
            {
                t.NextRound();
                if (c % 2 == 0)
                {
                    yield return GameMgmt.Get().load.ShowSubMessage($"Updating town ({c}/{m})");
                }
                c++;
            }

            BuildingInfo[] b = GameMgmt.Get().building.GetAll();
            c = 0;
            m = b.Length;
            
            foreach (BuildingInfo e in b)
            {
                e.NextRound();
                if (c % 10 == 0)
                {
                    yield return GameMgmt.Get().load.ShowSubMessage($"Updating building ({c}/{m})");
                }
                c++;
            }
        
            UnitInfo[] u = GameMgmt.Get().unit.GetAll();
            c = 0;
            m = u.Length;
        
            foreach (UnitInfo e in u)
            {
                e.NextRound();
                if (c % 10 == 0)
                {
                    yield return GameMgmt.Get().load.ShowSubMessage($"Updating units ({c}/{m})");
                }
                c++;
            }
        
            GameMgmt.Get().newMap.NextRound();
            SwitchNight();
        
            yield return S.Players().NextRound();
        
        }

        private void SwitchNight()
        {
            if (!LSys.tem.options["night"].Bool())
            {
                return;
            }
            
            //FindObjectOfType<LightingManager2D>().disableEngine = !IsDayTime("night");
            //LightingMainBuffer2D.ForceUpdate();
        }
        
        public string Icon()
        {
            return Icon(_round);
        }

        public string Icon(int round)
        {
            return GetRound(round).Icon;
        }

        public Round GetRound()
        {
            return GetRound(_round);
        }

        public Round GetRound(int round)
        {
            return L.b.rounds[round % L.b.rounds.Length];
        }
    
        public string GetRoundString()
        {
            return GetRoundString(_round);
        }

        public string GetRoundString(int round)
        {
            return $"{GetSeasonString()} {GetRound().daytime}, year {round/L.b.rounds.Length+1}";
        }

        public string GetSeasonString()
        {
            return GetRound().season;
        }

        public bool IsSeason(String s)
        {
            return GetSeasonString() == s;
        }

        public bool IsDayTime(String d)
        {
            return GetRound().daytime == d;
        }

        public Dictionary<string, string> Modi()
        {
            return GetRound().modi;
        }
    }
}
