using System;
using System.Collections.Generic;
using Game;
using Libraries;
using Players;
using Terrains;
using UnityEngine;

namespace Modifiers
{
    [Serializable]
    public class ModifierMgmt : BaseMgmt<Modifier>
    {
        public Dictionary<string, BaseModifierCalc> Classes;
        public ModifierMgmt()
        {
            name = "Modifier";
            Classes = new Dictionary<string, BaseModifierCalc>();
            Classes.Add("base",new BaseModifierCalc());
            Classes.Add("t",new TerrainModifierCalc());
        }
        
        protected override void ParseElement(Modifier ele, string header, string data)
        {
            switch (header)
            {
                default:
                    Debug.LogWarning($"{name} missing {header} for data {data}");
                    break;
            }
        }
        
        protected override Modifier Create()
        {
            return new Modifier();
        }
    }
}