using Game;
using MapElements;
using Players;
using Tools;

namespace reqs
{
    
    public class ReqBuildingCategory : BaseReqOnlyMapElement
    {
        public override bool Check(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            if (onMap == null || !onMap.IsBuilding())
                onMap = S.Building(pos);
            
            return onMap != null && onMap.baseData.category.Contains(sett);
        }

        public override bool Final(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            return true;
        }

        public override string Desc(Player player, MapElementInfo onMap, string sett, NVector pos)
        {
            if (onMap == null || !onMap.IsBuilding())
                onMap = S.Building(pos);
            
            return Desc(player, sett)+(onMap==null?S.T("reqBuildingCategoryNone"):S.T("reqBuildingCategoryHere",onMap.baseData.category));
        }

        public override string Desc(Player player, string sett)
        {
            //check building
            return S.T("reqBuildingCategory", S.T(sett));
        }
    }
}