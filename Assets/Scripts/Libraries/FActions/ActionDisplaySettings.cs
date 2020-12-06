using Buildings;
using JetBrains.Annotations;
using Libraries.FActions.General;
using MapElements;
using Players;
using Tools;
using UI;

namespace Libraries.FActions
{
    public class ActionDisplaySettings
    {
        public PanelBuilder panel;
        public Player player;
        public MapElementInfo mapElement;
        public NVector pos;
        public ActionHolder holder;
        public bool compact;
        public bool addReq;
        public string header;

        public ActionDisplaySettings(PanelBuilder panel, ActionHolder holder)
        {
            addReq = true;
            this.panel = panel;
            this.holder = holder;
        }
        
        public ActionDisplaySettings(PanelBuilder panel, Player player, ActionHolder holder): this(panel, holder)
        {
            this.player = player;
        }

        public ActionDisplaySettings(PanelBuilder panel, Player player, [CanBeNull] MapElementInfo mapElement, NVector pos, ActionHolder holder): this(panel, player, holder)
        {
            this.mapElement = mapElement;
            this.pos = pos;
        }
    }
}