using System;
using System.Collections.Generic;
using Classes;
using Game;
using Libraries.FActions;
using Libraries.FActions.General;
using Tools;
using UnityEngine;

namespace Libraries.Buildings
{
    [Serializable]
    public abstract class BaseDataBuildingUnitMgmt<T> : BaseMgmt<T> where T : BaseDataBuildingUnit, new()
    {
        protected ActionHolder lastAction;
        protected BaseDataBuildingUnitMgmt(string id) : base(id) { }
        protected BaseDataBuildingUnitMgmt(string id, string icon) : base(id, icon) { }

        protected override void ParseElement(T ele, string header, string data)
        {
            switch (header)
            {
                case "modi":
                    Delimiter(ele.modi, data);
                    break;
                case "action":
                    ActionParse(ele, data);
                    break;
                case "actionreq":
                    lastAction.req.Add(data);
                    break;
                case "actiondata":
                    var d = SplitHelper.Delimiter(data);
                    lastAction.data.Add(d.key,d.value);
                    break;
                case "buildtime":
                    ele.buildTime = Int(data);
                    break;
                case "cost":
                    Res(ele.cost,data);
                    break;
                case "visible":
                    ele.visibilityRange = Int(data);
                    break;
                case "hp":
                    ele.hp = Int(data);
                    break;
                case "ap":
                    ele.ap = Int(data);
                    break;
                case "atk":
                    ele.atk = Int(data);
                    break;
                case "def":
                    ele.def = Int(data);
                    break;
                case "dammin":
                    ele.damMin = Int(data);
                    break;
                case "dammax":
                    ele.damMax = Int(data);
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }

        protected void ActionParse(T ele, string data)
        {
            var a = SplitHelper.Delimiter(data);
            lastAction = LClass.s.GetNewAction(a.key).Create(a.value);
            ele.action.Add(lastAction);
        }
    }
}