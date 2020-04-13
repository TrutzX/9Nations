using System;
using System.Collections.Generic;
using System.Linq;
using Libraries.Options;
using UnityEngine;

namespace Libraries.GameOptions
{
    [Serializable]
    public class GameOptionMgmt : OptionMgmt<GameOption>
    {
        [NonSerialized] public Dictionary<string, string> startConfig;
        
        public GameOptionMgmt() : base("gameoption") { }
    }
}