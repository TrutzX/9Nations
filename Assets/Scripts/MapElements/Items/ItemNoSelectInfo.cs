using System.Linq;
using Buildings;
using Classes.Actions;
using Game;
using Libraries;
using Libraries.Items;
using Tools;
using Towns;
using UI;
using UI.Show;
using UnityEngine;
using UnityEngine.UI;

namespace MapElements.Items
{
    public class ItemNoSelectSplitInfo : SplitElement
    {
        protected readonly string id;
        protected readonly MapElementInfo _info;
        protected readonly Button _button;
        
        public ItemNoSelectSplitInfo(MapElementInfo info, Button button, string id) : this(info, button, id, S.T("itemEquipUn"),SpriteHelper.Load("no"))
        {
        }
        
        public ItemNoSelectSplitInfo(MapElementInfo info, Button button, string id, string title, Sprite icon) : base(title,icon)
        {
            _info = info;
            this.id = id;
            _button = button;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            panel.AddHeaderLabelT("itemEquipNo");
            panel.AddLabelT("itemEquipUn");
        }

        public override void Perform()
        {
            RemoveOld();

            //set new
            _info.data.items.Remove(id);
            UIHelper.UpdateButtonText(_button,S.T("itemEquipNo"));
            UIHelper.UpdateButtonImage(_button, SpriteHelper.Load("no"));
        }

        protected void RemoveOld()
        {
            //remove old?
            if (_info.data.items.ContainsKey(id))
            {
                _info.Town().AddRes(_info.data.items[id], 1, ResType.Equip);
                L.b.items[_info.data.items[id]].action.Removes(ActionEvent.Quest, S.ActPlayer(), _info, _info.Pos());
            }
        }
    }
}