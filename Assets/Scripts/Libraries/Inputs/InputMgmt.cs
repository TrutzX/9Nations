using System;
using System.Collections.Generic;
using System.Linq;
using Buildings;
using Libraries.FActions;
using Libraries.GameButtons;
using Players;
using Tools;
using UnityEngine;

namespace Libraries.Inputs
{
    [Serializable]
    public class InputMgmt : BaseMgmt<InputKey>
    {
        public InputMgmt() : base("input")
        {
        }

        protected override void ParseElement(InputKey ele, string header, string data)
        {
            switch (header)
            {
                case "key":
                    ele.key = data;
                    break;
                case "type":
                    ele.type = data;
                    break;
                default:
                    base.ParseElement(ele, header, data);
                    break;
            }
        }
        
        /// <summary>
        /// Show the action with the key, if possible
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public string ActionName(FDataAction action)
        {
            //has the action an input?
            if (!ContainsKey(action.id))
            {
                return action.name;
            }

            return this[action.id].Name();
        }
        
        /// <summary>
        /// Show the gameButton with the key, if possible
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns> 
        public string GameButtonName(GameButton button)
        {
            //has the action an input?
            if (!ContainsKey(button.id))
            {
                return button.name;
            }

            return this[button.id].Name();
        }
    }
}