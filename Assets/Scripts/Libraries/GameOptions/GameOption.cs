using System;
using Game;
using Libraries.Options;
using UnityEngine;

namespace Libraries.GameOptions
{
    [Serializable]
    public class GameOption : Option
    {
        public override string Value()
        {
            if (GameHelper.IsGame() && GameMgmt.Get().data.features.ContainsKey(id))
            {
                return GameMgmt.Get().data.features[id];
            }
            if (!GameHelper.IsGame() && L.b.gameOptions.startConfig.ContainsKey(id))
            {
                return L.b.gameOptions.startConfig[id];
            }

            return standard;
        }

        public override void SetValue(string value)
        {
            if (GameHelper.IsGame())
                GameMgmt.Get().data.features[id] = value;
            else
            {
                L.b.gameOptions.startConfig[id] = value;
            }
        }
    }
}