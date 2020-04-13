using System;
using System.Collections.Generic;
using Game;
using Libraries;
using Libraries.Modifiers;
using Players;
using UnityEngine;

namespace Modifiers
{
    [Serializable]
    public class ModifierMgmt : BaseMgmt<Modifier>
    {
        public Dictionary<string, BaseModifierCalc> Classes;
        public ModifierMgmt() : base("modifier")
        {
            Classes = new Dictionary<string, BaseModifierCalc>();
            Classes.Add("base",new BaseModifierCalc());
            Classes.Add("t",new TerrainModifierCalc());
        }
    }
}