using System.Linq;
using Buildings;
using Classes.Actions;
using Game;
using Libraries;
using Libraries.Items;
using Towns;
using UI;
using UI.Show;
using UnityEngine.UI;

namespace MapElements.Items
{
    public class ItemSelectSplitInfo : ItemNoSelectSplitInfo
    {
        private readonly Item item;
        
        public ItemSelectSplitInfo(MapElementInfo info, Button button, string id, Item item) : base(info, button, id, item.Name(),item.Sprite())
        {
            this.item = item;
            disabled = item.req.Desc(info, info.Pos());
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            item.ShowOwn(panel, _info);
        }

        public override void Perform()
        {
            RemoveOld();
            
            //set new
            _info.data.items[id] = item.id;
            _info.Town().AddRes(item.id,-1, ResType.Equip);
            UIHelper.UpdateButtonText(_button,item.Name());
            UIHelper.UpdateButtonImage(_button, item.Sprite());
            L.b.items[_info.data.items[id]].action.Performs(ActionEvent.Quest, S.ActPlayer(), _info, _info.Pos());
        }
    }
}