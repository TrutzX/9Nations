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
                    base.ParseElement(ele, header, data);
                    break;
            }
        }
        
    }
}