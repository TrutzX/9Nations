using Buildings;
using Game;
using Libraries.Buildings;
using Tools;
using UI;

namespace Classes.Actions.Addons
{
    public class BuildUpgradeSplitElement : BuildSplitElement
    {
        public BuildUpgradeSplitElement(BaseDataBuildingUnit build, MapElementInfo go, NVector pos, ISplitManager ism) : base(build, go, pos, ism)
        {
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            base.ShowDetail(panel);
            //show cost differences
            panel.AddHeaderLabel("Real cost");
            bool found = false;
            
            foreach (var cost in build.cost)
            {
                if (!go.baseData.cost.ContainsKey(cost.Key))
                {
                    panel.AddRes(cost.Key, cost.Value);
                    found = true;
                    continue;
                }

                if (cost.Value > go.baseData.cost[cost.Key])
                {
                    panel.AddRes(cost.Key, cost.Value-go.baseData.cost[cost.Key]);
                    found = true;
                }
            }
            
            if (!found)
                panel.AddLabel("No extra cost");
        }
        
        public override void Perform()
        {
            go.Upgrade(build.id);
            OnMapUI.Get().UpdatePanel(pos);
        }
    }
}