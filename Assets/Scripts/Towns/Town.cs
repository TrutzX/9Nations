using System;
using System.Collections.Generic;
using DataTypes;
using Game;
using Players;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Towns
{
    [Serializable]
    public class Town
    {
        [SerializeField] public string name;
        [SerializeField] public int playerId;
        [SerializeField] public int id;
        [SerializeField] public int x, y;
        [SerializeField] public int level;

        [SerializeField] private Dictionary<string, int> res;

        /// <summary>
        /// Only for loading
        /// </summary>
        public Town(){}
        
        public Town(int id, int playerId, string name, int x, int y)
        {
            this.id = id;
            this.playerId = playerId;
            this.name = name;
            this.x = x;
            this.y = y;
            level = 1;
            
            res = new Dictionary<string, int>();
        }

        public void AddRes(string id, int amount)
        {
            if (!res.ContainsKey(id))
            {
                res[id] = amount;
            } else
                res[id] += amount;
            
            //check
            if (res[id] < 0)
            {
                throw new ArgumentException($"Res {id} is negative with {res[id]}");
            }
        }

        public int GetRes(string id)
        {
            return res.ContainsKey(id)?res[id]:0;
        }
        
        public string GetTownLevelName()
        {
            Nation n = Data.nation[PlayerMgmt.Get(playerId).nation];
            switch (level)
            {
                case 1:
                    return n.townLevelName1;
                case 2:
                    return n.townLevelName1;
                case 3:
                    return n.townLevelName1;
                case 4:
                    return n.townLevelName1;
                default:
                    return $"Level {level} for {n.name} unknown";
            }
        }

        public Sprite GetIcon()
        {
            return SpriteHelper.LoadIcon("base:foundTown");
        }

        public void ShowInfo(PanelBuilder panel)
        {
            panel.AddImageLabel(name, GetIcon());
            //todo panel.AddInput("town name");
            panel.AddLabel(GetTownLevelName());
            panel.AddRess("Resources",res);
        }

        public Player Player()
        {
            return PlayerMgmt.Get(playerId);
        }
    }
}