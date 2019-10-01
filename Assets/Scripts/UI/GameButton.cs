using System.Collections;
using System.Collections.Generic;
using Players;
using reqs;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace DataTypes
{
    public partial class GameButton {
        // Start is called before the first frame update
        
        public Dictionary<string,string> GenReq()
        {
            return ReqHelper.GetReq(req1,req2);
        }

        public bool CheckReq(Player player)
        {
            return ReqHelper.Check(player, GenReq());
        }

        public GameObject CreateImageButton(Transform transform, Player player, Text text)
        {
            GameObject button = UIElements.CreateImageButton(icon, transform, () =>
            {
                GameButtonHelper.Call(id, player);
            }, sound);
            UIHelper.HoverEnter(text,name,button, () => { text.text = ""; });

            return button;
        }

        public GameObject CreateImageTextButton(Transform transform, Player player)
        {
            GameObject button = UIHelper.CreateImageTextButton(name, SpriteHelper.LoadIcon(icon), transform, () =>
            {
                NAudio.Play(sound);
                GameButtonHelper.Call(id, player);
            });
            return button;
        }
    }
}