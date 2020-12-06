using Buildings;
using UI;

namespace MapElements.Items
{
    public class BuildingItemSplitInfo : ItemSplitInfo
    {
        public BuildingItemSplitInfo(MapElementInfo info) : base(info)
        {
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            AddItem(panel, "pos1", "building");
            AddItem(panel, "pos2", "building");
            AddItem(panel, "pos3", "building");
        }
    }
}