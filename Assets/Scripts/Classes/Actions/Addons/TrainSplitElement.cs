using Game;
using Libraries.Buildings;
using Players;
using Tools;
using UI;

namespace Classes.Actions.Addons
{
    public class TrainSplitElement : BuildSplitElement
    {
        public TrainSplitElement(BaseDataBuildingUnit build, Buildings.MapElementInfo go, NVector pos, ISplitManager ism) : base(build, go, pos, ism)
        {
        }

        public override void Perform()
        {
            UpdatePref("lastTrain");
            GameMgmt.Get().unit.Create(S.Towns().NearestTown(S.ActPlayer(),pos,false).id, build.id, pos);
            OnMapUI.Get().UpdatePanel(pos);
        }
    }
}