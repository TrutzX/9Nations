using System;
using Game;
using Libraries.Options;
using Players;
using UnityEngine;

namespace Libraries.PlayerOptions
{
    [Serializable]
    public class PlayerOption : Option
    {
        public override string Value()
        {
            return PlayerMgmt.ActPlayer().GetFeature(id);
        }

        public override bool Exist()
        {
            throw new NotImplementedException();
        }

        public override void SetValue(string value)
        {
            PlayerMgmt.ActPlayer().SetFeature(id, value);
        }
    }
}