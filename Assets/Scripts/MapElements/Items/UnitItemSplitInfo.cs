using Buildings;
using UI;

namespace MapElements.Items
{
    public class UnitItemSplitInfo : ItemSplitInfo
    {
        public UnitItemSplitInfo(MapElementInfo info) : base(info)
        {
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            AddItem(panel, "hand1", "hand");
            AddItem(panel, "hand2", "hand");
            AddItem(panel, "armour", "armour");
            AddItem(panel, "shoe", "shoe");
        }
    }
}