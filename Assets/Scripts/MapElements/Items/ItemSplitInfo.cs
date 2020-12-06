using System.Linq;
using System.Security.Principal;
using Buildings;
using Game;
using Libraries;
using UI;
using UI.Show;
using UnityEngine.UI;

namespace MapElements.Items
{
    public class ItemSplitInfo : SplitElement
    {
        private readonly MapElementInfo _info;
        
        public ItemSplitInfo(MapElementInfo info) : base(L.b.items.Name(),L.b.items.Sprite())
        {
            this._info = info;
        }

        public override void ShowDetail(PanelBuilder panel)
        {
            
        }

        public override void Perform()
        {
        }

        protected void AddItem(PanelBuilder panel, string id, string category)
        {
            panel.AddHeaderLabelT("item"+id);

            string title = S.T("itemEquipNo");
            string icon = "no";
            if (_info.data.items.ContainsKey(id))
            {
                var item = L.b.items[_info.data.items[id]];
                title = item.Name();
                icon = item.Icon;
            }

            var b = panel.AddImageTextButton(title, icon, () => { });
            b.onClick.AddListener(() => { Equip(id, category, b);});
        }

        private void Equip(string id, string category, Button button)
        {
            if (_info.Town() == null)
            {
                UIHelper.ShowOk(S.T("itemEquipNew"),S.T("townNo"));
                return;
            }
            
            WindowBuilderSplit wbs = WindowBuilderSplit.Create(S.T("itemEquipNew"),"Equip");
            wbs.Add(new ItemNoSelectSplitInfo(_info, button, id));
            foreach (var item in L.b.items.GetAllByCategory(category))
            {
                //has already?
                if (_info.data.items.ContainsValue(item.id))
                {
                    continue;
                }
                
                if (_info.Town().GetRes(item.id) >= 1)
                {
                    wbs.Add(new ItemSelectSplitInfo(_info, button, id, item));
                }
            }
            wbs.Finish();
        }
    }
}