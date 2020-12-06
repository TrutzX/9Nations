using System;
using Classes;
using Libraries.FActions;
using Tools;

namespace Libraries.Spells
{
    [Serializable]
    public class SpellMgmt : BaseMgmt<Spell>
    {
        public SpellMgmt() : base("spell") { }

        protected override void ParseElement(Spell ele, string header, string data)
        {
            switch (header)
            {
                case "action":
                    ActionParse(ele.action, data);
                    break;
                case "difficult":
                    ele.difficult = Int(data);
                    break;
                case "cost":
                    ele.cost = Int(data);
                    break;
                case "animation":
                    ele.animation = data;
                    break;
                case "actionFail":
                    ActionParse(ele.actionFail, data);
                    break;
                case "reqTarget":
                    ele.reqTarget.Add(data);
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }
        
        protected void ActionParse(ActionHolders action, string data)
        {
            var a = SplitHelper.Delimiter(data);
            action.Add(LClass.s.GetNewAction(a.key).Create(a.value));
        }
    }
}