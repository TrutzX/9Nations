using System;
using System.Collections.Generic;

namespace Libraries.Modifiers
{
    [Serializable]
    public class ModifierMgmt : BaseMgmt<Modifier>
    {
        public Dictionary<string, BaseModifierCalc> classes;
        public ModifierMgmt() : base("modifier")
        {
            classes = new Dictionary<string, BaseModifierCalc>();
            classes.Add("base",new BaseModifierCalc());
            classes.Add("t",new TerrainModifierCalc());
        }
    }
}