using Buildings;
using Players;
using UnityEngine;

namespace reqs
{
    public class ReqFightElement : BaseReqFight
    {
        protected override string Name()
        {
            return "element";
        }

        protected override bool CheckMapElement(MapElementInfo mapElement, string sett)
        {
            return mapElement.Player().elements.Contains(sett);
        }
    }
}