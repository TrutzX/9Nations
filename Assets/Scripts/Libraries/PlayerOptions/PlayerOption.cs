using System;
using Game;
using Libraries.Options;
using UnityEngine;

namespace Libraries.PlayerOptions
{
    [Serializable]
    public class PlayerOption : Option
    {
        public override string ToString()
        {
            return standard;
        }

        public override void SetValue(string value)
        {
            Debug.LogWarning("Not implemented");
        }
    }
}