using System;
using Classes;
using Tools;

namespace Libraries.Items
{
    [Serializable]
    public class ItemMgmt : BaseMgmt<Item>
    {
        public ItemMgmt() : base("item") { }

        protected override void ParseElement(Item ele, string header, string data)
        {
            switch (header)
            {
                case "action":
                    ActionParse(ele, data);
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }

        
        protected void ActionParse(Item ele, string data)
        {
            var a = SplitHelper.Delimiter(data);
            ele.action.Add(LClass.s.GetNewAction(a.key).Create(a.value));
        }
    }
}