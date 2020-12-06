using System.Linq;
using Buildings;
using Classes.Actions.Addons;
using Game;
using Libraries;
using Libraries.Buildings;
using Libraries.Elements;
using Libraries.FActions;
using Libraries.FActions.General;
using MapElements;
using MapElements.Material;
using Players;
using Tools;
using UI;
using UI.Show;
using Units;

namespace Classes.Actions
{
    public class ActionBuild : BaseSelectElementAction<DataBuildingMgmt,DataBuilding>
    {
        public ActionBuild() : base("build"){}

        protected override DataBuildingMgmt Objects()
        {
            return L.b.buildings;
        }

        protected override SplitElement CreateSplitElement(DataBuilding build, MapElementInfo info, NVector pos, ISplitManager ism)
        {
            return new BaseSelectElementSplitElement<DataBuilding>(id, build, info, pos, ism, (mei, position) => 
            {
                MaterialWindow.ShowBuildMaterialWindow(build, pos, cost =>
                {
                    GameMgmt.Get().building.Create(S.Towns().NearestTown(S.ActPlayer(),pos,false).id, build.id, pos, cost);
                    OnMapUI.Get().UpdatePanel(pos);
                });
            });
        }
    }
}