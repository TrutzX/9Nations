using System;
using Libraries.MapGenerations;
using UnityEngine;

namespace Libraries.Movements
{
    [Serializable]
    public class MovementMgmt : BaseMgmt<Movement>
    {
        public MovementMgmt() : base("movement")
        {
        }

        protected override void ParseElement(Movement ele, string header, string data)
        {
            switch (header)
            {
                case "def":
                    ele.def = Int(data);
                    break;
                default:
                    Debug.LogWarning($"{name} missing {header} for data {data}");
                    break;
            }
        }

        protected override Movement Create()
        {
            return new Movement();
        }
    }
}