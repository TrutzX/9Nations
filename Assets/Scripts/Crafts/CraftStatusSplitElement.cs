using System.Collections.Generic;
using Buildings;
using Classes.Actions.Addons;
using Game;
using Libraries;
using Libraries.Buildings;
using Libraries.Crafts;
using Libraries.FActions;
using Tools;
using UI;
using UI.Show;
using UnityEngine.UI;

namespace Crafts
{
    public class CraftStatusSplitElement : SplitElement
    {
        private MapElementInfo _info;
        private ActionHolder _holder;
        
        public CraftStatusSplitElement(MapElementInfo info, ActionHolder holder) : base("Status", "!Icons/icons:craft")
        {
            _info = info;
            _holder = holder;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabel("Receipts");
            
            int i = 0;
            bool found = false;

            while (_holder.data.ContainsKey("craft"+i))
            {
                found = true;
                var d = SplitHelper.SplitInt(_holder.data["craft" + i]);
                Craft c = L.b.crafts[d.key];
                int id = i;
                Button b = panel.AddImageTextButton((d.value == -1?"Endless ":d.value+"x ") + c.Name(), c.Icon, () =>
                {
                    Remove(id);
                });
                
                i++;
            }

            if (!found)
                panel.AddLabel("No active receipt found. You can add some from the left menu.");
        }

        private void Remove(int i)
        {
            CraftMgmt.RebuildAfter(i, _holder.data);
            window.Reload();
        }

        public override void Perform()
        {
        }
    }
}