using System;
using System.Collections.Generic;
using DataTypes;
using Game;
using Libraries;
using Players;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Nation = Nations.Nation;

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
            return Player().Nation().TownNameLevel[level - 1];
        }

        public Sprite GetIcon()
        {
            return SpriteHelper.Load("Icons/base:foundTown");
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