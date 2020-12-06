using System;
using Libraries.Options;

namespace Libraries.PlayerOptions
{
    [Serializable]
    public class PlayerOptionMgmt : OptionMgmt<PlayerOption>
    {
        public PlayerOptionMgmt() : base("playeroption") { }
    }
}