using Buildings;
using Game;
using Libraries;
using Libraries.Buildings;
using MapElements;
using MapElements.Material;
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
                var r = L.b.res[cost.Key];
                if (!go.baseData.cost.ContainsKey(cost.Key))
                {
                    r.AddImageLabel(panel, cost.Value);
                    found = true;
                    continue;
                }

                if (cost.Value > go.baseData.cost[cost.Key])
                {
                    r.AddImageLabel(panel, cost.Value-go.baseData.cost[cost.Key]);
                    found = true;
                }
            }
            
            if (!found)
                panel.AddLabel("No extra cost");
        }
        
        public override void Perform()
        {
            MaterialWindow.ShowBuildMaterialWindow(build, pos, cost =>
            {
                go.Upgrade(build.id, cost);
            });
            OnMapUI.Get().UpdatePanel(pos);
        }
    }
}