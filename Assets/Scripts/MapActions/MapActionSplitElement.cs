using Buildings;
using DataTypes;
using UI;
using UI.Show;
using UnityEngine;

namespace MapActions
{
    public class MapActionSplitElement : SplitElement
    {
        private MapAction mapAction;
        private MapElementInfo self, nonSelf;
        public MapActionSplitElement(MapAction mapAction, MapElementInfo self, MapElementInfo nonSelf) : base(mapAction.name, SpriteHelper.Load(mapAction.icon))
        {
            this.mapAction = mapAction;
            this.self = self;
            this.nonSelf = nonSelf;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabel(mapAction.name);
            panel.AddLabel($"Cost: {mapAction.ap} AP");
            panel.AddLabel(mapAction.desc);
            panel.AddReq("Own Requirements",mapAction.GenSelfReq(),self,self.Pos());
            panel.AddReq("Other Requirements",mapAction.GenNonSelfReq(),nonSelf,nonSelf.Pos());
        }

        public override void Perform()
        {
            self.data.ap -= mapAction.ap;
            Debug.LogWarning("perform "+mapAction.id);
            OLib.Get().mapActions[mapAction.id].Perform(self, nonSelf);
        }
    }
}