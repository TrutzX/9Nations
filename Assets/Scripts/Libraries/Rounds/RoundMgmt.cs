using System;
using UnityEngine;

namespace Libraries.Rounds
{
    [Serializable]
    public class RoundMgmt : BaseMgmt<Round>
    {
        public RoundMgmt() : base("round") { }

        public Round this[int id] => this[id.ToString()];

        protected override void ParseElement(Round ele, string header, string data)
        {
            switch (header)
            {
                case "modi":
                    Delimiter(ele.modi, data);
                    break;
                case "season":
                    ele.season = data;
                    break;
                case "daytime":
                    ele.daytime = data;
                    break;
                default:
                    Debug.LogWarning($"{name} missing {header} for data {data}");
                    break;
            }
        }

        protected override Round Create()
        {
            return new Round();
        }
    }
}