using System;
using Buildings;
using Players;
using UnityEngine;
using UnityEngine.UI;

namespace Libraries.GameButtons
{
    [Serializable]
    public class GameButtonMgmt : BaseMgmt<GameButton>
    {
        public GameButtonMgmt() : base("gamebutton","GameButton",null) { }
        
        public void BuildMenu(Player player, string category, IMapUI text, bool button, Transform transform)
        {
            foreach (GameButton b in GetAllByCategory(category))
            {
                //can use?
                if (!b.req.Check(player) && !LSys.tem.options["showaction"].Bool())
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