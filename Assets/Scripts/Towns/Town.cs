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
        [SerializeField] public int playerID;
        [SerializeField] public int id;
        [SerializeField] public int x, y;
        [SerializeField] public int level;

        [SerializeField] private Dictionary<string, int> ress;

        /// <summary>
        /// Only for loading
        /// </summary>
        public Town()
        {
            
        }
        
        public Town(int id, int playerID, string name, int x, int y)
        {
            this.id = id;
            this.playerID = playerID;
            this.name = name;
            this.x = x;
            this.y = y;
            level = 1;
            
            ress = new Dictionary<string, int>();
        }

        public void AddRess(string id, int amount)
        {
            if (!ress.ContainsKey(id))
            {
                ress[id] = amount;
            } else
                ress[id] += amount;
            
            //check
            if (ress[id] < 0)
            {
                throw new ArgumentException($"Ress {id} is negativ with {ress[id]}");
            }
        }

        public int GetRess(string id)
        {
            return ress.ContainsKey(id)?ress[id]:0;
        }
        
        public string GetTownLevelName()
        {
            Nation n = Data.nation[PlayerMgmt.Get(playerID).nation];
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
            panel.AddRess("Ressources",ress);
        }

        public Player Player()
        {
            return PlayerMgmt.Get(playerID);
        }
    }
}