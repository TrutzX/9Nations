using Buildings;
using MapElements;
using Players;
using UnityEngine;

namespace reqs
{
    public class ReqFightNationEthos : BaseReqFight
    {
        protected override string Name()
        {
            return "nation ethos";
        }

        protected override bool CheckMapElement(MapElementInfo mapElement, string sett)
        {
            return mapElement.Player().Nation().Ethos == sett;
        }
    }
}