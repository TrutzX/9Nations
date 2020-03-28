using System;
using Players;
using UnityEngine;
using UnityEngine.UI;

namespace Libraries.GameButtons
{
    [Serializable]
    public class GameButtonMgmt : BaseMgmt<GameButton>
    {
        public GameButtonMgmt() : base("gamebutton","GameButton","x") { }

        protected override void ParseElement(GameButtons.GameButton ele, string header, string data)
        {
            switch (header)
            {
                case "location":
                    ele.location = data;
                    break;
                default:
                    Debug.LogWarning($"{name} missing {header} for data {data}");
                    break;
            }
        }

        protected override GameButton Create()
        {
            return new GameButton();
        }
        
        public void BuildMenu(Player player, string location, Text text, bool button, Transform transform)
        {
            foreach (GameButton b in L.b.gameButtons.Values())
            {
                if (!b.location.Contains(location))
                {
                    continue;
                }
                
                //can use?
                if (!b.req.Check(player) && !global::Data.features.showAction.Bool())
                {
                    continue;
                }

                //create it
                if (button)
                {
                    b.CreateImageTextButton(transform, player);
                }
                else
                {
                    b.CreateImageButton(transform, player, text);
                }
            }
        }
    }
}