using Buildings;
using Classes.Actions.Addons;
using Game;
using Libraries.Buildings;
using Libraries.Crafts;
using Libraries.FActions;
using MapElements;
using Tools;
using UI;
using UI.Show;

namespace Crafts
{
    public class CraftSplitElement : SplitElement
    {
        private Craft _craft;
        private MapElementInfo _info;
        private ActionHolder _holder;
        
        public CraftSplitElement(Craft craft, MapElementInfo info, ActionHolder holder) : base(craft.Name(), craft.Icon)
        {
            _craft = craft;
            _info = info;
            _holder = holder;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            _craft.ShowLexicon(panel);
            _craft.req.BuildPanel(panel, _info, _info.Pos());
            
            panel.AddHeaderLabel("Actions");
            panel.AddImageTextButton(_craft.Name(), _craft.Icon, () =>
                _holder.data["craft" + NextFreeID()] = $"{_craft.id}:1"
            );
            panel.AddImageTextButton($"10x {_craft.Name()}", _craft.Icon, () =>
                _holder.data["craft" + NextFreeID()] = $"{_craft.id}:10"
            );
            panel.AddImageTextButton($"100x {_craft.Name()}", _craft.Icon, () =>
                _holder.data["craft" + NextFreeID()] = $"{_craft.id}:100"
            );
            panel.AddImageTextButton($"endless {_craft.Name()}", _craft.Icon, () =>
                _holder.data["craft" + NextFreeID()] = $"{_craft.id}:-1"
            );
        }
        
        private int NextFreeID()
        {
            int i = 0;

            while (_holder.data.ContainsKey("craft"+i))
            {
                i++;
            }

            return i;
        }
        
        public override void Perform()
        {
        }
    }
}