using System;
using Buildings;
using Libraries.GameButtons;
using Players;
using UnityEngine;

namespace Libraries.FightModis
{
    [Serializable]
    public class FightModiMgmt : BaseMgmt<FightModi>
    {
        public FightModiMgmt() : base("fightmodi") { }

        protected override void ParseElement(FightModi ele, string header, string data)
        {
            switch (header)
            {
                case "modi":
                    ele.modi = Int(data);
                    break;
                default:
                    Debug.LogWarning($"{name} missing {header} for data {data}");
                    break;
            }
        }

        protected override FightModi Create()
        {
            return new FightModi();
        }
        
    }
}