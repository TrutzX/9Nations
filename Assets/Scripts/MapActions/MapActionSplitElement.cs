using Buildings;
using DataTypes;
using UI;
using UnityEngine;

namespace MapActions
{
    public class MapActionSplitElement : SplitElement
    {
        private MapAction mapAction;
        private MapElementInfo self, nonSelf;
        public MapActionSplitElement(MapAction mapAction, MapElementInfo self, MapElementInfo nonSelf) : base(mapAction.name, SpriteHelper.LoadIcon(mapAction.icon))
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
            panel.AddReq("Own Requirements",mapAction.GenSelfReq(),self,self.X(),self.Y());
            panel.AddReq("Other Requirements",mapAction.GenNonSelfReq(),nonSelf,nonSelf.X(),nonSelf.Y());
        }

        public override void Perform()
        {
            self.data.ap -= mapAction.ap;
            Debug.LogWarning("perform "+mapAction.id);
            NLib.Get().mapActions[mapAction.id].Perform(self, nonSelf);
        }
    }
}